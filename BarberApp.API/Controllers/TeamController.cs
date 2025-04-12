// BarberApp.API/Controllers/TeamController.cs
using BarberApp.Application.Interface;
using BarberApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (!Guid.TryParse(id, out var guidId))
            {
                return BadRequest(new { message = "Invalid team member ID format." });
            }

            try
            {
                var teamMember = await _teamService.GetTeamMemberById(guidId);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeamMember(string id, [FromBody] Team team)
        {
            if (!Guid.TryParse(id, out var guidId) || team == null || guidId != team.Id)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeamMember(string id)
        {
            if (!Guid.TryParse(id, out var guidId))
            {
                return BadRequest(new { message = "Invalid team member ID format." });
            }

            try
            {
                await _teamService.DeleteTeamMember(guidId);
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