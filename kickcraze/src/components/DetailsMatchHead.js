import * as React from "react";
import "../styles/DetailsMatchHead.css";
import "../styles/MainStyle.css";
import BigTeamLogoWithInscription from "./BigTeamLogoWithInscription";

export default function DetailsMatchHead({
  date,
  homeImage,
  homeName,
  homeScore,
  awayImage,
  awayName,
  awayScore,
}) {
  return (
    <div id="detailsMatchHead">
      <div className="teamInfo">
        <BigTeamLogoWithInscription image={homeImage} inscription={homeName} />
      </div>
      <div id="gameInfo">
        <div id="date">{date}</div>
        <div id="score">
          {homeScore}-{awayScore}
        </div>
      </div>
      <div className="teamInfo">
        <BigTeamLogoWithInscription image={awayImage} inscription={awayName} />
      </div>
    </div>
  );
}
