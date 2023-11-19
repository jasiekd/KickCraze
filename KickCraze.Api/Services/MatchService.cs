using KickCraze.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KickCraze.Api.Services
{
    public class MatchService : IMatchService
    {
        private readonly CustomHttpClient _customHttpClient;

        public MatchService(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }

        public async Task<IActionResult> GetMatches(GetMatchesRequestDto matchesData)
        {
            HttpResponseMessage? response;
            if (matchesData.LeagueID == "ALL")
            {
                response = await _customHttpClient.GetAsync($"matches?date={matchesData.Date:yyyy-MM-dd}");
            }
            else
            {
                response = await _customHttpClient.GetAsync($"competitions/{matchesData.LeagueID}/matches?dateFrom={matchesData.Date:yyyy-MM-dd}&dateTo={matchesData.Date:yyyy-MM-dd}");
            }
            if (response.IsSuccessStatusCode)
            {
                List<MatchElement> allMatches = new();
                string content = await response.Content.ReadAsStringAsync();
                dynamic jsonData = JsonConvert.DeserializeObject(content);

                foreach (var match in jsonData.matches)
                {
                    
                    int matchID = match.id;
                    string matchStatus = match.status;
                    int matchDay = match.matchday;
                    int homeTeamID = match.homeTeam.id;
                    string homeTeamName = match.homeTeam.name;
                    int? homeTeamScore = match.score.fullTime.home;
                    int? homeTeamScoreBreak = match.score.halfTime.home;
                    string homeTeamCrestURL = match.homeTeam.crest;
                    int awayTeamID = match.awayTeam.id;
                    string awayTeamName = match.awayTeam.name;
                    int? awayTeamScore = match.score.fullTime.away;
                    int? awayTeamScoreBreak = match.score.halfTime.away;
                    string awayTeamCrestURL = match.awayTeam.crest;
                    string result = match.score.winner;
                    int leagueID = match.competition.id;
                    string leagueName = match.competition.name;
                    DateTime matchDate = match.utcDate;
                    TimeZoneInfo polandTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Warsaw");
                    DateTime polandDateTime = TimeZoneInfo.ConvertTimeFromUtc(matchDate, polandTimeZone);

                    matchDate = polandDateTime;


                    allMatches.Add(new(matchID, leagueID, leagueName, matchStatus, homeTeamID, homeTeamName, homeTeamScore, homeTeamCrestURL, awayTeamID, awayTeamName, awayTeamScore, awayTeamCrestURL, matchDate));
                }

                List<GetMatchesResponseDto> matchesDataDto = new();
                foreach (var match in allMatches)
                {
                    if(matchesDataDto.Any(x => x.LeagueID == match.LeagueID))
                    {
                        matchesDataDto.First(x => x.LeagueID == match.LeagueID).Matches.Add(match);
                    }
                    else
                    {
                        GetMatchesResponseDto tmp = new()
                        {
                            LeagueID = match.LeagueID,
                            LeagueName = match.LeagueName
                        };
                        tmp.Matches.Add(match);
                        matchesDataDto.Add(tmp);
                    }
                }

                return new OkObjectResult(matchesDataDto);
            }
            else
            {
                return new BadRequestResult();
            }
        }

        public async Task<IActionResult> GetMatchInfo(GetMatchesRequestDto matchesData)
        {
            return new OkResult();
        }
        public async Task<IActionResult> PredictResult(GetMatchesRequestDto matchesData)
        {
            return new OkResult();
        }

        //public async Task<MatchElement> GetMatchElement(dynamic match)
        //{
        //    int matchID = match.id;
        //    string matchStatus = match.status;
        //    int matchDay = match.matchday;
        //    int homeTeamID = match.homeTeam.id;
        //    string homeTeamName = match.homeTeam.name;
        //    int? homeTeamScore = match.score.fullTime.home;
        //    int? homeTeamScoreBreak = match.score.halfTime.home;
        //    int awayTeamID = match.awayTeam.id;
        //    string awayTeamName = match.awayTeam.name;
        //    int? awayTeamScore = match.score.fullTime.away;
        //    int? awayTeamScoreBreak = match.score.halfTime.away;
        //    string result = match.score.winner;
        //    int leagueID = match.competition.id;
        //    DateTime matchDate = match.utcDate;
        //    TimeZoneInfo polandTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Warsaw");
        //    DateTime polandDateTime = TimeZoneInfo.ConvertTimeFromUtc(matchDate, polandTimeZone);

        //    matchDate = polandDateTime;

        //    return new(matchID, matchStatus, homeTeamID, homeTeamName, homeTeamScore, awayTeamID, awayTeamName, awayTeamScore, matchDate, result);
        //}

        //async Task<TeamPositions> getPositions(int homeTeamID, int awayTeamID, string season, int matchDay, int leagueID)
        //{
        //    int homeTeamPosition = 0;
        //    int awayTeamPosition = 0;
        //    int homeTeamGoalDiff = 0;
        //    int awayTeamGoalDiff = 0;



        //    if (matchDay == 1)
        //    {
        //        return new TeamPositions
        //        {
        //            HomeTeamPosition = homeTeamPosition,
        //            AwayTeamPosition = awayTeamPosition,
        //            HomeTeamGoalDiff = homeTeamGoalDiff,
        //            AwayTeamGoalDiff = awayTeamGoalDiff,
        //        };
        //    }

        //    HttpResponseMessage responseTable = await _customHttpClient.GetAsync($"competitions/{leagueID}/standings/?season={season}&matchday={matchDay - 1}");
        //        if (responseTable.IsSuccessStatusCode)
        //        {
        //            string contentTable = await responseTable.Content.ReadAsStringAsync();
        //            dynamic jsonData = JsonConvert.DeserializeObject(contentTable);
        //            dynamic standings = jsonData.standings;
        //            if (standings != null && standings.Count > 0)
        //            {
        //                var table = standings[0].table;

        //                if (table != null && table.Count > 0)
        //                {
        //                    foreach (var team in table)
        //                    {
        //                        int teamTableID = team.team.id;
        //                        int position = team.position;
        //                        int goalsFor = team.goalsFor;
        //                        int goalsAgainst = team.goalsAgainst;
        //                        if (teamTableID.Equals(homeTeamID))
        //                        {
        //                            homeTeamPosition = position;
        //                            homeTeamGoalDiff = goalsFor - goalsAgainst;
        //                        }

        //                        if (teamTableID.Equals(awayTeamID))
        //                        {
        //                            awayTeamPosition = position;
        //                            awayTeamGoalDiff = goalsFor - goalsAgainst;
        //                        }
        //                    }
        //                }
        //            }
        //        return new TeamPositions
        //        {
        //            HomeTeamPosition = homeTeamPosition,
        //            AwayTeamPosition = awayTeamPosition,
        //            HomeTeamGoalDiff = homeTeamGoalDiff,
        //            AwayTeamGoalDiff = awayTeamGoalDiff,
        //        };
        //    }
        //        else
        //        {
        //            // Obsługa błędu
        //            Console.WriteLine($"Error: {responseTable.StatusCode}");
        //        }
        //    }
        //}
    }
}

