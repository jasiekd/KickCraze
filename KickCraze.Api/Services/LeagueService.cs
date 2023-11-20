using KickCraze.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KickCraze.Api.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly CustomHttpClient _customHttpClient;

        public LeagueService(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }
        public async Task<IActionResult> GetLeagues()
        {
            var response = await _customHttpClient.GetAsync($"competitions/");
            if(response.IsSuccessStatusCode)
            {
                GetLeaguesResponseDto getLeaguesResponseDto = new();
                string responseData = await response.Content.ReadAsStringAsync();
                dynamic jsonData = JsonConvert.DeserializeObject(responseData);
                getLeaguesResponseDto.Leagues.Add(new LeagueElement("ALL", "Wszystkie ligi", "https://i.imgur.com/zDqOHeY.png", true));
                foreach (var league in jsonData.competitions)
                {
                    string type = league.type;
                    if (type != "LEAGUE") continue;
                    string leagueID = league.id;
                    string leagueName = league.name;
                    string leagueEmblemURL = league.emblem;
                    getLeaguesResponseDto.Leagues.Add(new LeagueElement(leagueID, leagueName, leagueEmblemURL));
                }
                return new OkObjectResult(getLeaguesResponseDto);
            }
            else
            {
                return new BadRequestResult();
            }
        }
        public async Task<IActionResult> GetLeagueTable(GetLeagueTableRequestDto getLeagueTableRequestDto)
        {
            //var response = await _customHttpClient.GetAsync($"competitions/{getLeagueTableRequestDto.LeagueID}/standings");
            var response = await _customHttpClient.GetAsync($"competitions/{getLeagueTableRequestDto.LeagueID}/standings?date={getLeagueTableRequestDto.Date:yyyy-MM-dd}"); //pomyslec nad tym
            if(response.IsSuccessStatusCode)
            {
                GetLeagueTableResponseDto getLeagueTableResponseDto = new();
                string responseData = await response.Content.ReadAsStringAsync();
                dynamic jsonData = JsonConvert.DeserializeObject(responseData);
                getLeagueTableResponseDto.NameLeague = jsonData.competition.name;
                var standings = jsonData.standings;
                if (standings != null && standings.Count > 0)
                {
                    var table = standings[0].table;

                    if (table != null && table.Count > 0)
                    {
                        foreach (var team in table)
                        {
                            int position = team.position;
                            string teamName = team.team.name;
                            string teamCrestURL = team.team.crest;
                            int points = team.points;
                            int played = team.playedGames;
                            int won = team.won;
                            int draw = team.draw;
                            int lost = team.lost;
                            int goalsFor = team.goalsFor;
                            int goalsAgainst = team.goalsAgainst;
                            int goalDifference = team.goalDifference;

                            getLeagueTableResponseDto.LeagueTable.Add(new LeagueTableElement(position,teamName,teamCrestURL,points,played,won,draw,lost,goalsFor,goalsAgainst,goalDifference)
                            );

                        }
                    }
                }
                return new OkObjectResult(getLeagueTableResponseDto);
            }
            else
            {
                return new BadRequestResult();
            }
            
        }
    }
}
