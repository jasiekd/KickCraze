import * as React from "react";
import "../styles/BigTeamLogoWithInscription.css";
import "../styles/MainStyle.css";

export default function BigTeamLogoWithInscription({ image, inscription }) {
  return (
    <div className="bigTeamLogoWithInscription">
      <div className="image">
        <img className="logo" src={image} alt="Big Team Logo" />
      </div>
      <div className="inscription">{inscription}</div>
    </div>
  );
}
