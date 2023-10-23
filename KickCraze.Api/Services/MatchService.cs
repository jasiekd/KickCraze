using KickCraze.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace KickCraze.Api.Services
{
    public class MatchService : IMatchService
    {
        public MatchService() { }

        public async Task<IActionResult> GetMatches(GetMatchesRequestDto matchesData)
        {
            return new OkResult();
        }
    }
}
