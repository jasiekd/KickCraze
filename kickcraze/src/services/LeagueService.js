import { backendHostname } from "./HostName.js";
import axios from "axios";

export default class LeagueService {
    static async GetLeagues(){
        try{
            const response = await axios.get(backendHostname+"League/GetLeagues")
            return response;
        }catch(error){
            return error.response;
        }
    }
  
}