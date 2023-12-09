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
      <main className="content">
        <Header />
        <div className="content2">
          <DetailsMatch id={id} />
        </div>
      </main>
      <Footer />
    </>
  );
}
