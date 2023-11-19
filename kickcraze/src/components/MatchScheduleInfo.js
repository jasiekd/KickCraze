import * as React from "react";
import "../styles/MatchScheduleInfo.css";
import "../styles/MainStyle.css";
import SmallTeamLogo from "./SmallTeamLogo";
import { useNavigate } from "react-router-dom";

export default function MatchScheduleInfo({ match }) {
  const navigate = useNavigate();
  const dateObject = new Date(match.matchDate)
  const hours = dateObject.getHours();
  const minutes = dateObject.getMinutes();
  return (
    <div className="leagueI" onClick={()=>navigate(`/detailsMatch/${match.matchID}`)}>
      <div className="time">{hours}:{minutes < 10 ? `0${minutes}`:minutes}</div>
      <div className="mainInfo">
        <div className="homeInfo">
          <div className="homeName"><SmallTeamLogo image={match.homeTeamCrestURL}/>{match.homeTeamName}</div>
          <div className="awayName">{match.homeTeamScore}</div>
        </div>
        <div className="awayInfo">
          <div className="homeName"><SmallTeamLogo image={match.awayTeamCrestURL}/>{match.awayTeamName}</div>
          <div className="awayName">{match.awayTeamScore}</div>
        </div>
      </div>
      <div className="status">{match.matchStatus}</div>
    </div>
  );
}
