import { backendHostname } from "./HostName.js";
import axios from "axios";

export default class MatchService {
  static async GetMatches(requestData) {
    try {
      const response = await axios.post(backendHostname + "Match/GetMatches", {
        leagueID: requestData.leagueID,
        date: requestData.date,
      });
      return response;
    } catch (error) {
      return error.response;
    }
  }
  static async GetMatchInfo(requestData) {
    try {
      const response = await axios.post(
        backendHostname + "Match/GetMatchInfo",
        {
          matchID: requestData.matchID,
        }
      );
      return response;
    } catch (error) {
      return error.response;
    }
  }
  static async GetLastMatchesForTeam(requestData) {
    try {
      const dateString = requestData.date;
      const [datePart, timePart] = dateString.split(" ");
      const [day, month, year] = datePart.split(".");
      const [hours, minutes] = timePart.split(":");
      const date = new Date(year, month - 1, day, hours, minutes);

      const response = await axios.post(
        backendHostname + "Match/GetLastMatchesForTeam",
        {
          teamID: requestData.teamID,
          matchDate: date,
        }
      );
      return response;
    } catch (error) {
      return error.response;
    }
  }
  static async PredictResult(requestData) {
    try {
      const response = await axios.post(
        backendHostname + "Match/PredictResult",
        {
          matchID: requestData.matchID,
        }
      );
      return response;
    } catch (error) {
      return error.response;
    }
  }
}
