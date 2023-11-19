using KickCraze.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace KickCraze.Api.Services
{
    public interface ILeagueService
    {
        Task<IActionResult> GetLeagueTable(GetLeagueTableRequestDto getLeagueTableRequestDto);
        Task<IActionResult> GetLeagues();
    }
}
