import * as React from "react";
import "../styles/DetailsMatchLastMatchesElement.css";
import SmallTeamLogo from "./SmallTeamLogo";
import "../styles/MainStyle.css";

export default function DetailsMatchLastMatchesElement({ match }) {
  const [matchDate, matchTime] = match.matchDate.split(" ");
  return (
    <div className="DetailsMatchLastMatchesElement">
      <div className="date">{matchDate}</div>
      <div className="mainInfo">
        <div className="homeInfo">
          <div className="homeName">
            <SmallTeamLogo image={match.homeTeamCrestURL} />
            {match.homeTeamName}
          </div>
          <div className="awayName">{match.homeTeamScore}</div>
        </div>
        <div className="awayInfo">
          <div className="homeName">
            <SmallTeamLogo image={match.awayTeamCrestURL} />
            {match.awayTeamName}
          </div>
          <div className="awayName">{match.awayTeamScore}</div>
        </div>
      </div>
    </div>
  );
}
