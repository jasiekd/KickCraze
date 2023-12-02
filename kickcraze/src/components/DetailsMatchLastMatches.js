import * as React from "react";
import "../styles/DetailsMatchLastMatches.css";
import "../styles/MainStyle.css";

export default function DetailsMatchLastMatches({ teamID, teamName }) {

  return (
    <div className="DetailsMatchLastMatches">
            Ostatnie mecze: {teamName}
    </div>
  );
}
