using Football.API.Models;
using Football.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Football.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService= playerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> Get()
        {
            return Ok(await _playerService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _playerService.GetByIdAsync(id);
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Player player)
        {
            var response = (Player)(await _playerService.PostAsync(player));
            if (response == null)
                return NotFound();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Player player)
        {
            var response = (Player)(await _playerService.UpdateAsync(id, player));
            if (response == null)
                return NotFound();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Id }, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _playerService.DeleteAsync(id));
        }
    }
}
