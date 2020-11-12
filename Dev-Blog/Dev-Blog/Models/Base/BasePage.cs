using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Base
{
    public abstract class BasePage : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IEmail _email;

        [BindProperty]
        public AccountVM Input { get; set; }

        public BasePage(SignInManager<User> signInManager, UserManager<User> userManager, IEmail email)
        {
            _email = email;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public BasePage(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public BasePage(IEmail email, SignInManager<User> signInManager)
        {
            _email = email;
            _signInManager = signInManager;
        }

        public BasePage(IEmail email)
        {
            _email = email;
        }

        public BasePage(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public BasePage(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public BasePage()
        {
        }

        // LOGIN
        public async Task<IActionResult> OnPostLogin()
        {
            var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, false, false);

            // if successful login and current page is login error
            if (result.Succeeded && Request.Path.ToString() == "/Account/LoginError")
                return RedirectToPage("Index");

            // if successful login
            else if (result.Succeeded)
                Response.Redirect(Request.Path.ToString());

            // if unsuccessful
            else return RedirectToPage("/Error/Error");

            return Page();
        }

        // REGISTER
        public async Task<IActionResult> OnPostRegister()
        {
            User user = new User()
            {
                UserName = Input.UserName,
                Email = Input.Email
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            // if successful login and current page is login error
            if (result.Succeeded && Request.Path.ToString() == "/Account/LoginError")
                return RedirectToPage("Index");

            // if successful
            else if (result.Succeeded)
            {
                await _email.Welcome(user.Email);
                Claim userName = new Claim("UserName", Input.UserName);
                Claim email = new Claim("Email", Input.Email);
                await _userManager.AddClaimAsync(user, userName);
                await _userManager.AddClaimAsync(user, email);
                await _signInManager.SignInAsync(user, isPersistent: false);

                Response.Redirect(Request.Path.ToString());
            }

            // if unsuccessful
            else return RedirectToPage("/Error/Error");

            return Page();
        }
    }
}