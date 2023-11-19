using KickCraze.Api.Dto;
using KickCraze.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KickCraze.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeagueController : ControllerBase
    {
        private readonly ILeagueService _leagueService;

        public LeagueController(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        [HttpGet("GetLeagues")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLeagueTable()
        {
            return await _leagueService.GetLeagues();
        }

        [HttpPost("GetLeagueTable")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLeagueTable([FromBody]  GetLeagueTableRequestDto getLeagueTableRequestDto)
        {
            return await _leagueService.GetLeagueTable(getLeagueTableRequestDto);
        }
    }
}