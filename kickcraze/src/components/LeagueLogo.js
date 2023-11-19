import * as React from "react";
import "../styles/LeagueLogo.css";
import "../styles/MainStyle.css";

export default function LeagueLogo({ image }) {
  return (
    <div className="leagueLogo">
      <img className="leagueEmblem" src={image} alt="League Logo" />
    </div>
  );
}
