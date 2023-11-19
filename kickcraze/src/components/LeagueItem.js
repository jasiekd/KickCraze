import * as React from "react";
import "../styles/LeagueItem.css";
import "../styles/MainStyle.css";
import logoLeague from "../images/leaguelogo.png";
import LeagueLogo from "./LeagueLogo";
export default function LeagueItem({ id, name, photo, active, onClick }) {
  const leagueClass = active ? "active" : "unactive";
  return (
    <div className="league" onClick={onClick}>
      <div className={leagueClass}></div>
      <LeagueLogo image={photo} />
      <div className="title">{name}</div>
    </div>
  );
}
