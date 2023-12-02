using KickCraze.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace KickCraze.Api.Services
{
    public interface IMatchService
    {
        Task<IActionResult> GetMatches(GetMatchesRequestDto matchesData);
        Task<IActionResult> GetMatchInfo(GetMatchInfoRequestDto matchData);
        Task<IActionResult> PredictResult(GetMatchesRequestDto matchesData);
    }
}
