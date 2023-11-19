namespace KickCraze.Api.Dto
{
    public class GetMatchesRequestDto
    {
        public string LeagueID { get; set; } = "ALL";
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
