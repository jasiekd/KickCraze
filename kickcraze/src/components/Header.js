import * as React from "react";
import "../styles/Header.css";
import "../styles/MainStyle.css";
import LogoWithInscription from "./LogoWithInscription";
import { useNavigate } from "react-router-dom";

export default function Header() {
  const navigate = useNavigate();

  return (
    <header className="App-header">
      <LogoWithInscription onClick={() => navigate("/")} />
    </header>
  );
}
