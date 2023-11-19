import * as React from "react";
import "../styles/MatchSchedule.css";
import "../styles/MainStyle.css";
import MatchScheduleTitle from "./MatchScheduleTitle";
import MatchScheduleInfo from "./MatchScheduleInfo";
import { useState, useEffect, forwardRef } from "react";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import "react-datepicker/dist/react-datepicker-cssmodules.css";
import "date-fns-tz";
import { registerLocale, setDefaultLocale } from "react-datepicker";
import pl from "date-fns/locale/pl";
import { GetMatches } from "../controllers/MatchController";
import { HashLoader } from "react-spinners";

registerLocale("pl", pl);
setDefaultLocale("pl");

const override = {
  display: "block",
  margin: "0 auto",
};

export default function MatchSchedule({ activeLeagueID }) {
  const [selectedDate, setSelectedDate] = useState(new Date());
  const [isLoading, setIsLoading] = useState(true);
  const [matchesData, setMatchesData] = useState([]);

  const ExampleCustomInput = forwardRef(({ value, onClick }, ref) => (
    <div id="date" onClick={onClick} ref={ref}>
      {value}
    </div>
  ));

  const FetchData = async () => {
    setIsLoading(true);

    const data = await GetMatches({
      leagueID: activeLeagueID,
      date: selectedDate,
    });
    console.log(data);
    if (data !== null) {
      setMatchesData(data);
    }

    setIsLoading(false);
  };
  const handleDateChange = (date) => {
    setSelectedDate(date);
  };

  useEffect(() => {
    FetchData();
  }, [selectedDate, activeLeagueID]);

  return (
    <div id="matchSchedule">
      <div id="headerSchedule">
        <div id="title">Mecze</div>
        <DatePicker
          dateFormat="dd/MM/yyyy"
          selected={selectedDate}
          onChange={(date) => handleDateChange(date)}
          locale="pl"
          customInput={<ExampleCustomInput />}
          calendarClassName="dark-theme-calendar"
        />
      </div>
      {isLoading ? (
        <>
          <HashLoader
            loading={isLoading}
            cssOverride={override}
            aria-label="Loading Spinner"
            data-testid="loader"
            color="#ffffff"
            size={50}
          />
          <div className="loadingText">Trwa ładowanie meczy</div>
        </>
      ) : matchesData.length === 0 ? (
        <div className="loadingText">Brak meczy przy aktualnych filtrach</div>
      ) : (
        matchesData.map((league) => (
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
        ))
      )}
    </div>
  );
}
