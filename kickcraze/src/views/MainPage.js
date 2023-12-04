import * as React from "react";
import Header from "../components/Header";
import Leagues from "../components/Leagues";
import MatchSchedule from "../components/MatchSchedule";
import "../styles/MainStyle.css";
import { useState } from "react";
import Footer from "../components/Footer";

export default function MainPage() {
  const [activeLeagueID, setActiveLeagueID] = useState("ALL");
  const [isMatchesLoading, setIsMatchesLoading] = useState(true);

  return (
    <>
      <Header />
      <main className="content">
        <Leagues
          setActiveLeagueID={setActiveLeagueID}
          isMatchesLoading={isMatchesLoading}
        />
        <MatchSchedule
          activeLeagueID={activeLeagueID}
          isLoading={isMatchesLoading}
          setIsLoading={setIsMatchesLoading}
        />
      </main>
      <Footer />
    </>
  );
}
