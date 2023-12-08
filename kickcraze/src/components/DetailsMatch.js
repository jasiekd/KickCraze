import * as React from "react";
import { useState, useEffect } from "react";
import "../styles/DetailsMatch.css";
import "../styles/MainStyle.css";
import DetailsMatchHead from "./DetailsMatchHead";
import DetailsMatchPrediction from "./DetailsMatchPrediction";
import DetailsMatchLastMatches from "./DetailsMatchLastMatches";
import { GetMatchInfo } from "../controllers/MatchController";

export default function DetailsMatch({ id }) {
  const [isLoading, setIsLoading] = useState(true);
  const [matchData, setMatchData] = useState({});

  useEffect(() => {
    FetchData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const FetchData = async () => {
    setIsLoading(true);

    const data = await GetMatchInfo({ matchID: id });
    if (data !== null) {
      setMatchData(data);
    }

    setIsLoading(false);
  };

  return (
    <div id="detailsMatch">
      <div id="detailsTitle">Szczegóły meczu</div>
      <DetailsMatchHead
        date={matchData.matchDate}
        homeImage={matchData.homeTeamCrestURL}
        homeName={matchData.homeTeamName}
        homeScore={matchData.homeTeamScore}
        homeScoreBreak={matchData.homeTeamScoreBreak}
        awayImage={matchData.awayTeamCrestURL}
        awayName={matchData.awayTeamName}
        awayScore={matchData.awayTeamScore}
        awayScoreBreak={matchData.awayTeamScoreBreak}
        isLoading={isLoading}
      />

      {isLoading ? null : matchData.matchDate === undefined ? null : (
        <>
          <DetailsMatchPrediction matchID={id} />
          <div id="lastMatches">
            <DetailsMatchLastMatches
              teamID={matchData.homeTeamID}
              teamName={matchData.homeTeamName}
              date={matchData.matchDate}
            />
            <DetailsMatchLastMatches
              teamID={matchData.awayTeamID}
              teamName={matchData.awayTeamName}
              date={matchData.matchDate}
            />
          </div>
        </>
      )}
    </div>
  );
}
