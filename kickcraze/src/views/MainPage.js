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
      <main className="content">
        <Header />
        <div className="content2">
          <Leagues
            setActiveLeagueID={setActiveLeagueID}
            isMatchesLoading={isMatchesLoading}
          />
          <MatchSchedule
            activeLeagueID={activeLeagueID}
            isLoading={isMatchesLoading}
            setIsLoading={setIsMatchesLoading}
          />
        </div>
      </main>
      <Footer />
    </>
  );
}
