using BarberApp.Application.Interface;
using BarberApp.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        // ✅ Get all team members (barbers)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var teamMembers = await _teamService.GetAllTeamMember();

                if (teamMembers == null || !teamMembers.Any())
                {
                    return NotFound(new { message = "No team members found." });  // 404 - Not Found
                }

                return Ok(teamMembers);  // 200 - OK
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving team members.",
                    error = ex.Message
                });  // 500 - Internal Server Error
            }
        }

        // ✅ Get a team member by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid team member ID." });  // 400 - Bad Request
            }

            try
            {
                var teamMember = await _teamService.GetTeamMemberById(id);
                if (teamMember == null)
                {
                    return NotFound(new { message = $"Team member with ID {id} not found." });  // 404 - Not Found
                }
                return Ok(teamMember);  // 200 - OK
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

        // ✅ Add a new team member (barber)
        [HttpPost]
        public async Task<IActionResult> AddTeamMember([FromBody] Team team)
        {
            if (team == null)
            {
                return BadRequest(new { message = "Invalid team member data." });  // 400 - Bad Request
            }

            if (string.IsNullOrWhiteSpace(team.Name) || string.IsNullOrWhiteSpace(team.Specialty))
            {
                return BadRequest(new { message = "Team member name and specialty are required." });  // 400 - Bad Request
            }

            try
            {
                await _teamService.AddTeamMember(team);
                return CreatedAtAction(nameof(GetById), new { id = team.Id }, team);  // 201 - Created
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });  // 400 - Bad Request
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = "Duplicate entry detected.", error = ex.Message });  // 409 - Conflict
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while adding the team member.",
                    error = ex.InnerException?.Message ?? ex.Message
                });  // 500 - Internal Server Error
            }
        }

        // ✅ Update an existing team member
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeamMember(int id, [FromBody] Team team)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid team member ID." });  // 400 - Bad Request
            }

            if (team == null || id != team.Id)
            {
                return BadRequest(new { message = "Team member data is invalid or ID mismatch." });  // 400 - Bad Request
            }

            try
            {
                await _teamService.UpdateTeamMember(team);
                return NoContent();  // 204 - No Content (successful update)
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });  // 404 - Not Found
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while updating the team member.",
                    error = ex.InnerException?.Message ?? ex.Message
                });  // 500 - Internal Server Error
            }
        }

        // ✅ Delete a team member
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeamMember(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid team member ID." });  // 400 - Bad Request
            }

            try
            {
                await _teamService.DeleteTeamMember(id);
                return NoContent();  // 204 - No Content (successful delete)
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });  // 404 - Not Found
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while deleting the team member.",
                    error = ex.InnerException?.Message ?? ex.Message
                });  // 500 - Internal Server Error
            }
        }
    }
}
