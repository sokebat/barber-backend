using BarberApp.Application.Interface;
using BarberApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberApp.Persistence.Repository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly BarberDbContext _context;
        private readonly ILogger<TeamRepository> _logger;

        public TeamRepository(BarberDbContext context, ILogger<TeamRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Team>> GetAllTeamMember()
        {
            try
            {
                return await _context.Team.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all team members.");
                throw new Exception("Database error occurred while retrieving team members.");
            }
        }

        public async Task<Team> GetTeamMemberById(int id)
        {
            try
            {
                var teamMember = await _context.Team.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
                if (teamMember == null)
                    throw new KeyNotFoundException($"Team member with ID {id} not found.");

                return teamMember;
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
                if (team == null)
                    throw new ArgumentNullException(nameof(team), "Team member cannot be null.");

                await _context.Team.AddAsync(team);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding a new team member.");
                throw new Exception("An error occurred while adding the team member.");
            }
        }

        public async Task DeleteTeamMember(int id)
        {
            try
            {
                var teamMember = await _context.Team.FindAsync(id);
                if (teamMember == null)
                    throw new KeyNotFoundException("Team member not found.");

                _context.Team.Remove(teamMember);
                await _context.SaveChangesAsync();
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
                if (team == null)
                    throw new ArgumentNullException(nameof(team), "Team member cannot be null.");

                var existingMember = await _context.Team.FindAsync(team.Id);
                if (existingMember == null)
                    throw new KeyNotFoundException("Team member not found.");

                _context.Entry(existingMember).CurrentValues.SetValues(team);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating team member with ID {team.Id}.");
                throw;
            }
        }
    }
}
