import LeagueService from "../services/LeagueService";

export const GetLeagues = async () => {
    const response = await LeagueService.GetLeagues();
    if(response === undefined){
      return null;
    }
    if (response.status === 200) {
      return response.data;
    } else if (response.status === 400) {

      return null;
    }
  };
  