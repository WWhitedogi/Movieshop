using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryInterface
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        // check if user exists in the db 
        Task<User> GetUserByEmail(string email);
    }
}
