import * as React from "react";
import Header from "../components/Header";
import Leagues from "../components/Leagues";
import MatchSchedule from "../components/MatchSchedule";
import "../styles/MainStyle.css";

export default function MainPage() {
  return (
    <>
      <Header />
      <main className="content">
        <Leagues />
        <MatchSchedule />
      </main>
    </>
  );
}
