using System.Text;

namespace FetchData
{
    public class Match
    {

        public Match(int i) {
            MatchID = i;
            MatchStatus = "status";
            HomeTeamID = 0;
            HomeTeamName = "homeTeamName";
            HomeTeamGoalDiffBefore = 0;
            HomeTeamGoalDiffAfter = 0;
            HomeTeamPositionBefore = 0;
            HomeTeamPositionAfter = 0;
            HomeTeamScore = 0;
            HomeTeamScoreBreak = 0;
            AwayTeamID = 0;
            AwayTeamName = "awayTeamName";
            AwayTeamGoalDiffBefore = 0;
            AwayTeamGoalDiffAfter = 0;
            AwayTeamPositionBefore = 0;
            AwayTeamPositionAfter = 0;
            AwayTeamScore = 0;
            AwayTeamScoreBreak = 0;
            MatchDate = DateTime.Now;
            MatchResult = "DRAW";
        }
        public Match(int matchID, string? matchStatus, int homeTeamID, string homeTeamName, int homeTeamGoalDiffBefore, int homeTeamGoalDiffAfter, int homeTeamPositionBefore, int homeTeamPositionAfter, int? homeTeamScore, int? homeTeamScoreBreak, int awayTeamID, string awayTeamName, int awayTeamGoalDiffBefore, int awayTeamGoalDiffAfter, int awayTeamPositionBefore, int awayTeamPositionAfter, int? awayTeamScore, int? awayTeamScoreBreak, DateTime matchDate, string matchResult)
        {
            MatchID = matchID;
            MatchStatus = matchStatus;
            HomeTeamID = homeTeamID;
            HomeTeamName = homeTeamName;
            HomeTeamGoalDiffBefore = homeTeamGoalDiffBefore;
            HomeTeamGoalDiffAfter = homeTeamGoalDiffAfter;
            HomeTeamPositionBefore = homeTeamPositionBefore;
            HomeTeamPositionAfter = homeTeamPositionAfter;
            HomeTeamScore = homeTeamScore;
            HomeTeamScoreBreak = homeTeamScoreBreak;
            AwayTeamID = awayTeamID;
            AwayTeamName = awayTeamName;
            AwayTeamGoalDiffBefore = awayTeamGoalDiffBefore;
            AwayTeamGoalDiffAfter = awayTeamGoalDiffAfter;
            AwayTeamPositionBefore = awayTeamPositionBefore;
            AwayTeamPositionAfter = awayTeamPositionAfter;
            AwayTeamScore = awayTeamScore;
            AwayTeamScoreBreak = awayTeamScoreBreak;
            MatchDate = matchDate;
            MatchResult = matchResult;
        }

        public int MatchID { get; set; }
        public string? MatchStatus { get; set; }
        public int HomeTeamID { get; set; }
        public string HomeTeamName { get; set; }
        public int HomeTeamGoalDiffBefore { get; set; }
        public int HomeTeamGoalDiffAfter { get; set; }
        public int HomeTeamPositionBefore { get; set; }
        public int HomeTeamPositionAfter { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? HomeTeamScoreBreak { get; set; }
        public int AwayTeamID { get; set; }
        public string AwayTeamName { get; set; }
        public int AwayTeamGoalDiffBefore { get; set; }
        public int AwayTeamGoalDiffAfter { get; set; }
        public int AwayTeamPositionBefore { get; set; }
        public int AwayTeamPositionAfter { get; set; }
        public int? AwayTeamScore { get; set; }
        public int? AwayTeamScoreBreak { get; set; }
        public DateTime MatchDate { get; set; }
        public string MatchResult { get; set; }

        public override string? ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (MatchStatus.Equals("FINISHED"))
            {
                builder.AppendFormat("{0}[{1}] {2} - {3} [{4}]{5} {6}", HomeTeamName, HomeTeamPositionBefore, HomeTeamScore, AwayTeamScore, AwayTeamPositionBefore, AwayTeamName, MatchDate);
            }
            else
            {
                builder.AppendFormat("{0} vs {1} {2}", HomeTeamName, AwayTeamName, MatchDate);
            }
            return builder.ToString();
        }
    }
}
