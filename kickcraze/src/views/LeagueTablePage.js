import * as React from "react";
import Header from "../components/Header";
import LeagueTable from "../components/LeagueTable";
import "../styles/MainStyle.css";
import { useParams } from "react-router-dom";
import Footer from "../components/Footer";

export default function LeagueTablePage() {
  const { leagueSeason, leagueID, date } = useParams();
  return (
    <>
      <Header />
      <main className="content">
        <LeagueTable
          leagueSeason={leagueSeason}
          leagueID={leagueID}
          date={date}
        />
      </main>
      <Footer />
    </>
  );
}
