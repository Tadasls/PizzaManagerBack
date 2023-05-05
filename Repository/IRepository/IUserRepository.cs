using CompetitionEventsManager.Models;
using CompetitionEventsManager.Models.Dto;
using CompetitionEventsManager.Services.IServices;
using System.Linq.Expressions;

namespace CompetitionEventsManager.Repository.IRepository
{
    public interface IUserRepository
    {
        bool Exist(string userName);
        Task<LocalUser> GetAsync(Expression<Func<LocalUser, bool>> filter, bool tracked = true);
        Task<bool> IsRegisteredAsync(int userId);
        Task<bool> IsUniqueUserAsync(string username);
        Task<int> RegisterAsync(LocalUser user);
        bool TryLogin(string userName, string password, out LocalUser? user);
    }
}