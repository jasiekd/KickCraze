namespace KickCraze.Api.Dto
{
    public class GetMatchInfoResponseDto
    {
        public string MatchDate { get; set; }
        public string HomeTeamID { get; set; }
        public string HomeTeamName { get; set; }
        public string HomeTeamCrestURL { get; set; }
        public string HomeTeamScore { get; set; }
        public string HomeTeamScoreBreak { get; set; }
        public string AwayTeamID { get; set; }
        public string AwayTeamName { get; set; }
        public string AwayTeamCrestURL { get; set; }
        public string AwayTeamScore { get; set; }
        public string AwayTeamScoreBreak { get; set; }
        public GetMatchInfoResponseDto(
        string matchDate, string homeTeamID, string homeTeamName, string homeTeamCrestURL, string homeTeamScore, string homeTeamScoreBreak, string awayTeamID,string awayTeamName, string awayTeamCrestURL, string awayTeamScore, string awayTeamScoreBreak)
        {
            MatchDate = matchDate;
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
        }
    }
}
