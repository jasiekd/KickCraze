import * as React from "react";
import "../styles/LeagueTable.css";
import "../styles/MainStyle.css";
import image from "../images/homeTeamLogo.png";
import LeagueTableItem from "./LeagueTableItem";

export default function LeagueTable({ id }) {
  const data = {
    id: 1,
    nameLeague: "Premier League",
    table: [
      {
        id: 10,
        position: 1,
        image: image,
        nameTeam: "Manchester City",
        played: 7,
        win: 6,
        draw: 0,
        lose: 1,
        goals: "17:5",
        diffGoals: 12,
        points: 18,
      },
      {
        id: 11,
        position: 2,
        image: image,
        nameTeam: "Tottenham",
        played: 7,
        win: 5,
        draw: 2,
        lose: 0,
        goals: "17:8",
        diffGoals: 9,
        points: 17,
      },
      {
        id: 12,
        position: 3,
        image: image,
        nameTeam: "Arsenal",
        played: 7,
        win: 5,
        draw: 2,
        lose: 0,
        goals: "15:6",
        diffGoals: 9,
        points: 17,
      },
    ],
  };

  return (
    <div id="leagueTable">
      <div id="leagueTitle">Tabela {data.nameLeague}</div>
      <div id="table">
        <div id="tableHeader">
          <div title="Pozycja w tabeli" className="positionH">Lp.</div>
          <div className="nameH">Drużyna</div>
          <div title="Rozegrane mecze" className="statsH">M</div>
          <div title="Wygrane mecze" className="statsH">W</div>
          <div title="Zremisowane mecze" className="statsH">R</div>
          <div title="Przegrane mecze" className="statsH">P</div>
          <div title="Strzelone bramki:Stracone bramki"className="statsH">B</div>
          <div title="Różnica między strzelonymi a straconymi bramkami" className="statsH">RB</div>
          <div title="Punkty" className="statsH">PKT</div>
        </div>
        {data.table.map((team) => (
          <LeagueTableItem
            position={team.position}
            image={team.image}
            name={team.nameTeam}
            played={team.played}
            win={team.win}
            draw={team.draw}
            lose={team.lose}
            goals={team.goals}
            diffGoals={team.diffGoals}
            points={team.points}
          />
        ))}
      </div>
    </div>
  );
}
