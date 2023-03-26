using Football.API.Models;
using Football.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Football.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _matchService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _matchService.GetByIdAsync(id);
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Match match)
        {
            var response = (Match)(await _matchService.PostAsync(match));
            if (response == null)
                return NotFound();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Match match)
        {
            var response = (Match)(await _matchService.UpdateAsync(id, match));
            if (response == null)
                return NotFound();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Id }, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _matchService.DeleteAsync(id));
        }
    }
}
