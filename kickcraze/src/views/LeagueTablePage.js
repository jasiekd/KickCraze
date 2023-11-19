import * as React from "react";
import Header from "../components/Header";
import LeagueTable from "../components/LeagueTable";
import "../styles/MainStyle.css";
import { useParams } from "react-router-dom";

export default function LeagueTablePage() {
    const { id } = useParams();
  return (
    <>
      <Header />
      <main className="content">
        <LeagueTable id={id}/>
      </main>
    </>
  );
}
