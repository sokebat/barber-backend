using BarberApp.Application.Interface;
using BarberApp.Domain.Models;
using Microsoft.Extensions.Logging;


namespace BarberApp.Application.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ILogger<TeamService> _logger;

        public TeamService(ITeamRepository teamRepository, ILogger<TeamService> logger)
        {
            _teamRepository = teamRepository;
            _logger = logger;
        }

        public async Task<List<Team>> GetAllTeamMember()
        {
            try
            {
                return await _teamRepository.GetAllTeamMember();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all team members.");
                throw;
            }
        }

        public async Task<Team> GetTeamMemberById(int id)
        {
            try
            {
                return await _teamRepository.GetTeamMemberById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving team member with ID {id}.");
                throw;
            }
        }

        public async Task AddTeamMember(Team team)
        {
            try
            {
                await _teamRepository.AddTeamMember(team);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding a new team member.");
                throw;
            }
        }

        public async Task DeleteTeamMember(int id)
        {
            try
            {
                await _teamRepository.DeleteTeamMember(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting team member with ID {id}.");
                throw;
            }
        }

        public async Task UpdateTeamMember(Team team)
        {
            try
            {
                await _teamRepository.UpdateTeamMember(team);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating team member with ID {team.Id}.");
                throw;
            }
        }
    }
}
