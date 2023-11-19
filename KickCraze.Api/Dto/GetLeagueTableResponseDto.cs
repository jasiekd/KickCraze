namespace KickCraze.Api.Dto
{
    public class GetLeagueTableResponseDto
    {
        public string NameLeague { get; set; }

        public List<LeagueTableElement>  LeagueTable { get; set; } = new List<LeagueTableElement>();
    }
}
