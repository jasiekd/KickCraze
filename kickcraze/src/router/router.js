import React from "react";
import MainPage from "./../views/MainPage.js";
import LeagueTablePage from "./../views/LeagueTablePage.js";
import DetailsMatchPage from "../views/DetailsMatchPage.js";
import { createBrowserRouter } from "react-router-dom";

const router = createBrowserRouter([
  {
    path: "/",
    element: <MainPage />,
  },
  {
    path: "/leagueTable/:id",
    element: <LeagueTablePage />,
  },
  {
    path: "/detailsMatch/:id",
    element: <DetailsMatchPage />,
  },
]);

export default router;
