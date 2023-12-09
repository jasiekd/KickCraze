using KickCraze.Api.Dto;
using KickCraze.Api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using Microsoft.ML;
using Microsoft.ML.Data;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace KickCraze.Api.Services
{
    public class MatchService : IMatchService
    {
        private readonly CustomHttpClient _customHttpClient;
        private readonly PredictionEnginePool<FootballMatchData, MatchPrediction> _predictionEnginePool;
        private readonly MLContext _mlContext;

        public MatchService(CustomHttpClient customHttpClient, PredictionEnginePool<FootballMatchData, MatchPrediction> predictionEnginePool)
        {
            _customHttpClient = customHttpClient;
            _predictionEnginePool = predictionEnginePool;
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

                MatchInfoFromApi tmp = await GetMatchInfoFromApi(jsonData);

                GetMatchInfoResponseDto responseDto = new(tmp.MatchDate.ToString("dd.MM.yyyy HH:mm"), tmp.HomeTeamID.ToString(), tmp.HomeTeamName, tmp.HomeTeamCrestURL, tmp.HomeTeamScore.ToString(), tmp.HomeTeamScoreBreak.ToString(), tmp.AwayTeamID.ToString(), tmp.AwayTeamName, tmp.AwayTeamCrestURL, tmp.AwayTeamScore.ToString(), tmp.AwayTeamScoreBreak.ToString());
                return new OkObjectResult(responseDto);
            }
            else
            {
                return new BadRequestResult();
            }
        }

        private async Task<MatchInfoFromApi> GetMatchInfoFromApi(dynamic match)
        {
            int matchID = match.id;
            string matchStatus = match.status;
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
            DateTime matchDate = match.utcDate;
            TimeZoneInfo polandTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Warsaw");
            DateTime polandDateTime = TimeZoneInfo.ConvertTimeFromUtc(matchDate, polandTimeZone);

            matchDate = polandDateTime;

            return new ( matchID, matchStatus, homeTeamID, homeTeamName, homeTeamCrestURL, homeTeamScore, homeTeamScoreBreak, awayTeamID, awayTeamName, awayTeamCrestURL, awayTeamScore, awayTeamScoreBreak, matchDate, result );
        }
        private async Task<MatchInfoFromApiToPrediction> GetMatchInfoFromApiToPrediction(dynamic match, Dictionary<(string, int, int), dynamic> leagueTables)
        {
            int matchID = match.id;
            string matchStatus = match.status;
            int matchDay = match.matchday;
            string startDate = match.season.startDate;
            int homeTeamID = match.homeTeam.id;
            string homeTeamName = match.homeTeam.name;
            int? homeTeamScore = match.score.fullTime.home;
            int? homeTeamScoreBreak = match.score.halfTime.home;
            int awayTeamID = match.awayTeam.id;
            string awayTeamName = match.awayTeam.name;
            int? awayTeamScore = match.score.fullTime.away;
            int? awayTeamScoreBreak = match.score.halfTime.away;
            string result = match.score.winner;
            int leagueID = match.competition.id;
            DateTime matchDate = match.utcDate;
            TimeZoneInfo polandTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Warsaw");
            DateTime polandDateTime = TimeZoneInfo.ConvertTimeFromUtc(matchDate, polandTimeZone);

            var teamsPositionsBefore = await GetPositions(homeTeamID, awayTeamID, startDate[..4], matchDay, leagueID, leagueTables);
            var teamsPositionsAfter = await GetPositions(homeTeamID, awayTeamID, startDate[..4], matchDay + 1, leagueID, leagueTables);

            matchDate = polandDateTime;

            return new(matchID, leagueID, matchStatus, homeTeamID, homeTeamName, teamsPositionsBefore.HomeTeamGoalDiff, teamsPositionsAfter.HomeTeamGoalDiff, teamsPositionsBefore.HomeTeamPosition, teamsPositionsAfter.HomeTeamPosition, homeTeamScore, homeTeamScoreBreak, awayTeamID, awayTeamName, teamsPositionsBefore.AwayTeamGoalDiff, teamsPositionsAfter.AwayTeamGoalDiff, teamsPositionsBefore.AwayTeamPosition, teamsPositionsAfter.AwayTeamPosition, awayTeamScore, awayTeamScoreBreak, matchDate, result);
        }

        async Task<TeamPositions?> GetPositions(int homeTeamID, int awayTeamID, string season, int matchDay, int leagueID, Dictionary<(string, int, int), dynamic> leagueTables)
        {
            int homeTeamPosition = 0;
            int awayTeamPosition = 0;
            int homeTeamGoalDiff = 0;
            int awayTeamGoalDiff = 0;

            if (matchDay == 1)
            {
                return new TeamPositions
                {
                    HomeTeamPosition = homeTeamPosition,
                    AwayTeamPosition = awayTeamPosition,
                    HomeTeamGoalDiff = homeTeamGoalDiff,
                    AwayTeamGoalDiff = awayTeamGoalDiff,
                };
            }

            dynamic standings = null;

            if (leagueTables.ContainsKey((season, matchDay - 1, leagueID)))
            {
                standings = leagueTables.GetValueOrDefault((season, matchDay - 1, leagueID));
            }
            else
            {
                string linkTable = $"https://api.football-data.org/v4/competitions/{leagueID}/standings/?season={season}&matchday={matchDay - 1}";  //tabela ligi
                HttpResponseMessage responseTable = await _customHttpClient.GetAsync(linkTable);
                if (responseTable.IsSuccessStatusCode)
                {
                    string contentTable = await responseTable.Content.ReadAsStringAsync();
                    dynamic jsonData = JsonConvert.DeserializeObject(contentTable);
                    standings = jsonData.standings;
                    leagueTables.Add((season, matchDay - 1, leagueID), standings);
                }
                else
                {
                    // Obsługa błędu
                    Console.WriteLine($"Error: {responseTable.StatusCode}");
                    return null;
                }
            }

            //Console.WriteLine(standings);
            if (standings != null && standings.Count > 0)
            {
                var table = standings[0].table;

                if (table != null && table.Count > 0)
                {
                    foreach (var team in table)
                    {
                        int teamTableID = team.team.id;
                        int position = team.position;
                        int goalsFor = team.goalsFor;
                        int goalsAgainst = team.goalsAgainst;
                        if (teamTableID.Equals(homeTeamID))
                        {
                            homeTeamPosition = position;
                            homeTeamGoalDiff = goalsFor - goalsAgainst;
                        }

                        if (teamTableID.Equals(awayTeamID))
                        {
                            awayTeamPosition = position;
                            awayTeamGoalDiff = goalsFor - goalsAgainst;
                        }
                    }
                }
            }

            return new TeamPositions
            {
                HomeTeamPosition = homeTeamPosition,
                AwayTeamPosition = awayTeamPosition,
                HomeTeamGoalDiff = homeTeamGoalDiff,
                AwayTeamGoalDiff = awayTeamGoalDiff,
            };
        }

        private async Task GetLast5MatchesToPredictAsync(Dictionary<int, MatchInfoFromApiToPrediction> last5Matches, string last5URL, Dictionary<(string, int, int), dynamic> leagueTables)
        {
            HttpResponseMessage responseLast5 = await _customHttpClient.GetAsync(last5URL);
            if (responseLast5.IsSuccessStatusCode)
            {
                string contentLast = await responseLast5.Content.ReadAsStringAsync();
                dynamic dataLast = JsonConvert.DeserializeObject(contentLast);

                //Console.WriteLine(dataLast);

                for (int i = dataLast.matches.Count - 1; i >= 0; i--)
                {
                    if (last5Matches.Count == 5) break;
                    //Console.WriteLine(match);
                    string type = dataLast.matches[i].competition.type;
                    string stage = dataLast.matches[i].stage;
                    if (type != "LEAGUE") continue;
                    if (stage != "REGULAR_SEASON") continue;
                    MatchInfoFromApiToPrediction tmp = await GetMatchInfoFromApiToPrediction(dataLast.matches[i], leagueTables);
                    last5Matches.Add(tmp.MatchID, tmp);
                }

                for (int i = last5Matches.Count; i < 5; i++)
                {
                    MatchInfoFromApiToPrediction tmp = new(i);
                    last5Matches.Add(tmp.MatchID, tmp);
                }
            }
            else
            {
                // Obsługa błędu
                Console.WriteLine($"Error: {responseLast5.StatusCode}");
            }
        }

        private const int NumberOfMatchesToConsider = 5;

        public static async Task<FootballMatchData> MatchesDataToFootballMatchData(MatchInfoFromApiToPrediction mainMatch, Dictionary<int, MatchInfoFromApiToPrediction> homeLast5Matches, Dictionary<int, MatchInfoFromApiToPrediction> awayLast5Matches)
        {
            var footballMatchData = new FootballMatchData();

            // Map data for the main match
            MapMatchData(mainMatch, footballMatchData);

            // Map data for home team's last 5 matches
            MapLastMatchesData(homeLast5Matches, footballMatchData, isHomeTeam: true);

            // Map data for away team's last 5 matches
            MapLastMatchesData(awayLast5Matches, footballMatchData, isHomeTeam: false);

            return footballMatchData;
        }

        private static void MapMatchData(MatchInfoFromApiToPrediction match, FootballMatchData footballMatchData)
        {
            footballMatchData.HomeTeamName = match.HomeTeamName;
            footballMatchData.HomeTeamGoalDiff = match.HomeTeamGoalDiffAfter;
            footballMatchData.HomeTeamPosition = match.HomeTeamPositionAfter;
            footballMatchData.AwayTeamName = match.AwayTeamName;
            footballMatchData.AwayTeamGoalDiff = match.AwayTeamGoalDiffAfter;
            footballMatchData.AwayTeamPosition = match.AwayTeamPositionAfter;
            footballMatchData.MatchResult = match.MatchResult;
        }

        private static void MapLastMatchesData(Dictionary<int, MatchInfoFromApiToPrediction> lastMatches, FootballMatchData footballMatchData, bool isHomeTeam)
        {
            for (int i = 1; i <= lastMatches.Count; i++)
            {
                MapLastMatchData(lastMatches.ElementAt(i-1).Value, footballMatchData, i, isHomeTeam);
            }
        }

        private static void MapLastMatchData(MatchInfoFromApiToPrediction lastMatch, FootballMatchData footballMatchData, int matchNumber, bool isHomeTeam)
        {
            // Determine the prefix for the properties based on whether it's the home or away team
            string prefix = isHomeTeam ? $"H{matchNumber}" : $"A{matchNumber}";

            // Map the data for the specific past match
            footballMatchData.GetType().GetProperty($"{prefix}LastHomeTeamGoalDiffBef")?.SetValue(footballMatchData, lastMatch.HomeTeamGoalDiffBefore);
            footballMatchData.GetType().GetProperty($"{prefix}LastHomeTeamGoalDiffAft")?.SetValue(footballMatchData, lastMatch.HomeTeamGoalDiffAfter);
            footballMatchData.GetType().GetProperty($"{prefix}LastHomeTeamPosBef")?.SetValue(footballMatchData, lastMatch.HomeTeamPositionBefore);
            footballMatchData.GetType().GetProperty($"{prefix}LastHomeTeamPosAft")?.SetValue(footballMatchData, lastMatch.HomeTeamPositionAfter);
            footballMatchData.GetType().GetProperty($"{prefix}LastHomeTeamScoreBreak")?.SetValue(footballMatchData, lastMatch.HomeTeamScoreBreak);
            footballMatchData.GetType().GetProperty($"{prefix}LastHomeTeamScore")?.SetValue(footballMatchData, lastMatch.HomeTeamScore);
            footballMatchData.GetType().GetProperty($"{prefix}LastAwayTeamGoalDiffBef")?.SetValue(footballMatchData, lastMatch.AwayTeamGoalDiffBefore);
            footballMatchData.GetType().GetProperty($"{prefix}LastAwayTeamGoalDiffAft")?.SetValue(footballMatchData, lastMatch.AwayTeamGoalDiffAfter);
            footballMatchData.GetType().GetProperty($"{prefix}LastAwayTeamPosBef")?.SetValue(footballMatchData, lastMatch.AwayTeamPositionBefore);
            footballMatchData.GetType().GetProperty($"{prefix}LastAwayTeamPosBefAft")?.SetValue(footballMatchData, lastMatch.AwayTeamPositionAfter);
            footballMatchData.GetType().GetProperty($"{prefix}LastAwayTeamScoreBreak")?.SetValue(footballMatchData, lastMatch.AwayTeamScoreBreak);
            footballMatchData.GetType().GetProperty($"{prefix}LastAwayTeamScore")?.SetValue(footballMatchData, lastMatch.AwayTeamScore);
            footballMatchData.GetType().GetProperty($"{prefix}MatchResult")?.SetValue(footballMatchData, lastMatch.MatchResult);
        }

        public IEnumerable<string> GetLabels()
        {
            var schema = _predictionEnginePool.GetPredictionEngine(modelName: "ResultMatchPrediction").OutputSchema;

            var labelColumn = schema.GetColumnOrNull("MatchResult");
            if (labelColumn == null)
            {
                throw new Exception("MatchResult column not found. Make sure the name searched for matches the name in the schema.");
            }

            var keyNames = new VBuffer<ReadOnlyMemory<char>>();
            labelColumn.Value.GetKeyValues(ref keyNames);
            return keyNames.DenseValues().Select(x => x.ToString());
        }

        public IOrderedEnumerable<KeyValuePair<string, float>> GetSortedScoresWithLabels(MatchPrediction result)
        {
            var unlabeledScores = result.Score;
            var labelNames = GetLabels();

            Dictionary<string, float> labledScores = new Dictionary<string, float>();
            for (int i = 0; i < labelNames.Count(); i++)
            {
                // Map the names to the predicted result score array
                var labelName = labelNames.ElementAt(i);
                labledScores.Add(labelName.ToString(), unlabeledScores[i]);
            }

            return labledScores.OrderByDescending(c => c.Value);
        }

        public async Task<IActionResult> PredictResult(PredictResultRequestDto predictResultRequest)
        {
            HttpResponseMessage? response = await _customHttpClient.GetAsync($"matches/{predictResultRequest.MatchID}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic jsonData = JsonConvert.DeserializeObject(content);
                Dictionary<(string, int, int), dynamic> leagueTables = new();

                MatchInfoFromApiToPrediction mainMatch = await GetMatchInfoFromApiToPrediction(jsonData, leagueTables);
                string date300DaysAgoFromMatchDate = mainMatch.MatchDate.AddDays(-300).ToString("yyyy-MM-dd");
                string homeLast5URL = $"https://api.football-data.org/v4/teams/{mainMatch.HomeTeamID}/matches?status=FINISHED&dateFrom={date300DaysAgoFromMatchDate}&dateTo={mainMatch.MatchDate.AddDays(-1):yyyy-MM-dd}&limit=10&competitions={mainMatch.LeagueID}";
                string awayLast5URL = $"https://api.football-data.org/v4/teams/{mainMatch.AwayTeamID}/matches?status=FINISHED&dateFrom={date300DaysAgoFromMatchDate}&dateTo={mainMatch.MatchDate.AddDays(-1):yyyy-MM-dd}&limit=10&competitions={mainMatch.LeagueID}";
                Dictionary<int, MatchInfoFromApiToPrediction> homeLast5Matches = new();
                Dictionary<int, MatchInfoFromApiToPrediction> awayLast5Matches = new();
                

                await GetLast5MatchesToPredictAsync(homeLast5Matches, homeLast5URL, leagueTables);

                await GetLast5MatchesToPredictAsync(awayLast5Matches, awayLast5URL, leagueTables);

                FootballMatchData matchData = await MatchesDataToFootballMatchData(mainMatch, homeLast5Matches, awayLast5Matches);

                var result = _predictionEnginePool.Predict(modelName: "ResultMatchPrediction", matchData);

                var sortedScoresWithLabel = GetSortedScoresWithLabels(result);

                List<PredictResultResponseDto> predictResults = new()
                {
                    new() { Key = mainMatch.HomeTeamName, Value = (int)Math.Round(sortedScoresWithLabel.First(x => x.Key == "HOME_TEAM").Value * 100), Color = "#28a745" },
                    new() { Key = "Remis", Value = (int)Math.Round(sortedScoresWithLabel.First(x => x.Key == "DRAW").Value * 100) , Color = "#6c757d" },
                    new() { Key = mainMatch.AwayTeamName, Value = (int)Math.Round(sortedScoresWithLabel.First(x => x.Key == "AWAY_TEAM").Value * 100), Color = "#dc3545" }
                };


                return new OkObjectResult(predictResults);
            }
            else
            {
                return new BadRequestResult();
            }
        }

        public async Task<IActionResult> GetLastMatchesForTeam(GetLastMatchesForTeamRequestDto matchData)
        {
            string date300DaysAgo = matchData.MatchDate.AddDays(-60).ToString("yyyy-MM-dd");
            HttpResponseMessage response = await _customHttpClient.GetAsync($"teams/{matchData.TeamID}/matches?status=FINISHED&dateFrom={date300DaysAgo}&dateTo={matchData.MatchDate.AddDays(-1):yyyy-MM-dd}&limit=10");

            if (response.IsSuccessStatusCode)
            {
                GetLastMatchesForTeamResponseDto responseDto = new()
                {
                    TeamID = matchData.TeamID
                };
                string content = await response.Content.ReadAsStringAsync();
                dynamic jsonData = JsonConvert.DeserializeObject(content);

                for(int i = jsonData.matches.Count - 1; i >= 0;i--)
                //foreach (var match in jsonData.matches)
                {
                    if (responseDto.LastMatches.Count == 5) break;
                    string type = jsonData.matches[i].competition.type;
                    string stage = jsonData.matches[i].stage;
                    if (type != "LEAGUE") continue;
                    if (stage != "REGULAR_SEASON") continue;
                    MatchInfoFromApi tmp = await GetMatchInfoFromApi(jsonData.matches[i]);
                    responseDto.LastMatches.Add(new LastMatchesForTeamResponseElement(tmp.MatchID.ToString(), tmp.MatchDate, tmp.HomeTeamID.ToString(), tmp.HomeTeamName, tmp.HomeTeamCrestURL, tmp.HomeTeamScore.ToString(), tmp.AwayTeamID.ToString(), tmp.AwayTeamName, tmp.AwayTeamCrestURL, tmp.AwayTeamScore.ToString()));
                }

                //responseDto.LastMatches = responseDto.LastMatches.AsQueryable().OrderByDescending(x => x.MatchDate).ToList();

                return new OkObjectResult(responseDto);
            }
            else
            {
                return new BadRequestResult();
            }
        }
    }
}
