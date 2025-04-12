using BarberApp.Application.Interface;
using BarberApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarberApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var teamMembers = await _teamService.GetAllTeamMember();
                if (teamMembers == null || !teamMembers.Any())
                {
                    return NotFound(new { message = "No team members found." });
                }
                return Ok(teamMembers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new

                {
                    message = "An error occurred while retrieving team members.",
                    error = ex.Message
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid team member ID." });
            }

            try
            {
                var teamMember = await _teamService.GetTeamMemberById(id);
                if (teamMember == null)
                {
                    return NotFound(new { message = $"Team member with ID {id} not found." });
                }
                return Ok(teamMember);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"An error occurred while retrieving the team member with ID {id}.",
                    error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTeamMember([FromBody] Team team)
        {
            if (team == null)
            {
                return BadRequest(new { message = "Invalid team member data." });
            }

            if (string.IsNullOrWhiteSpace(team.Name) || string.IsNullOrWhiteSpace(team.Specialty) ||
                string.IsNullOrWhiteSpace(team.Description))
            {
                return BadRequest(new { message = "Name, specialty, and description are required." });
            }

            try
            {
                await _teamService.AddTeamMember(team);
                return CreatedAtAction(nameof(GetById), new { id = team.Id }, team);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = "Duplicate entry detected.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while adding the team member.",
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTeamMember(int id, [FromBody] Team team)
        {
            if (id <= 0 || team == null || id != team.Id)
            {
                return BadRequest(new { message = "Team member data is invalid or ID mismatch." });
            }

            try
            {
                await _teamService.UpdateTeamMember(team);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while updating the team member.",
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTeamMember(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid team member ID." });
            }

            try
            {
                await _teamService.DeleteTeamMember(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while deleting the team member.",
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
    }
}