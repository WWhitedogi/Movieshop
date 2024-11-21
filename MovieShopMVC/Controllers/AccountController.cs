using ApplicationCore.ServiceInterface;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MovieShopMVC.Controllers{
    public class AccountController : Controller{
        private readonly IUserService _userService;
        public AccountController(IUserService userService){
            _userService=userService;
        }
    //register page
        [HttpGet]
        public IActionResult Register(){
            //返回注册视图
            return View();
        }
        //处理注册表单提交
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel model){
            //检查表单数据是否合法（比如必填字段有没有填写）
            var user=await _userService.RegisterUser(model);
            return RedirectToAction("Login");
        }
    //login page
        [HttpGet]
        public IActionResult Login()
        {
            // 返回登录视图
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestModel model){
            var user= await _userService.ValidateUser(model.Email,
            model.Password);
            //cookie based authentication
            //create the claims that we are going to store in cookie
            //system.secutrity.claims
            var claims=new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Surname,user.LastName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.DateOfBirth,user.DateOfBirth.ToString()),
            };
            //create the identity object that will use the above claims
            //microsoft.AspNetCore.Authentication.Cookies;
            var claimsIdentity=new ClaimsIdentity(
                claims,CookieAuthenticationDefaults.AuthenticationScheme);
            //cretae the cookie
            //HttpContext
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
            //redirect to hoome page
            return LocalRedirect("~/");
        }
    }

}