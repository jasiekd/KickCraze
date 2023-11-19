import { backendHostname } from "./HostName.js";
import axios from "axios";

export default class MatchService {
    static async GetMatches(requestData){
        try{
            const response = await axios.post(backendHostname+"Match/GetMatches",{
                leagueID: requestData.leagueID,
                date: requestData.date
            });
            return response;
        }catch(error){
            return error.response;
        }
    }
  
}