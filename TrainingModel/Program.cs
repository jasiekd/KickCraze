using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;
using System.Data;
using KickCraze.Api.Model;

var mlContext = new MLContext(seed: 0);

var options = new TextLoader.Options()
{
    Separators = new char[] { ';' },
    HasHeader = true
};

string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);


//var data = mlContext.Data.LoadFromTextFile<FootballMatchData>($"{currentDirectory}\\data.csv", options);
var data = mlContext.Data.LoadFromTextFile<FootballMatchData>($"{currentDirectory}\\dataz0.csv", options);
//var data = mlContext.Data.LoadFromTextFile<FootballMatchData>($"{currentDirectory}\\dataz0dodatkowedane.csv", options);
int parts = 10;
var crossValidation = mlContext.Data.CrossValidationSplit(data, numberOfFolds: parts, seed: 0);

var featureColumns = data.Schema
    .Select(col => col.Name)
    .Where(colName => colName != "MatchResult" && colName != "HomeTeamName" && colName != "AwayTeamName")
.ToArray();

//kod do trenowania modelu na roznych parametrach uczenia

var topResults = new List<(double MacroAccuracyTrain, double MacroAccuracyTest, FastTreeBinaryTrainer.Options Parameters)>();
var bestMacroAccuracy = double.MinValue;
List<double> learningRates = new() { 0.001, 0.005, 0.01 }; // lista z wspolczynnikami uczenia

for (int j = 0; j < learningRates.Count; j++)
{
    //zakres parametru utworzonych liczby drzew podczas tworzenia modelu
    for (int numberOfTrees = 4; numberOfTrees <= 12; numberOfTrees += 2)
    {
        //zakres parametru maksymalnej liczby lisci do tworzenia modelu
        for (int numberOfLeaves = 4; numberOfLeaves <= 10; numberOfLeaves += 2)
        {
            var trainerOptions = new FastTreeBinaryTrainer.Options()
            {
                RandomStart = true,
                NumberOfTrees = numberOfTrees,
                NumberOfLeaves = numberOfLeaves,
                MinimumExampleCountPerLeaf = 2,
                LearningRate = learningRates.ElementAt(j),
                LabelColumnName = @"MatchResult",
                FeatureColumnName = @"Features",
                DiskTranspose = false,
                FeatureFraction = 1,
            };
            //definiowanie parametrów modelu
            var pipeline = mlContext.Transforms.Conversion.MapValueToKey(@"MatchResult", @"MatchResult")
                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"H1MatchResult", outputColumnName: @"H1MatchResult"))
                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"H2MatchResult", outputColumnName: @"H2MatchResult"))
                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"H3MatchResult", outputColumnName: @"H3MatchResult"))
                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"H4MatchResult", outputColumnName: @"H4MatchResult"))
                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"H5MatchResult", outputColumnName: @"H5MatchResult"))
                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"A1MatchResult", outputColumnName: @"A1MatchResult"))
                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"A2MatchResult", outputColumnName: @"A2MatchResult"))
                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"A3MatchResult", outputColumnName: @"A3MatchResult"))
                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"A4MatchResult", outputColumnName: @"A4MatchResult"))
                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"A5MatchResult", outputColumnName: @"A5MatchResult"))
                    .Append(mlContext.Transforms.Concatenate("Features", featureColumns))
                .Append(mlContext.MulticlassClassification.Trainers.OneVersusAll(
                    mlContext.BinaryClassification.Trainers.FastTree(trainerOptions),
                    labelColumnName: @"MatchResult"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: @"PredictedLabel", inputColumnName: @"PredictedLabel"));
            //sprawdzanie parametrów modelu
            List<double> featuresL = new();
            List<double> features = new();

            for (int i = 0; i < parts; i++)
            {
                var trainData = crossValidation[i].TrainSet;
                var testData = crossValidation[i].TestSet;
                var model = pipeline.Fit(trainData);

                var predictionsL = model.Transform(trainData);
                var metricsL = mlContext.MulticlassClassification.Evaluate(predictionsL, labelColumnName: @"MatchResult");
                featuresL.Add(metricsL.MacroAccuracy);

                var predictions = model.Transform(testData);
                var metrics = mlContext.MulticlassClassification.Evaluate(predictions, labelColumnName: @"MatchResult");
                features.Add(metrics.MacroAccuracy);
            }

            var averageMacroAccuracyTest = features.Average();
            var averageMacroAccuracyTrain = featuresL.Average();
            topResults.Add((averageMacroAccuracyTrain, averageMacroAccuracyTest, trainerOptions));

        }
    }
}

topResults.Sort((x, y) => y.MacroAccuracyTest.CompareTo(x.MacroAccuracyTest));

//Wybór 10 najlepszych rezultatów
var top10Results = topResults.Take(10);

Console.WriteLine("10 najlepszych wyników:");
foreach (var result in top10Results)
{
    Console.WriteLine($"MacroAccuracy dla danych uczacych: {result.MacroAccuracyTrain}");
    Console.WriteLine($"MacroAccuracy dla danych testowych: {result.MacroAccuracyTest}");
    Console.WriteLine($"Najlepsze parametry uczenia:");
    Console.WriteLine($"LearningRate: {result.Parameters.LearningRate}");
    Console.WriteLine($"NumberOfTrees: {result.Parameters.NumberOfTrees}");
    Console.WriteLine($"NumberOfLeaves: {result.Parameters.NumberOfLeaves}");
}



