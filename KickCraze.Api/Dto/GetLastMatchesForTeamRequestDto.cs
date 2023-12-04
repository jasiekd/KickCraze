namespace KickCraze.Api.Dto
{
    public class GetLastMatchesForTeamRequestDto
    {
        public string TeamID { get; set; }
        public DateTime MatchDate { get; set; }
    }
}
