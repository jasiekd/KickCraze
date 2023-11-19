import LeagueService from "../services/LeagueService";

export const GetLeagues = async () => {
  const response = await LeagueService.GetLeagues();
  if (response === undefined) {
    return null;
  }
  if (response.status === 200) {
    return response.data;
  } else if (response.status === 400) {
    return null;
  }
};

export const GetLeagueTable = async (requestData) => {
  const response = await LeagueService.GetLeagueTable(requestData);
  if (response === undefined) {
    return null;
  }
  if (response.status === 200) {
    return response.data;
  } else if (response.status === 400) {
    return null;
  }
};
