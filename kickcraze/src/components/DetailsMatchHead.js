import * as React from "react";
import "../styles/DetailsMatchHead.css";
import "../styles/MainStyle.css";
import BigTeamLogoWithInscription from "./BigTeamLogoWithInscription";
import { HashLoader } from "react-spinners";

const override = {
  display: "block",
  margin: "0 auto",
};

export default function DetailsMatchHead({
  date,
  homeImage,
  homeName,
  homeScore,
  homeScoreBreak,
  awayImage,
  awayName,
  awayScore,
  awayScoreBreak,
  isLoading,
}) {
  return (
    <div
      id="detailsMatchHead"
      style={
        isLoading ? { flexDirection: "column", marginBottom: "20px" } : null
      }
    >
      {isLoading ? (
        <>
          <HashLoader
            loading={isLoading}
            cssOverride={override}
            aria-label="Loading Spinner"
            data-testid="loader"
            color="#ffffff"
            size={50}
          />
          <div className="loadingText">Ładowanie szczegółów meczu</div>
        </>
      ) : (
        <>
          <div className="teamInfo">
            <BigTeamLogoWithInscription
              image={homeImage}
              inscription={homeName}
            />
          </div>
          <div id="gameInfo">
            <div id="date">{date}</div>
            <div id="score">
              <div>
                {homeScore}-{awayScore}
              </div>
              <div style={{ fontSize: "smaller", color: "gray" }}>
                ({homeScoreBreak}-{awayScoreBreak})
              </div>
            </div>
          </div>
          <div className="teamInfo">
            <BigTeamLogoWithInscription
              image={awayImage}
              inscription={awayName}
            />
          </div>
        </>
      )}
    </div>
  );
}
