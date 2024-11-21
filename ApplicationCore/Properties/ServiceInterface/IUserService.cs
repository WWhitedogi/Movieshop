using ApplicationCore.Models;
using System.Threading.Tasks;
using System;
namespace ApplicationCore.ServiceInterface
{
    public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel);

        /// <summary>
        /// User Login
        /// </summary>
        ///  <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<UserLoginResponseModel> ValidateUser(string email, string password);
    }

}