import MatchService from "../services/MatchService";

export const GetMatches = async (requestData) => {
  const response = await MatchService.GetMatches(requestData);
  if (response === undefined) {
    return null;
  }
  if (response.status === 200) {
    return response.data;
  } else if (response.status === 400) {
    return null;
  }
};

export const GetMatchInfo = async (requestData) => {
  const response = await MatchService.GetMatchInfo(requestData);
  if (response === undefined) {
    return null;
  }
  if (response.status === 200) {
    return response.data;
  } else if (response.status === 400) {
    return null;
  }
};
