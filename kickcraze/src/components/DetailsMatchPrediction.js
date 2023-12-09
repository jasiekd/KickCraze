import React, { useState} from "react";
import "../styles/DetailsMatchPrediction.css";
import "../styles/MainStyle.css";
import { HashLoader } from "react-spinners";
import { PredictResult } from "../controllers/MatchController";

const override = {
  display: "block",
  margin: "0 auto",
};

export default function DetailsMatchPrediction({ matchID }) {
  const [isLoading, setIsLoading] = useState(false);
  const [predictionData, setPredictionData] = useState(undefined);

  const handlePress = async () => {
    setIsLoading(true);

    const data = await PredictResult({ matchID: matchID });

    setPredictionData(data);

    setIsLoading(false);
  };

  return (
    <div className="DetailsMatchPrediction">
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
          <div className="loadingText">
            Przeliczanie danych, proces potrwa kilka minut
          </div>
        </>
      ) : predictionData === undefined ? (
        <button onClick={handlePress}>
          Oblicz przewidywany rezultat meczu
        </button>
      ) : predictionData === null ? (
        <div className="loadingText">
          Nie udało się obliczyć przewidywanego rezultatu meczu :/
        </div>
      ) : (
        <>
          <h2 style={{whiteSpace: "nowrap",}}>Przewidywany rezultat meczu</h2>
          <div id="predictChart">
            {predictionData.map((item) => (
              <div
                style={{
                  height: 15,
                  width: `${item.value}%`,
                  backgroundColor: `${item.color}`,
                }}
                key={item.key}
              ></div>
            ))}
          </div>
          <div
            style={{
              display: "flex",
              flexDirection: "row",
              justifyContent: "space-between",
              width: "90%",
              alignItems: "center",
              marginTop: 10,
            }}
          >
            {predictionData.map((item) => (
              <div
                style={{
                  display: "flex",
                  flexDirection: "row",
                  alignItems: "center",
                  whiteSpace: "nowrap",
                }}
                key={item.key}
              >
                <div
                  style={{
                    height: 15,
                    width: 15,
                    backgroundColor: `${item.color}`,
                    marginRight: 5,
                    whiteSpace: "nowrap",
                  }}
                ></div>
                <div>
                  {item.key} {item.value}%
                </div>
              </div>
            ))}
          </div>
        </>
      )}
    </div>
  );
}
