using DevIO.Api.ViewModels;
using DevIO.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevIO.Api.Controllers
{
    [Route("api")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AuthController(INotificador notificador,
                               SignInManager<IdentityUser> signInManager,
                               UserManager<IdentityUser> userManager) : base(notificador)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return CustomResponse(HttpStatusCode.Created, registerUser);
            }

            foreach (var error in result.Errors)
                NotificarErro(error.Description);


            return CustomResponse(HttpStatusCode.Created, registerUser);
        }
        [HttpPost("entrar")]
        public async Task<ActionResult> login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);
            if (result.Succeeded) return CustomResponse(HttpStatusCode.OK, loginUser);
            if (result.IsLockedOut)
            {
                NotificarErro("");
                return CustomResponse(HttpStatusCode.TooManyRequests, loginUser);
            }
            return CustomResponse(HttpStatusCode.Unauthorized, loginUser);
        }
    }
}
