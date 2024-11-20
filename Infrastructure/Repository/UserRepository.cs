using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class UserRepository:EfRepository<User>,IUserRepository
{
    public UserRepository(MovieShopDbContext dbContext):base(dbContext)
    {

    }
    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _dbContext.Users.Include(u => u.Roles)
                                        .FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

}