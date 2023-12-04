using KickCraze.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

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
                response = await _customHttpClient.GetAsync($"matches?date={matchesData.Date:yyyy-MM-dd}&competitions=2002,2015,2021,2014,2019");
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
                    string leagueSeason = match.season.startDate;
                    DateTime matchDate = match.utcDate;
                    TimeZoneInfo polandTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Warsaw");
                    DateTime polandDateTime = TimeZoneInfo.ConvertTimeFromUtc(matchDate, polandTimeZone);

                    matchDate = polandDateTime;

                    if (matchStatus.Equals("FINISHED"))
                    {
                        matchStatus = "Koniec";
                    }
                    else if (matchStatus.Equals("IN_PLAY") || matchStatus.Equals("PAUSED"))
                    {
                        matchStatus = "W trakcie";
                    }else if (matchStatus.Equals("POSTPONED"))
                    {
                        matchStatus = "Przełożony";
                    }
                    else
                    {
                        matchStatus = string.Empty;
                    }

                    allMatches.Add(new(matchID, leagueID, leagueName, leagueSeason[..4], matchStatus, homeTeamID, homeTeamName, homeTeamScore, homeTeamCrestURL, awayTeamID, awayTeamName, awayTeamScore, awayTeamCrestURL, matchDate));
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
                            LeagueName = match.LeagueName,
                            LeagueSeason = match.LeagueSeason,
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

        public async Task<IActionResult> GetMatchInfo(GetMatchInfoRequestDto matchData)
        {
            HttpResponseMessage? response = await _customHttpClient.GetAsync($"matches/{matchData.MatchID}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic jsonData = JsonConvert.DeserializeObject(content);

                MatchInfoFromApi tmp = await GetMatch(jsonData);

                //DateTime matchDate = jsonData.utcDate;
                //TimeZoneInfo polandTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Warsaw");
                //DateTime polandDateTime = TimeZoneInfo.ConvertTimeFromUtc(matchDate, polandTimeZone);
                //string homeTeamID = jsonData.homeTeam.id;
                //string homeTeamName = jsonData.homeTeam.name;
                //string homeTeamCrest = jsonData.homeTeam.crest;
                //string homeTeamScore = jsonData.score.fullTime.home;
                //string homeTeamScoreBreak = jsonData.score.halfTime.home;
                //string awayTeamID = jsonData.awayTeam.id;
                //string awayTeamName = jsonData.awayTeam.name;
                //string awayTeamCrest = jsonData.awayTeam.crest;
                //string awayTeamScore = jsonData.score.fullTime.away;
                //string awayTeamScoreBreak = jsonData.score.halfTime.away;

                GetMatchInfoResponseDto responseDto = new(tmp.MatchDate.ToString("dd.MM.yyyy HH:mm"), tmp.HomeTeamID.ToString(), tmp.HomeTeamName, tmp.HomeTeamCrestURL, tmp.HomeTeamScore.ToString(), tmp.HomeTeamScoreBreak.ToString(), tmp.AwayTeamID.ToString(), tmp.AwayTeamName, tmp.AwayTeamCrestURL, tmp.AwayTeamScore.ToString(), tmp.AwayTeamScoreBreak.ToString());
                return new OkObjectResult(responseDto);
            }
            else
            {
                return new BadRequestResult();
            }
        }

        private async Task<MatchInfoFromApi> GetMatch(dynamic match)
        {
            int matchID = match.id;
            string matchStatus = match.status;
            int matchDay = match.matchday;
            string startDate = match.season.startDate;
            int homeTeamID = match.homeTeam.id;
            string homeTeamName = match.homeTeam.name;
            string homeTeamCrestURL = match.homeTeam.crest;
            int? homeTeamScore = match.score.fullTime.home;
            int? homeTeamScoreBreak = match.score.halfTime.home;
            int awayTeamID = match.awayTeam.id;
            string awayTeamName = match.awayTeam.name;
            string awayTeamCrestURL = match.awayTeam.crest;
            int? awayTeamScore = match.score.fullTime.away;
            int? awayTeamScoreBreak = match.score.halfTime.away;
            string result = match.score.winner;
            int leagueID = match.competition.id;
            DateTime matchDate = match.utcDate;
            TimeZoneInfo polandTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Warsaw");
            DateTime polandDateTime = TimeZoneInfo.ConvertTimeFromUtc(matchDate, polandTimeZone);

            matchDate = polandDateTime;

            return new ( matchID, matchStatus, homeTeamID, homeTeamName, homeTeamCrestURL, homeTeamScore, homeTeamScoreBreak, awayTeamID, awayTeamName, awayTeamCrestURL, awayTeamScore, awayTeamScoreBreak, matchDate, result );
        }

        public async Task<IActionResult> GetLastMatchesForTeam(GetLastMatchesForTeamRequestDto matchData)
        {
            string date300DaysAgo = matchData.MatchDate.AddDays(-300).ToString("yyyy-MM-dd");
            HttpResponseMessage response = await _customHttpClient.GetAsync($"teams/{matchData.TeamID}/matches?status=FINISHED&dateFrom={date300DaysAgo}&dateTo={matchData.MatchDate.AddDays(-1):yyyy-MM-dd}&limit=10");

            if(response.IsSuccessStatusCode)
            {
                GetLastMatchesForTeamResponseDto responseDto = new()
                {
                    TeamID = matchData.TeamID
                };
                string content = await response.Content.ReadAsStringAsync();
                dynamic jsonData = JsonConvert.DeserializeObject(content);

                foreach(var match in jsonData.matches)
                {
                    if (responseDto.LastMatches.Count == 5) break;
                    //Console.WriteLine(match);
                    string type = match.competition.type;
                    string stage = match.stage;
                    if (type != "LEAGUE") continue;
                    if (stage != "REGULAR_SEASON") continue;
                    MatchInfoFromApi tmp = await GetMatch(match);
                    responseDto.LastMatches.Add(new LastMatchesForTeamResponseElement(tmp.MatchID.ToString(), tmp.MatchDate.ToString(), tmp.HomeTeamID.ToString(), tmp.HomeTeamName, tmp.HomeTeamCrestURL, tmp.AwayTeamScore.ToString(), tmp.AwayTeamID.ToString(), tmp.AwayTeamName, tmp.AwayTeamCrestURL, tmp.AwayTeamScore.ToString()));
                }
                //responseDto.LastMatches = test;
                return new OkObjectResult(responseDto);
            }
            else
            {
                return new BadRequestResult();
            }
            //    if (responseLast5.IsSuccessStatusCode)
            //    {
            //        string contentLast = await responseLast5.Content.ReadAsStringAsync();
            //        dynamic dataLast = JsonConvert.DeserializeObject(contentLast);

            //        //Console.WriteLine(dataLast);

            //        foreach (var match in dataLast.matches)
            //        {
            //            if (last5Matches.Count == 5) return;
            //            //Console.WriteLine(match);
            //            string type = match.competition.type;
            //            string stage = match.stage;
            //            if (type != "LEAGUE") continue;
            //            if (stage != "REGULAR_SEASON") continue;
            //            Match tmp = await GetMatch(match);
            //            last5Matches.Add(tmp.MatchID, tmp);
            //        }
            //    }
            //    else
            //    {
            //        // Obsługa błędu
            //        Console.WriteLine($"Error: {responseLast5.StatusCode}");
            //    }
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

