using System.ComponentModel.DataAnnotations;

namespace KickCraze.Api.Entities
{
    public class LeaguesEntity
    {
        [Key]
        public int LeagueID { get; set; }
        public string LeagueName { get; set; }
        public string LeagueEmblemURL { get; set; }
    }
}
