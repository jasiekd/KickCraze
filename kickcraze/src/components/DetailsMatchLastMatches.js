import * as React from "react";
import { useState, useEffect } from "react";
import "../styles/DetailsMatchLastMatches.css";
import "../styles/MainStyle.css";
import DetailsMatchLastMatchesElement from "./DetailsMatchLastMatchesElement";
import { GetLastMatchesForTeam } from "../controllers/MatchController";
import { HashLoader } from "react-spinners";

const override = {
  display: "block",
  margin: "0 auto",
};

export default function DetailsMatchLastMatches({ teamID, teamName, date }) {
  const [isLoading, setIsLoading] = useState(true);
  const [lastMatchesData, setLastMatchesData] = useState({});

  useEffect(() => {
    FetchData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const FetchData = async () => {
    setIsLoading(true);

    const data = await GetLastMatchesForTeam({ teamID: teamID, date: date });

    if (data !== null) {
      setLastMatchesData(data);
    }

    setIsLoading(false);
  };

  return (
    <div className="DetailsMatchLastMatches">
      <div className="lastMatchesHeader">Ostatnie mecze: {teamName}</div>

      {isLoading ? (
        <>
          <HashLoader
            loading={isLoading}
            cssOverride={override}
            aria-label="Loading Spinner"
            data-testid="loader"
            color="#ffffff"
            size={50}
          />
          <div className="loadingText">Ładowanie ostatnich meczów</div>
        </>
      ) : Object.keys(lastMatchesData).length === 0 ? (
        <div className="loadingText">Błąd przy pobieraniu ostatnich meczów</div>
      ) : (
        lastMatchesData.lastMatches.map((match) => (
          <DetailsMatchLastMatchesElement key={match.matchID} match={match} />
        ))
      )}
    </div>
  );
}
