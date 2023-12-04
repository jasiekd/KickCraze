using KickCraze.Api.Dto;
using KickCraze.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KickCraze.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpPost("GetMatches")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMatches([FromBody] GetMatchesRequestDto matchesData)
        {
            return await _matchService.GetMatches(matchesData);
        }

        [HttpPost("GetMatchInfo")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMatchInfo([FromBody] GetMatchInfoRequestDto matchData)
        {
            return await _matchService.GetMatchInfo(matchData);
        }

        [HttpPost("GetLastMatchesForTeam")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLastMatchesForTeam([FromBody] GetLastMatchesForTeamRequestDto matchData)
        {
            return await _matchService.GetLastMatchesForTeam(matchData);
        }

        [HttpPost("PredictResult")]
        [AllowAnonymous]
        public async Task<IActionResult> PredictResult([FromBody] GetMatchesRequestDto matchesData)
        {
            return await _matchService.PredictResult(matchesData);
        }
    }
}