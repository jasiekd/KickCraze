using FetchData;
using Newtonsoft.Json;

TimeSpan delayBetweenRequests = TimeSpan.FromSeconds(60);
string authToken = "b815483e661e4e57b5a1a27b80af477c";

Dictionary<(string, int, int), dynamic> leagueTables = new();
string competitionID = "2014";


HttpClient client = new(new CustomHttpClientHandler(delayBetweenRequests));
client.DefaultRequestHeaders.Add("X-Auth-Token", authToken);

for (int i = 1; i <= 38; i++)
{
    string linkLearn = $"http://api.football-data.org/v4/competitions/2014/matches?season=2022&matchday={i}";
    HttpResponseMessage responseL = await client.GetAsync(linkLearn);

    // Przetworzenie odpowiedzi i wyświetlenie lig, które grają dzisiaj mecze
    if (responseL.IsSuccessStatusCode)
    {
        string content = await responseL.Content.ReadAsStringAsync();

        dynamic data = JsonConvert.DeserializeObject(content);
        //Console.WriteLine(data.matches);

        foreach (var match in data.matches)
        {
            Match mainMatch = await GetMatch(match);

            Console.WriteLine(mainMatch.ToString());
            //Console.WriteLine(match);
            string date60DaysAgo = mainMatch.MatchDate.AddDays(-300).ToString("yyyy-MM-dd");
            //Ostatnie 5 meczy dla wybranych druzyn z meczu
            string homeLast5URL = $"https://api.football-data.org/v4/teams/{mainMatch.HomeTeamID}/matches?status=FINISHED&dateFrom={date60DaysAgo}&dateTo={mainMatch.MatchDate.AddDays(-1):yyyy-MM-dd}&limit=10&competitions={competitionID}";
            string awayLast5URL = $"https://api.football-data.org/v4/teams/{mainMatch.AwayTeamID}/matches?status=FINISHED&dateFrom={date60DaysAgo}&dateTo={mainMatch.MatchDate.AddDays(-1):yyyy-MM-dd}&limit=10&competitions={competitionID}";

            Dictionary<int, Match> homeLast5Matches = new();
            Dictionary<int, Match> awayLast5Matches = new();

            await getLast5MatchesAsync(homeLast5Matches, homeLast5URL);
            drawDictionary(homeLast5Matches, true);

            await getLast5MatchesAsync(awayLast5Matches, awayLast5URL);
            drawDictionary(awayLast5Matches, false);
            SaveToCSV("data.csv", mainMatch, homeLast5Matches, awayLast5Matches, true);
        }

    }
    else
    {
        // Obsługa błędu
        Console.WriteLine($"Error: {responseL.StatusCode}");
    }
}

Console.WriteLine($"Koniec pobierania :)");






void drawDictionary(Dictionary<int, Match> last5Matches, bool home)
{
    Console.WriteLine($"Ostatnie 5 meczy druzyny {(home ? "gospodarzy" : "gosci")}");
    foreach (var match in last5Matches)
    {
        Console.WriteLine(match.Value.ToString());
    }
}

void SaveToCSV(string fileName, Match matchData, Dictionary<int, Match> homeTeamMatches, Dictionary<int, Match> awayTeamMatches, bool home)
{
    using StreamWriter writer = new(fileName, true);
    if (new FileInfo(fileName).Length == 0)
    {
        writer.WriteLine("HomeTeamName,HomeTeamGoalDiff,HomeTeamPosition,AwayTeamName,AwayTeamGoalDiff,AwayTeamPosition,MatchResult,H1LastHomeTeamGoalDiffBef,H1LastHomeTeamGoalDiffAft,H1LastHomeTeamPosBef,H1LastHomeTeamPosAft,H1LastHomeTeamScoreBreak,H1LastHomeTeamScore,H1LastAwayTeamGoalDiffBef,H1LastAwayTeamGoalDiffAft,H1LastAwayTeamPosBef,H1LastAwayTeamPosBefAft,H1LastAwayTeamScoreBreak,H1LastAwayTeamScore,H1MatchResult,H2LastHomeTeamGoalDiffBef,H2LastHomeTeamGoalDiffAft,H2LastHomeTeamPosBef,H2LastHomeTeamPosAft,H2LastHomeTeamScoreBreak,H2LastHomeTeamScore,H2LastAwayTeamGoalDiffBef,H2LastAwayTeamGoalDiffAft,H2LastAwayTeamPosBef,H2LastAwayTeamPosBefAft,H2LastAwayTeamScoreBreak,H2LastAwayTeamScore,H2MatchResult,H3LastHomeTeamGoalDiffBef,H3LastHomeTeamGoalDiffAft,H3LastHomeTeamPosBef,H3LastHomeTeamPosAft,H3LastHomeTeamScoreBreak,H3LastHomeTeamScore,H3LastAwayTeamGoalDiffBef,H3LastAwayTeamGoalDiffAft,H3LastAwayTeamPosBef,H3LastAwayTeamPosBefAft,H3LastAwayTeamScoreBreak,H3LastAwayTeamScore,H3MatchResult,H4LastHomeTeamGoalDiffBef,H4LastHomeTeamGoalDiffAft,H4LastHomeTeamPosBef,H4LastHomeTeamPosAft,H4LastHomeTeamScoreBreak,H4LastHomeTeamScore,H4LastAwayTeamGoalDiffBef,H4LastAwayTeamGoalDiffAft,H4LastAwayTeamPosBef,H4LastAwayTeamPosBefAft,H4LastAwayTeamScoreBreak,H4LastAwayTeamScore,H4MatchResult,H5LastHomeTeamGoalDiffBef,H5LastHomeTeamGoalDiffAft,H5LastHomeTeamPosBef,H5LastHomeTeamPosAft,H5LastHomeTeamScoreBreak,H5LastHomeTeamScore,H5LastAwayTeamGoalDiffBef,H5LastAwayTeamGoalDiffAft,H5LastAwayTeamPosBef,H5LastAwayTeamPosBefAft,H5LastAwayTeamScoreBreak,H5LastAwayTeamScore,H5MatchResult,A1LastHomeTeamGoalDiffBef,A1LastHomeTeamGoalDiffAft,A1LastHomeTeamPosBef,A1LastHomeTeamPosAft,A1LastHomeTeamScoreBreak,A1LastHomeTeamScore,A1LastAwayTeamGoalDiffBef,A1LastAwayTeamGoalDiffAft,A1LastAwayTeamPosBef,A1LastAwayTeamPosBefAft,A1LastAwayTeamScoreBreak,A1LastAwayTeamScore,A1MatchResult,A2LastHomeTeamGoalDiffBef,A2LastHomeTeamGoalDiffAft,A2LastHomeTeamPosBef,A2LastHomeTeamPosAft,A2LastHomeTeamScoreBreak,A2LastHomeTeamScore,A2LastAwayTeamGoalDiffBef,A2LastAwayTeamGoalDiffAft,A2LastAwayTeamPosBef,A2LastAwayTeamPosBefAft,A2LastAwayTeamScoreBreak,A2LastAwayTeamScore,A2MatchResult,A3LastHomeTeamGoalDiffBef,A3LastHomeTeamGoalDiffAft,A3LastHomeTeamPosBef,A3LastHomeTeamPosAft,A3LastHomeTeamScoreBreak,A3LastHomeTeamScore,A3LastAwayTeamGoalDiffBef,A3LastAwayTeamGoalDiffAft,A3LastAwayTeamPosBef,A3LastAwayTeamPosBefAft,A3LastAwayTeamScoreBreak,A3LastAwayTeamScore,A3MatchResult,A4LastHomeTeamGoalDiffBef,A4LastHomeTeamGoalDiffAft,A4LastHomeTeamPosBef,A4LastHomeTeamPosAft,A4LastHomeTeamScoreBreak,A4LastHomeTeamScore,A4LastAwayTeamGoalDiffBef,A4LastAwayTeamGoalDiffAft,A4LastAwayTeamPosBef,A4LastAwayTeamPosBefAft,A4LastAwayTeamScoreBreak,A4LastAwayTeamScore,A4MatchResult,A5LastHomeTeamGoalDiffBef,A5LastHomeTeamGoalDiffAft,A5LastHomeTeamPosBef,A5LastHomeTeamPosAft,A5LastHomeTeamScoreBreak,A5LastHomeTeamScore,A5LastAwayTeamGoalDiffBef,A5LastAwayTeamGoalDiffAft,A5LastAwayTeamPosBef,A5LastAwayTeamPosBefAft,A5LastAwayTeamScoreBreak,A5LastAwayTeamScore,A5MatchResult");
    }
    string line = string.Join(",", $"{matchData.HomeTeamName},{matchData.HomeTeamGoalDiffBefore},{matchData.HomeTeamPositionBefore},{matchData.AwayTeamName},{matchData.AwayTeamGoalDiffBefore},{matchData.AwayTeamPositionBefore},{matchData.MatchResult}");
    string line2 = string.Join(",", homeTeamMatches.Select(match =>
        $"{match.Value.HomeTeamGoalDiffBefore},{match.Value.HomeTeamGoalDiffAfter},{match.Value.HomeTeamPositionBefore},{match.Value.HomeTeamPositionAfter},{match.Value.HomeTeamScoreBreak},{match.Value.HomeTeamScore},{match.Value.AwayTeamGoalDiffBefore},{match.Value.AwayTeamGoalDiffAfter},{match.Value.AwayTeamPositionBefore},{match.Value.AwayTeamPositionAfter},{match.Value.AwayTeamScoreBreak},{match.Value.AwayTeamScore},{match.Value.MatchResult}"
    ));
    string line3 = string.Join(",", awayTeamMatches.Select(match =>
    $"{match.Value.HomeTeamGoalDiffBefore},{match.Value.HomeTeamGoalDiffAfter},{match.Value.HomeTeamPositionBefore},{match.Value.HomeTeamPositionAfter},{match.Value.HomeTeamScoreBreak},{match.Value.HomeTeamScore},{match.Value.AwayTeamGoalDiffBefore},{match.Value.AwayTeamGoalDiffAfter},{match.Value.AwayTeamPositionBefore},{match.Value.AwayTeamPositionAfter},{match.Value.AwayTeamScoreBreak},{match.Value.AwayTeamScore},{match.Value.MatchResult}"
));

    string finalLine = string.Join(",", line, line2, line3);

    writer.WriteLine(finalLine);

    Console.WriteLine($"Dane zapisano do pliku {fileName}");
}

async Task<TeamPositions> getPositions(int homeTeamID, int awayTeamID, string season, int matchDay, int leagueID)
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
        HttpResponseMessage responseTable = await client.GetAsync(linkTable);
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

async Task getLast5MatchesAsync(Dictionary<int, Match> last5Matches, string last5URL)
{
    HttpResponseMessage responseLast5 = await client.GetAsync(last5URL);
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
            Match tmp = await GetMatch(dataLast.matches[i]);
            last5Matches.Add(tmp.MatchID, tmp);
        }

        for (int i = last5Matches.Count; i < 5; i++)
        {
            Match tmp = new(i);
            last5Matches.Add(tmp.MatchID, tmp);
        }
    }
    else
    {
        if (responseLast5.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            for (int i = last5Matches.Count; i < 5; i++)
            {
                Match tmp = new(i);
                last5Matches.Add(tmp.MatchID, tmp);
            }

        }
        else
        {
            // Obsługa błędu
            Console.WriteLine($"Error: {responseLast5.StatusCode}");
        }
    }
}

async Task<Match> GetMatch(dynamic match)
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

    var teamsPositionsBefore = await getPositions(homeTeamID, awayTeamID, startDate[..4], matchDay, leagueID);
    var teamsPositionsAfter = await getPositions(homeTeamID, awayTeamID, startDate[..4], matchDay + 1, leagueID);

    matchDate = polandDateTime;

    return new(matchID, matchStatus, homeTeamID, homeTeamName, teamsPositionsBefore.HomeTeamGoalDiff, teamsPositionsAfter.HomeTeamGoalDiff, teamsPositionsBefore.HomeTeamPosition, teamsPositionsAfter.HomeTeamPosition, homeTeamScore, homeTeamScoreBreak, awayTeamID, awayTeamName, teamsPositionsBefore.AwayTeamGoalDiff, teamsPositionsAfter.AwayTeamGoalDiff, teamsPositionsBefore.AwayTeamPosition, teamsPositionsAfter.AwayTeamPosition, awayTeamScore, awayTeamScoreBreak, matchDate, result);
}