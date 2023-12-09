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

export default function MatchSchedule({
  activeLeagueID,
  isLoading,
  setIsLoading,
}) {
  const [selectedDate, setSelectedDate] = useState(new Date());
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
    //console.log(data);
    if (data !== null) {
      setMatchesData(data);
    }

    setIsLoading(false);
  };

  const handleDateChange = (date) => {
    if (isLoading) {
      window.alert(
        "Trwa ładowanie danych. Nie możesz zmieniać daty w tym czasie."
      );
    } else {
      setSelectedDate(date);
    }
  };

  const formatDate = (date) => {
    const day = date.getDate();
    const month = date.getMonth() + 1;
    const year = date.getFullYear();

    return `${month}-${day}-${year}`;
  };

  useEffect(() => {
    FetchData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
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
          <div className="matches" key={league.leagueID}>
            <MatchScheduleTitle
              key={league.leagueID}
              id={league.leagueID}
              name={league.leagueName}
              season={league.leagueSeason}
              date={formatDate(selectedDate)}
            />
            {league.matches.map((match) => (
              <MatchScheduleInfo key={match.matchID} match={match} />
            ))}
          </div>
        ))
      )}
    </div>
  );
}
