using KickCraze.Api.Model;
using Microsoft.ML.Data;
using Microsoft.ML;
using Microsoft.ML.Trainers.FastTree;
using TrainingModel;

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

int parts = 10; //parametr odpowiadajacy na ile czesci maja byc podzielone dane
int numberOfTrees = 6; //parametr ustawiajacy ilosc tworzonych drzew do utworzenia modelu
int numberOfLeaves = 4; //parametr ustawiajacy maksymalna ilosc lisci na dane drzewo
double learningRate = 0.005; //parametr ustawiajacy wspolczynnik uczenia
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
    .Append(mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryEstimator: mlContext.BinaryClassification.Trainers.FastTree(new FastTreeBinaryTrainer.Options() { RandomStart = true, NumberOfTrees = numberOfTrees, NumberOfLeaves = numberOfLeaves, MinimumExampleCountPerLeaf = 2, LearningRate = learningRate, LabelColumnName = @"MatchResult", FeatureColumnName = @"Features", DiskTranspose = false, FeatureFraction = 1, }), labelColumnName: @"MatchResult"))
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
    Console.WriteLine($"Oceny modelu dla danych uczących z {i + 1} dzielenia danych");
    Console.WriteLine($"LogLosss: {metricsL.LogLoss}");
    Console.WriteLine($"MacroAccuracy: {metricsL.MacroAccuracy}");
    Console.WriteLine($"MicroAccuracy: {metricsL.MicroAccuracy}");

    var predictions = model.Transform(testData);
    var metrics = mlContext.MulticlassClassification.Evaluate(predictions, labelColumnName: @"MatchResult");
    features.Add(metrics.MacroAccuracy);
    Console.WriteLine($"Oceny modelu dla danych testowych z {i + 1} dzielenia danych");
    Console.WriteLine($"LogLosss: {metrics.LogLoss}");
    Console.WriteLine($"MacroAccuracy: {metrics.MacroAccuracy}");
    Console.WriteLine($"MicroAccuracy: {metrics.MicroAccuracy}");
}

Console.WriteLine($"Średnia MacroAccurancy dla danych uczacych: {featuresL.Average()}");
Console.WriteLine($"Średnia MacroAccurancy dla danych testowych: {features.Average()}");
Console.WriteLine($"najlepsza MacroAccurancy dla danych uczacych: {featuresL.ElementAt(features.IndexOf(features.Max()))}");
Console.WriteLine($"najlepsza MacroAccurancy dla danych testowych: {features.Max()}");

var saveModel = pipeline.Fit(crossValidation.ElementAt(features.IndexOf(features.Max())).TrainSet);

DataViewSchema dataViewSchema = data.Schema;

mlContext.Model.Save(saveModel, dataViewSchema, "MatchResultModel.zip");

var predictionsBestL = saveModel.Transform(crossValidation.ElementAt(features.IndexOf(features.Max())).TrainSet);
var metricsBestL = mlContext.MulticlassClassification.Evaluate(predictionsBestL, labelColumnName: @"MatchResult");
Console.WriteLine($"Oceny modelu dla danych uczących z najlepszego dzielenia danych");
Console.WriteLine($"MacroAccuracy: {metricsBestL.MacroAccuracy}");
Console.WriteLine($"MicroAccuracy: {metricsBestL.MicroAccuracy}");
Console.WriteLine($"ConfusionMatrix: ");
Console.WriteLine(metricsBestL.ConfusionMatrix.GetFormattedConfusionTable());


var predictionsBest = saveModel.Transform(crossValidation.ElementAt(features.IndexOf(features.Max())).TestSet);
var metricsBest = mlContext.MulticlassClassification.Evaluate(predictionsBest, labelColumnName: @"MatchResult");
Console.WriteLine($"Oceny modelu dla danych testowych z najlepszego dzielenia danych");
Console.WriteLine($"MacroAccuracy: {metricsBest.MacroAccuracy}");
Console.WriteLine($"MicroAccuracy: {metricsBest.MicroAccuracy}");
Console.WriteLine($"ConfusionMatrix: ");
Console.WriteLine(metricsBest.ConfusionMatrix.GetFormattedConfusionTable());

var predictionavedEngine = mlContext.Model.CreatePredictionEngine<FootballMatchData, MatchPrediction>(saveModel);



Lazy<PredictionEngine<FootballMatchData, MatchPrediction>> PredictEngine = new Lazy<PredictionEngine<FootballMatchData, MatchPrediction>>(() => predictionavedEngine, true);

var predEngine = PredictEngine.Value;
var result = predEngine.Predict(TestMatchData.Match1);


var sortedScoresWithLabel = GetSortedScoresWithLabels(result, PredictEngine);
Console.WriteLine($"{"Class",-40}{"Score",-20}");
Console.WriteLine($"{"-----",-40}{"-----",-20}");

foreach (var score in sortedScoresWithLabel)
{
    Console.WriteLine($"{score.Key,-40}{score.Value,-20}");
}



static IOrderedEnumerable<KeyValuePair<string, float>> GetSortedScoresWithLabels(MatchPrediction result, Lazy<PredictionEngine<FootballMatchData, MatchPrediction>> predictionEngine)
{
    var unlabeledScores = result.Score;
    var labelNames = GetLabels(result, predictionEngine);

    Dictionary<string, float> labledScores = new Dictionary<string, float>();
    for (int i = 0; i < labelNames.Count(); i++)
    {
        var labelName = labelNames.ElementAt(i);
        labledScores.Add(labelName.ToString(), unlabeledScores[i]);
    }

    return labledScores.OrderByDescending(c => c.Value);
}

static IEnumerable<string> GetLabels(MatchPrediction result, Lazy<PredictionEngine<FootballMatchData, MatchPrediction>> predictionEngine)
{
    var schema = predictionEngine.Value.OutputSchema;

    var labelColumn = schema.GetColumnOrNull("MatchResult");
    if (labelColumn == null)
    {
        throw new Exception("MatchResult column not found. Make sure the name searched for matches the name in the schema.");
    }

    var keyNames = new VBuffer<ReadOnlyMemory<char>>();
    labelColumn.Value.GetKeyValues(ref keyNames);
    return keyNames.DenseValues().Select(x => x.ToString());
}