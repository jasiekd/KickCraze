using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
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
int parts = 5;
var crossValidation = mlContext.Data.CrossValidationSplit(data, numberOfFolds: parts, seed: 0);

var featureColumns = data.Schema
    .Select(col => col.Name)
    .Where(colName => colName != "MatchResult" && colName != "HomeTeamName" && colName != "AwayTeamName")
.ToArray();


//kod do przetrenowania najlepszego modelu i zapisaniu najlepszej czesci do pliku

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
    .Append(mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryEstimator: mlContext.BinaryClassification.Trainers.FastTree(new FastTreeBinaryTrainer.Options() { RandomStart = true, NumberOfTrees = 6, NumberOfLeaves = 6, MinimumExampleCountPerLeaf = 2, LearningRate = 0.001, LabelColumnName = @"MatchResult", FeatureColumnName = @"Features", DiskTranspose = false, FeatureFraction = 1, }), labelColumnName: @"MatchResult"))
    //.Append(mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryEstimator: mlContext.BinaryClassification.Trainers.FastForest(new FastForestBinaryTrainer.Options() { LabelColumnName = @"MatchResult", FeatureColumnName = @"Features", NumberOfLeaves = 4, NumberOfTrees = 4, MinimumExampleCountPerLeaf = 2, FeatureFraction = 0.8 }), labelColumnName: @"MatchResult"))
    .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: @"PredictedLabel", inputColumnName: @"PredictedLabel"));

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
    //Console.WriteLine($"Oceny modelu dla danych uczących z {i + 1} dzielenia danych");
    //Console.WriteLine($"LogLosss: {metricsL.LogLoss}");
    //Console.WriteLine($"Per-class log-loss: {string.Join(", ", metricsL.PerClassLogLoss)}");
    //Console.WriteLine($"MacroAccuracy: {metricsL.MacroAccuracy}");
    //Console.WriteLine($"MicroAccuracy: {metricsL.MicroAccuracy}");
    //Console.WriteLine($"ConfusionMatrix: ");
    //Console.WriteLine(metricsL.ConfusionMatrix.GetFormattedConfusionTable());


    var predictions = model.Transform(testData);
    var metrics = mlContext.MulticlassClassification.Evaluate(predictions, labelColumnName: @"MatchResult");
    features.Add(metrics.MacroAccuracy);
    //Console.WriteLine($"Oceny modelu dla danych testowych z {i + 1} dzielenia danych");
    //Console.WriteLine($"LogLosss: {metrics.LogLoss}");
    //Console.WriteLine($"Per-class log-loss: {string.Join(", ", metrics.PerClassLogLoss)}");
    //Console.WriteLine($"MacroAccuracy: {metrics.MacroAccuracy}");
    //Console.WriteLine($"MicroAccuracy: {metrics.MicroAccuracy}");
    //Console.WriteLine($"ConfusionMatrix: ");
    //Console.WriteLine(metrics.ConfusionMatrix.GetFormattedConfusionTable());


}

Console.WriteLine($"Średnia MacroAccurancy dla danych uczacych: {featuresL.Average()}");
Console.WriteLine($"Średnia MacroAccurancy dla danych testowych: {features.Average()}");
Console.WriteLine($"najlepsza MacroAccurancy dla danych uczacych: {featuresL.ElementAt(features.IndexOf(features.Max()))}");
Console.WriteLine($"najlepsza MacroAccurancy dla danych testowych: {features.Max()}");

var saveModel = pipeline.Fit(crossValidation.ElementAt(features.IndexOf(features.Max())).TrainSet);

DataViewSchema dataViewSchema = data.Schema;

mlContext.Model.Save(saveModel, dataViewSchema,"MatchResultModel.zip");


//kod do trenowania modelu na roznych parametrach uczenia

//var topResults = new List<(double MacroAccuracy, FastTreeBinaryTrainer.Options Parameters)>();
//var bestMacroAccuracy = double.MinValue;
//var bestParameters = new FastTreeBinaryTrainer.Options(); // Zainicjuj najlepsze parametry domyślnymi wartościami
//List<double> learningRates = new List<double> { 0.001, 0.005, 0.01 };

//for (int j = 0; j < learningRates.Count; j++)
//{
//    for (int numberOfTrees = 4; numberOfTrees <= 12; numberOfTrees += 2)
//    {
//        for (int numberOfLeaves = 4; numberOfLeaves <= 10; numberOfLeaves += 2)
//        {
//            // Inne parametry uczenia można dostosować w podobny sposób

//            var trainerOptions = new FastTreeBinaryTrainer.Options()
//            {
//                RandomStart = true,
//                NumberOfTrees = numberOfTrees,
//                NumberOfLeaves = numberOfLeaves,
//                MinimumExampleCountPerLeaf = 2,
//                //LearningRate = learningRate,
//                LearningRate = learningRates.ElementAt(j),
//                LabelColumnName = @"MatchResult",
//                FeatureColumnName = @"Features",
//                DiskTranspose = false,
//                FeatureFraction = 1,
//            };

//            var pipeline = mlContext.Transforms.Conversion.MapValueToKey(@"MatchResult", @"MatchResult")
//                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"H1MatchResult", outputColumnName: @"H1MatchResult"))
//                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"H2MatchResult", outputColumnName: @"H2MatchResult"))
//                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"H3MatchResult", outputColumnName: @"H3MatchResult"))
//                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"H4MatchResult", outputColumnName: @"H4MatchResult"))
//                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"H5MatchResult", outputColumnName: @"H5MatchResult"))
//                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"A1MatchResult", outputColumnName: @"A1MatchResult"))
//                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"A2MatchResult", outputColumnName: @"A2MatchResult"))
//                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"A3MatchResult", outputColumnName: @"A3MatchResult"))
//                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"A4MatchResult", outputColumnName: @"A4MatchResult"))
//                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"A5MatchResult", outputColumnName: @"A5MatchResult"))
//                    .Append(mlContext.Transforms.Concatenate("Features", featureColumns))
//                .Append(mlContext.MulticlassClassification.Trainers.OneVersusAll(
//                    mlContext.BinaryClassification.Trainers.FastTree(trainerOptions),
//                    labelColumnName: @"MatchResult"))
//                .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: @"PredictedLabel", inputColumnName: @"PredictedLabel"));

//            List<double> featuresL = new();
//            List<double> features = new();

//            for (int i = 0; i < parts; i++)
//            {
//                var trainData = crossValidation[i].TrainSet;
//                var testData = crossValidation[i].TestSet;

//                var model = pipeline.Fit(trainData);

//                //Console.WriteLine($"Oceny modelu dla danych uczących z {i + 1} dzielenia danych");
//                var predictionsL = model.Transform(trainData);
//                var metricsL = mlContext.MulticlassClassification.Evaluate(predictionsL, labelColumnName: @"MatchResult");
//                //Console.WriteLine($"LogLosss: {metricsL.LogLoss}");
//                //Console.WriteLine($"Per-class log-loss: {string.Join(", ", metricsL.PerClassLogLoss)}");
//                //Console.WriteLine($"MacroAccuracy: {metricsL.MacroAccuracy}");
//                featuresL.Add(metricsL.MacroAccuracy);
//                //Console.WriteLine($"MicroAccuracy: {metricsL.MicroAccuracy}");
//                //Console.WriteLine($"ConfusionMatrix: ");
//                //Console.WriteLine(metricsL.ConfusionMatrix.GetFormattedConfusionTable());

//                //Console.WriteLine($"Oceny modelu dla danych testowych z {i + 1} dzielenia danych");
//                var predictions = model.Transform(testData);
//                var metrics = mlContext.MulticlassClassification.Evaluate(predictions, labelColumnName: @"MatchResult");
//                //Console.WriteLine($"LogLosss: {metrics.LogLoss}");
//                //Console.WriteLine($"Per-class log-loss: {string.Join(", ", metrics.PerClassLogLoss)}");
//                //Console.WriteLine($"MacroAccuracy: {metrics.MacroAccuracy}");
//                features.Add(metrics.MacroAccuracy);
//                //Console.WriteLine($"MicroAccuracy: {metrics.MicroAccuracy}");
//                //Console.WriteLine($"ConfusionMatrix: ");
//                //Console.WriteLine(metrics.ConfusionMatrix.GetFormattedConfusionTable());
//            }


//            var averageMacroAccuracy = features.Average();

//            topResults.Add((averageMacroAccuracy, trainerOptions));
//            //Console.WriteLine($"Średnia MacroAccurancy dla danych testowych: {averageMacroAccuracy}");

//        }
//    }
//}

//topResults.Sort((x, y) => y.MacroAccuracy.CompareTo(x.MacroAccuracy));

//// Wybierz 10 najlepszych wyników
//var top5Results = topResults.Take(10);

//Console.WriteLine("10 najlepszych wyników:");
//foreach (var result in top5Results)
//{
//    Console.WriteLine($"MacroAccuracy: {result.MacroAccuracy}");
//    Console.WriteLine($"Najlepsze parametry uczenia:");
//    Console.WriteLine($"LearningRate: {result.Parameters.LearningRate}");
//    Console.WriteLine($"NumberOfTrees: {result.Parameters.NumberOfTrees}");
//    Console.WriteLine($"NumberOfLeaves: {result.Parameters.NumberOfLeaves}");
//}