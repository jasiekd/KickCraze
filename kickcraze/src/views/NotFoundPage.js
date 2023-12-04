import * as React from "react";
import Header from "../components/Header";
import "../styles/MainStyle.css";
import "../styles/NotFoundPage.css";
import { Link } from "react-router-dom";
import Footer from "../components/Footer";

export default function NotFoundPage() {
  return (
    <>
      <Header />
      <main className="content">
        <div id="notFound">
          <p>Przepraszam, strona której szukasz nie istnieje</p>
          <Link to="/">Przejdź do strony głównej</Link>
        </div>
      </main>
      <Footer />
    </>
  );
}
