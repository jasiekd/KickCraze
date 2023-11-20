namespace KickCraze.Api.Dto
{
    public class MatchElement
    {
        public int MatchID { get; set; }
        public int LeagueID { get; set; }
        public string LeagueName { get; set; }
        public string LeagueSeason { get; set; }
        public string? MatchStatus { get; set; }
        public int HomeTeamID { get; set; }
        public string HomeTeamName { get; set; }
        public int? HomeTeamScore { get; set; }
        public string HomeTeamCrestURL { get; set; }
        public int AwayTeamID { get; set; }
        public string AwayTeamName { get; set; }
        public int? AwayTeamScore { get; set; }
        public string AwayTeamCrestURL { get; set; }
        public DateTime MatchDate { get; set; }
        public MatchElement(int matchID, int leagueID, string leagueName ,string leagueSeason, string? matchStatus, int homeTeamID, string homeTeamName,  int? homeTeamScore, string homeTeamCrestURL, int awayTeamID, string awayTeamName, int? awayTeamScore, string awayTeamCrestURL, DateTime matchDate)
        {
            MatchID = matchID;
            LeagueID = leagueID;
            LeagueName = leagueName;
            LeagueSeason = leagueSeason;
            MatchStatus = matchStatus;
            HomeTeamID = homeTeamID;
            HomeTeamName = homeTeamName;
            HomeTeamScore = homeTeamScore;
            HomeTeamCrestURL = homeTeamCrestURL;
            AwayTeamID = awayTeamID;
            AwayTeamName = awayTeamName;
            AwayTeamScore = awayTeamScore;
            AwayTeamCrestURL = awayTeamCrestURL;
            MatchDate = matchDate;
        }
    }
}
