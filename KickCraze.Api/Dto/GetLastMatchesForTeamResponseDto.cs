namespace KickCraze.Api.Dto
{
    public class GetLastMatchesForTeamResponseDto
    {
        public string TeamID { get; set; }
        public List<LastMatchesForTeamResponseElement> LastMatches { get; set; } = new List<LastMatchesForTeamResponseElement>();
    }
}
