namespace KickCraze.Api.Dto
{
    public class LeagueTableElement
    {
        public int Position { get; set; }
        public string TeamName { get; set; }
        public string TeamCrestURL { get; set; }
        public int Points { get; set; }
        public int Played {  get; set; }
        public int Won { get; set; }
        public int Draw {  get; set; }
        public int Lost { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference {  get; set; }

        public LeagueTableElement(int position, string teamName, string teamCrestURL, int points, int played, int won, int draw, int lost, int goalsFor, int goalsAgainst, int goalDifference)
        {
            Position = position;
            TeamName = teamName;
            TeamCrestURL = teamCrestURL;
            Points = points;
            Played = played;
            Won = won;
            Draw = draw;
            Lost = lost;
            GoalsFor = goalsFor;
            GoalsAgainst = goalsAgainst;
            GoalDifference = goalDifference;
        }
    }
}
