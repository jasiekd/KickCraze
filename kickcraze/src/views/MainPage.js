import * as React from "react";
import Header from "../components/Header";
import Leagues from "../components/Leagues";
import MatchSchedule from "../components/MatchSchedule";
import "../styles/MainStyle.css";
import { useState } from "react";

export default function MainPage() {
  const [activeLeagueID, setActiveLeagueID] = useState("ALL");
  return (
    <>
      <Header />
      <main className="content">
        <Leagues setActiveLeagueID={setActiveLeagueID} />
        <MatchSchedule activeLeagueID={activeLeagueID} />
      </main>
    </>
  );
}
