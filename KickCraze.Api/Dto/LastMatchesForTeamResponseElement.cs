namespace KickCraze.Api.Dto
{
    public class LastMatchesForTeamResponseElement
    {
        public string MatchID { get; set; }
        public DateTime MatchDate { get; set; }
        public string HomeTeamID { get; set; }
        public string HomeTeamName { get; set; }
        public string HomeTeamCrestURL { get; set; }
        public string HomeTeamScore { get; set; }
        public string AwayTeamID { get; set; }
        public string AwayTeamName { get; set; }
        public string AwayTeamCrestURL { get; set; }
        public string AwayTeamScore { get; set; }
        public LastMatchesForTeamResponseElement(string matchID, DateTime matchDate, string homeTeamID, string homeTeamName, string homeTeamCrestURL, string homeTeamScore, string awayTeamID, string awayTeamName, string awayTeamCrestURL, string awayTeamScore)
        {
            MatchID = matchID;
            MatchDate = matchDate;
            HomeTeamID = homeTeamID;
            HomeTeamName = homeTeamName;
            HomeTeamCrestURL = homeTeamCrestURL;
            HomeTeamScore = homeTeamScore;
            AwayTeamID = awayTeamID;
            AwayTeamName = awayTeamName;
            AwayTeamCrestURL = awayTeamCrestURL;
            AwayTeamScore = awayTeamScore;
        }
    }
}
