import React from "react";
import MainPage from "./../views/MainPage.js";
import LeagueTablePage from "./../views/LeagueTablePage.js";
import DetailsMatchPage from "../views/DetailsMatchPage.js";
import NotFoundPage from "../views/NotFoundPage.js";
import { createBrowserRouter } from "react-router-dom";

const router = createBrowserRouter([
  {
    path: "/",
    element: <MainPage />,
  },
  {
    path: "/leagueTable/:leagueSeason/:leagueID/:date",
    element: <LeagueTablePage />,
  },
  {
    path: "/detailsMatch/:id",
    element: <DetailsMatchPage />,
  },
  {
    path: "*",
    element: <NotFoundPage />,
  },
]);

export default router;
