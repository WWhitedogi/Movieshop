using ApplicationCore.Entities;

namespace ApplicationCore.ServiceInterface
{
    public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel);
        Task<> ValidateUser(string email, string password);
    }
}