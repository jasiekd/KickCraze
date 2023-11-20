namespace KickCraze.Api.Dto
{
    public class GetMatchesResponseDto
    {
        public int LeagueID { get; set; }
        public string LeagueName { get; set; }
        public string LeagueSeason { get; set; }
        public List<MatchElement> Matches { get; set; } = new List<MatchElement>();
    }
}
