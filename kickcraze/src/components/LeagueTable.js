import * as React from "react";
import "../styles/LeagueTable.css";
import "../styles/MainStyle.css";
import LeagueTableItem from "./LeagueTableItem";
import { GetLeagueTable } from "../controllers/LeagueController";
import { useState, useEffect } from "react";
import { HashLoader } from "react-spinners";

const override = {
  display: "block",
  margin: "0 auto",
};

export default function LeagueTable({ leagueSeason, leagueID, date }) {
  const [isLoading, setIsLoading] = useState(true);
  const [leagueTableData, setLeagueTableData] = useState({});

  useEffect(() => {
    FetchData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const FetchData = async () => {
    setIsLoading(true);

    const data = await GetLeagueTable({
      leagueID: leagueID,
      leagueSeason: leagueSeason,
      date: date,
    });
    console.log(data);
    if (data !== null) {
      setLeagueTableData(data);
    }

    setIsLoading(false);
  };

  const convertDateFormat = (originalDate) => {
    const [month, day, year] = originalDate.split("-");

    const newDateFormat = `${day}/${month}/${year}`;

    return newDateFormat;
  };

  return (
    <div id="leagueTable">
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
          <div className="loadingText">Ładowanie tabeli</div>
        </>
      ) : Object.keys(leagueTableData).length === 0 ? (
        <div className="loadingText">Błąd przy pobieraniu tabeli</div>
      ) : (
        <>
          <div id="leagueTitle">
            Tabela {leagueTableData.nameLeague}
            <div id="date">{convertDateFormat(date)}</div>
          </div>
          <div id="table">
            <div id="tableHeader">
              <div title="Pozycja w tabeli" className="positionH">
                Lp.
              </div>
              <div className="nameH">Drużyna</div>
              <div title="Rozegrane mecze" className="statsH">
                M
              </div>
              <div title="Wygrane mecze" className="statsH">
                W
              </div>
              <div title="Zremisowane mecze" className="statsH">
                R
              </div>
              <div title="Przegrane mecze" className="statsH">
                P
              </div>
              <div title="Strzelone bramki:Stracone bramki" className="statsH">
                B
              </div>
              <div
                title="Różnica między strzelonymi a straconymi bramkami"
                className="statsH"
              >
                RB
              </div>
              <div title="Punkty" className="statsH">
                PKT
              </div>
            </div>
            {leagueTableData.leagueTable.map((team) => (
              <LeagueTableItem
                key={team.position}
                position={team.position}
                image={team.teamCrestURL}
                name={team.teamName}
                played={team.played}
                win={team.won}
                draw={team.draw}
                lose={team.lost}
                goals={`${team.goalsFor}:${team.goalsAgainst}`}
                diffGoals={team.goalDifference}
                points={team.points}
              />
            ))}
          </div>
        </>
      )}
    </div>
  );
}
