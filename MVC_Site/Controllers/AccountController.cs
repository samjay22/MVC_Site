using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Site.Models;
using System.Reflection.Metadata;
using System.Text.Encodings.Web;

namespace MVC_Site.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender<ApplicationUser> _emailSender;
        private readonly LinkGenerator _linkGenerator;
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
             IEmailSender<ApplicationUser> emailSender,
             LinkGenerator linkGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _linkGenerator = linkGenerator;
        }

        [HttpPost("/Register")]
        public async Task<IActionResult> Register(RegisterModel registerData)
        {
            var user = new ApplicationUser
            {
                FirstName = registerData.FirstName,
                LastName = registerData.LastName,
                Email = registerData.Email,
                DisplayName = registerData.DisplayName,
                DateOfBirth = registerData.DateOfBirth,
                UserName = registerData.Email.Split('@')[0]
            };
            var result = await _userManager.CreateAsync(user, registerData.Password); 
            if(result.Succeeded)
            {
                await _emailSender.SendConfirmationLinkAsync(user, registerData.Email, "/ConfirmEmail");
                return Redirect("/#");
            }
            else
            {
                Console.WriteLine(result.ToString());
                return View(registerData);
            }
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> Login(SignInModel signInModel){

            var result = await _signInManager.PasswordSignInAsync(signInModel.Email.Split('@')[0], signInModel.Password, true, false);
       
            return Redirect("/#");
        }
        
        [HttpGet("/ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string code, [FromQuery] string? changedEmail)
        {
            if(await _userManager.FindByIdAsync(userId) is not { } user)
            {
                return Unauthorized();
            }

            IdentityResult result;

            if (string.IsNullOrEmpty(changedEmail))
            {
                result = await _userManager.ConfirmEmailAsync(user, code);
            }
            else
            {
                // As with Identity UI, email and user name are one and the same. So when we update the email,
                // we need to update the user name.
                result = await _userManager.ChangeEmailAsync(user, changedEmail, code);

                if (result.Succeeded)
                {
                    result = await _userManager.SetUserNameAsync(user, changedEmail);
                }
            }


            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            return Ok();
        }

        [HttpGet("/Register")]
        public async Task<IActionResult> Register()
        {
            await Task.CompletedTask;
            return View();
        }
    }
}
