import * as React from "react";
import "../styles/DetailsMatch.css";
import "../styles/MainStyle.css";
import homeImage from "../images/homeTeamLogo2.png";
import awayImage from "../images/awayTeamLogo2.png";
import DetailsMatchHead from "./DetailsMatchHead";

export default function DetailsMatch({ id }) {
  const data = {
    id: 1,
    date: "31.10.2023 13:39",
    homeImage: homeImage,
    homeName: "Everton",
    homeScore: 0,
    awayImage: awayImage,
    awayName: "Arsenal",
    awayScore: 1,
  };

  return (
    <div id="detailsMatch">
      <div id="detailsTitle">Szczegóły meczu</div>
      <DetailsMatchHead
        date={data.date}
        homeImage={data.homeImage}
        homeName={data.homeName}
        homeScore={data.homeScore}
        awayImage={data.awayImage}
        awayName={data.awayName}
        awayScore={data.awayScore}
      />
    </div>
  );
}
