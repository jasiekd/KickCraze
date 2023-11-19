import * as React from "react";
import "../styles/Leagues.css";
import "../styles/MainStyle.css";
import LeagueItem from "./LeagueItem";
import { GetLeagues } from "../controllers/LeagueController";
import { useState, useEffect } from "react";

export default function Leagues() {
  const [isLoading, setIsLoading] = useState(true);
  const [leagueData, setLeagueData] = useState([ ]);

  useEffect(() => {
    FetchData();
  }, []);

  const FetchData = async () => {
    setIsLoading(true);

    const data = await GetLeagues();

    if (data !== null) {
      setLeagueData(data.leagues);
    }

    setIsLoading(false);
  };

  const handleTileClick = (id) => {
    const updatedData = leagueData.map((element) => {
      if (element.leagueID === id) {
        return { ...element, active: true };
      } else {
        return { ...element, active: false };
      }
    });

    setLeagueData(updatedData);
  };

  return (
    <div id="leagues">
      <div id="title">Dostępne Ligi</div>
      {isLoading ? <div>Ładowanie lig</div> : leagueData.map((element) => (
        <LeagueItem
          key={element.leagueID}
          id={element.leagueID}
          name={element.leagueName}
          photo={element.leagueEmblemURL}
          active={element.active}
          onClick={() => handleTileClick(element.leagueID)}
        />
      ))}
      
    </div>
  );
}
