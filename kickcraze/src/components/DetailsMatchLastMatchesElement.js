import * as React from "react";
import "../styles/DetailsMatchLastMatchesElement.css";
import SmallTeamLogo from "./SmallTeamLogo";
import "../styles/MainStyle.css";

export default function DetailsMatchLastMatchesElement({ match }) {
  // eslint-disable-next-line
  const [matchDate, matchTime] = match.matchDate.split("T");
  const [year,month,day] = matchDate.split("-");

  return (
    <div className="DetailsMatchLastMatchesElement">
      <div className="date">{day}.{month}.{year}</div>
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
