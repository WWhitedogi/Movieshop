using System.Threading.Tasks;
using ApplicationCore.Entities; // 添加引用，解决 User 类型未找到的问题

namespace ApplicationCore.RepositoryInterface
{
    public interface IUserRepository
    {
        // 根据 Email 获取用户
        Task<User> GetUserByEmail(string email);
    }
}
