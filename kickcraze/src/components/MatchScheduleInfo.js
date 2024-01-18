import * as React from "react";
import "../styles/MatchScheduleInfo.css";
import "../styles/MainStyle.css";
import SmallTeamLogo from "./SmallTeamLogo";
import { Link } from "react-router-dom";

export default function MatchScheduleInfo({ match }) {
  const dateObject = new Date(match.matchDate);
  const hours = dateObject.getHours();
  const minutes = dateObject.getMinutes();
  //console.log(match)
  return (
    <Link to={`/detailsMatch/${match.matchID}`} className="leagueI">
      <div className="time">
        {hours}:{minutes < 10 ? `0${minutes}` : minutes}
      </div>
      <div className="mainInfo">
        <div className="homeInfo">
          <div className="homeName">
            <SmallTeamLogo image={match.homeTeamCrestURL} />
            {match.homeTeamName}
          </div>
          <div className="awayName">
            {match.homeTeamScore === null ? "-" : match.homeTeamScore}
          </div>
        </div>
        <div className="awayInfo">
          <div className="homeName">
            <SmallTeamLogo image={match.awayTeamCrestURL} />
            {match.awayTeamName}
          </div>
          <div className="awayName">
            {match.awayTeamScore === null ? "-" : match.awayTeamScore}
          </div>
        </div>
      </div>
      <div className="status">{match.matchStatus}</div>
    </Link>
  );
}
