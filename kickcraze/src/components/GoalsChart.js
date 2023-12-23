import * as React from "react";
import "../styles/GoalsChart.css";
import "../styles/MainStyle.css";
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from "chart.js";
import { Pie } from "react-chartjs-2";

ChartJS.register(ArcElement, Tooltip, Legend);

export default function GoalsChart({ data, teamID, teamName }) {
  const goalsScoredMap = data.map((match) =>
    match.homeTeamID === teamID
      ? parseInt(match.homeTeamScore, 10)
      : parseInt(match.awayTeamScore, 10)
  );
  const goalsScored = goalsScoredMap.reduce(
    (acc, currentValue) => acc + currentValue,
    0
  );

  const goalsConcededMap = data.map((match) =>
    match.homeTeamID === teamID
      ? parseInt(match.awayTeamScore, 10)
      : parseInt(match.homeTeamScore, 10)
  );

  const goalsConceded = goalsConcededMap.reduce(
    (acc, currentValue) => acc + currentValue,
    0
  );

  const chartData = {
    labels: ["Gole Stracone", "Gole Strzelone"],
    datasets: [
      {
        label: "Gole",
        backgroundColor: ["rgba(255, 99, 132, 0.2)", "rgba(75,192,192,0.2)"],
        borderColor: ["rgba(255, 99, 132, 1)", "rgba(75,192,192,1)"],
        borderWidth: 1,
        hoverBackgroundColor: [
          "rgba(255, 99, 132, 0.4)",
          "rgba(75,192,192,0.4)",
        ],
        hoverBorderColor: ["rgba(255, 99, 132, 1)", "rgba(75,192,192,1)"],
        data: [goalsConceded, goalsScored],
      },
    ],
  };

  return (
    <div style={{width:"60%", display:"flex", flexDirection:"column", justifyItems:"center", alignItems:"center", alignSelf:"center", marginBottom:20}}>
      <div style={{ textAlign: "center"}}>Wykres goli dla {teamName}</div>
      <Pie data={chartData} width={300} height={300} />
    </div>
  );
}
