namespace KickCraze.Api.Dto
{
    public class LeagueElement
    {
        public string LeagueID { get; set; }
        public string LeagueName { get; set;}
        public string LeagueEmblemURL { get; set; }
        public bool Active { get; set; } = false;

        public LeagueElement(string leagueID, string leagueName, string leagueEmblemURL, bool active=false)
        {
            LeagueID = leagueID;
            LeagueName = leagueName;
            LeagueEmblemURL = leagueEmblemURL;
            Active = active;
        }
    }
}
