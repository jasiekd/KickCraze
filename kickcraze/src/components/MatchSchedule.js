import * as React from "react";
import "../styles/MatchSchedule.css";
import "../styles/MainStyle.css";
import MatchScheduleTitle from "./MatchScheduleTitle";
import MatchScheduleInfo from "./MatchScheduleInfo";
import homeImage from "../images/homeTeamLogo.png";
import awayImage from "../images/awayTeamLogo.png";
import { useState, useEffect } from "react";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import "react-datepicker/dist/react-datepicker-cssmodules.css";
import "date-fns-tz";
import { registerLocale, setDefaultLocale } from "react-datepicker";
import pl from "date-fns/locale/pl";
import { GetMatches } from "../controllers/MatchController";

registerLocale("pl", pl);
setDefaultLocale("pl");

const customDatePickerStyles = {
  calendarContainer: {
    background: "#121212",
    color: "white",
  },
};

export default function MatchSchedule() {
  const [selectedDate, setSelectedDate] = useState(new Date());
  const [isLoading, setIsLoading] = useState(true);
  const [matchesData, setMatchesData] = useState([]);

  useEffect(() => {
    FetchData();
  }, []);

  const FetchData = async () => {
    setIsLoading(true);

    const data = await GetMatches({ leagueID: "ALL", date: "2023-11-11" });
    console.log(data);
    if (data !== null) {
      setMatchesData(data);
    }

    setIsLoading(false);
  };
  const handleDateChange = (date) => {
    setSelectedDate(date);
  };
  const data = [
    {
      id: 1,
      name: "Premier League",
      matches: [
        {
          id: 7,
          homeName: "Everton",
          homeImage: homeImage,
          homeScore: 0,
          awayName: "Arsenal",
          awayImage: awayImage,
          awayScore: 1,
          hour: "17:30",
          status: "Koniec",
        },
      ],
    },
    {
      id: 2,
      name: "LaLiga",
      matches: [
        {
          id: 8,
          homeName: "Atletico Madryt",
          homeImage: homeImage,
          homeScore: 1,
          awayName: "Real Sociedad",
          awayImage: awayImage,
          awayScore: 0,
          hour: "19:00",
          status: "W trakcie",
        },
        {
          id: 9,
          homeName: "FC Barcelona",
          homeImage: homeImage,
          homeScore: "-",
          awayName: "Real Madryt",
          awayImage: awayImage,
          awayScore: "-",
          hour: "20:45",
          status: "",
        },
      ],
    },
  ];

  return (
    <div id="matchSchedule">
      <div id="headerSchedule">
        <div id="title">Mecze</div>
        <DatePicker
          selected={selectedDate}
          onChange={(date) => handleDateChange(date)}
          locale="pl"
          customStyles={customDatePickerStyles}
        />
      </div>

      {matchesData.map((league) => (
        <>
          <MatchScheduleTitle
            key={league.leagueID}
            id={league.leagueID}
            name={league.leagueName}
          />
          {league.matches.map((match) => (
            <MatchScheduleInfo key={match.matchID} match={match} />
          ))}
        </>
      ))}
    </div>
  );
}
