import * as React from "react";
import "../styles/MatchScheduleTitle.css";
import "../styles/MainStyle.css";
import { Link } from "react-router-dom";

export default function MatchScheduleTitle({ id, name, season, date }) {
  return (
    <div className="leagueS">
      <div className="title">{name}</div>
      <Link to={`/leagueTable/${season}/${id}/${date}`} className="leagueTable">
        Zobacz tabelę
      </Link>
    </div>
  );
}
