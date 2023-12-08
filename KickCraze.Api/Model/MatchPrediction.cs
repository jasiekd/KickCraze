using Microsoft.ML.Data;

namespace KickCraze.Api.Model
{
    public class MatchPrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLabel { get; set; }

        [ColumnName("Score")]
        public float[] Score { get; set; }
    }
}
