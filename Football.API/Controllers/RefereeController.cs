using Football.API.Models;
using Football.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Football.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class RefereeController : ControllerBase
    {
        private readonly IRefereeService _refereeService;

        public RefereeController(IRefereeService refereeService)
        {
            _refereeService = refereeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> Get()
        {
            return Ok(await _refereeService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _refereeService.GetByIdAsync(id);
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Referee referee)
        {
            var response = (Referee)(await _refereeService.PostAsync(referee));
            if (response == null)
                return NotFound();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Referee referee)
        {
            var response = (Referee)(await _refereeService.UpdateAsync(id, referee));
            if (response == null)
                return NotFound();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Id }, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _refereeService.DeleteAsync(id));
        }
    }
}
