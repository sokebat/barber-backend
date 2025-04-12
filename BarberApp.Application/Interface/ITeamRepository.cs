﻿using BarberApp.Domain.Models;


namespace BarberApp.Application.Interface
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetAllTeamMember();
        Task<Team> GetTeamMemberById(Guid id);
        Task AddTeamMember(Team team);
        Task DeleteTeamMember(Guid id);
        Task UpdateTeamMember(Team team);
    }
}
