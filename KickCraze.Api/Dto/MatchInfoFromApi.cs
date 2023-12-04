namespace KickCraze.Api.Dto
{
    public class MatchInfoFromApi
    {
        public MatchInfoFromApi(int matchID, string? matchStatus, int homeTeamID, string homeTeamName, string homeTeamCrestURL,int? homeTeamScore, int? homeTeamScoreBreak, int awayTeamID, string awayTeamName, string awayTeamCrestURL, int? awayTeamScore, int? awayTeamScoreBreak, DateTime matchDate, string matchResult)
        {
            MatchID = matchID;
            MatchStatus = matchStatus;
            HomeTeamID = homeTeamID;
            HomeTeamName = homeTeamName;
            HomeTeamCrestURL = homeTeamCrestURL;
            HomeTeamScore = homeTeamScore;
            HomeTeamScoreBreak = homeTeamScoreBreak;
            AwayTeamID = awayTeamID;
            AwayTeamName = awayTeamName;
            AwayTeamCrestURL = awayTeamCrestURL;
            AwayTeamScore = awayTeamScore;
            AwayTeamScoreBreak = awayTeamScoreBreak;
            MatchDate = matchDate;
            MatchResult = matchResult;
        }

        public int MatchID { get; set; }
        public string? MatchStatus { get; set; }
        public int HomeTeamID { get; set; }
        public string HomeTeamName { get; set; }
        public string HomeTeamCrestURL { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? HomeTeamScoreBreak { get; set; }
        public int AwayTeamID { get; set; }
        public string AwayTeamName { get; set; }
        public string AwayTeamCrestURL { get; set; }
        public int? AwayTeamScore { get; set; }
        public int? AwayTeamScoreBreak { get; set; }
        public DateTime MatchDate { get; set; }
        public string MatchResult { get; set; }
    }
}
