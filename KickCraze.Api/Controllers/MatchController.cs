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
            var result = _matchService.GetMatches(matchesData);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }
    }
}