import * as React from "react";
import "../styles/LogoWithInscription.css";
import "../styles/MainStyle.css";
import logo from "../images/logonapis2.png";

export default function LogoWithInscription({ onClick }) {
  return (
    <div id="top">
      <img className="logo" src={logo} alt="KickCraze Logo" onClick={onClick} />
    </div>
  );
}
