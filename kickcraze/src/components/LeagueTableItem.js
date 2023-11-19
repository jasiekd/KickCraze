import * as React from "react";
import "../styles/LeagueTableItem.css";
import "../styles/MainStyle.css";
import SmallTeamLogo from "./SmallTeamLogo";

export default function LeagueTableItem({ position, image, name, played, win, draw, lose, goals, diffGoals, points }) {
  return (
    <div className="team" >
        <div className="position">{position}.</div>
        <div className="name"><SmallTeamLogo image={image}/>{name}</div>
        <div className="stats">{played}</div>
        <div className="stats">{win}</div>
        <div className="stats">{draw}</div>
        <div className="stats">{lose}</div>
        <div className="stats">{goals}</div>
        <div className="stats">{diffGoals}</div>
        <div className="stats">{points}</div>
    </div>
  );
}
