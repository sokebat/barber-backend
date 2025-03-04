using BarberApp.Domain;
 

namespace BarberApp.Application.Interface
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetAllTeamMember();
        Task<Team> GetTeamMemberById(int id);
        Task AddTeamMember(Team team);
        Task DeleteTeamMember(int id);
        Task UpdateTeamMember(Team team);
    }
}
