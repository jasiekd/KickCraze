import * as React from "react";
import Header from "../components/Header";
import "../styles/MainStyle.css";
import { useParams } from "react-router-dom";
import DetailsMatch from "../components/DetailsMatch";
import Footer from "../components/Footer";

export default function DetailsMatchPage() {
  const { id } = useParams();
  return (
    <>
      <Header />
      <main className="content">
        <DetailsMatch id={id} />
      </main>
      <Footer />
    </>
  );
}
