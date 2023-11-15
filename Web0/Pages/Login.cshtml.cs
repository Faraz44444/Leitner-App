using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Infrastructure.Exception;
using DocumentFormat.OpenXml.EMMA;

namespace WebApp_UnderTheHood.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }

        private string returnUrl = "/Index";
        public string ErrorMessage = "";
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            if (Credential.UsernameOrEmail.Empty())
            {
                ModelState.AddModelError("UsernameOrEmail", "Please Enter Your Username");
            }

            if (Credential.Password.Empty())
            {
                ModelState.AddModelError("Password", "Please Enter Your Password");
            }

            //var newHas = await Core.AppContext.Current.Services.SecurityService.SetPassword(1, "Passw0rd", "");

            var isAuthenticated =
                await Core.AppContext.Current.NewServices.SecurityService.ValidateCredentials(Credential.UsernameOrEmail,
                    Credential.Password);
            if (isAuthenticated)
            {
                try
                {
                    var user =
                        await Core.AppContext.Current.NewServices.SecurityService.GetByUsernameOrEmail(Credential
                            .UsernameOrEmail);
                    if (user.IsSystemUser)
                        return ReturnError("Incorrect Username or Password");

                    var claimsIdentity = await user.GenerateNewClaimsIdentityAsync("MyCookieAuth", isPersistent: true);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);

                }
                catch(FeedbackException ex)
                {
                    return ReturnError(ex.Message);
                }
                return RedirectToPage(returnUrl);
            }

            return ReturnError("Incorrect Username or Password");

            //verify the credential
            //if (Credential.UsernameOrEmail == "admin" && Credential.Password == "password")
            //{
            //    //create the credentials
            //    var claims = new List<Claim>
            //        {
            //            new Claim(ClaimTypes.Name, "admin"),
            //            new Claim(ClaimTypes.Email, "admin@mywebsite.com"),
            //            new Claim("Department", "HR"),
            //            new Claim("Admin", "true"),
            //            // the value for this claim really does not matter because for the related policy we are only checking whether such a claim exists or not
            //            new Claim("Manager", "true"),
            //            new Claim("EmploymentDate", "2021-05-01")
            //        };

            //    var identity = new ClaimsIdentity(claims, authenticationType: "MyCookieAuth");

            //    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            //    var authProperties = new AuthenticationProperties()
            //    {
            //        IsPersistent = Credential.RememberMe
            //    };


            //    //generating the security context
            //    //serialize the claimsprincipal into a string and encrypt that and save it as a cookie in the HttpContext object
            //    await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, authProperties);

            //    return RedirectToPage("/Index");
            //}

            //return Page();
        }
        private IActionResult ReturnError(string errorMessage = "Sorry, an error has occured!")
        {
            ModelState.AddModelError("ErrorMessage", errorMessage);
            return Page();
        }
    }

    public class Credential
    {
        [Required]
        [Display(Name = "User Name")]
        public string UsernameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
    }


}
