using EarthDrive.Data;
using EarthDrive.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Security.Claims;

namespace EarthDrive.Controllers
{
    //User Authorizations Controller (Chris 22500937)
    public class UserAuthorizationsController : Controller
    {
        private readonly EarthDriveContext _context;

        public UserAuthorizationsController(EarthDriveContext context)
        {
            _context = context;
        }

        // GET: UserAuthorisations
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }

        //Logout Finction, Cleares Authentication cookie, clears users claims and sets the view to the home page.
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return (RedirectToAction("Index", "Home"));
        }

        // POST
        // Main login function for the website
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            //Checks that the input fields are not null or empty for user and assword
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                //supplies a dedicated error message to the viewdatat on the HTML page
                ViewData["LoginError"] = "Please enter both Username and Password.";
                return View();
            }

            //Awaits a responce from the Authorisation table to check if the user name exists
            var returningUser = await _context.AuthorizationTable
                .FirstOrDefaultAsync(u => u.Username == username);

            // If doesn't username exists 
            if (returningUser == null)
            {
                //supplies a dedicated error message to the viewdatat on the HTML page.
                ViewData["LoginError"] = "This account might not exist!";
                //updates the view
                return View();
            }
            //Uses PasswordHasher from the ASPNETCORE Identity function to create a encrypted password.
            var hasher = new PasswordHasher<UserAuthorization>();
            //Check the returning users hashed password to the supplied password from the returning user.
            var result = hasher.VerifyHashedPassword(returningUser, returningUser.Password, password);

            //If the password is inccorect a Failed will return and run this error.
            if (result == PasswordVerificationResult.Failed)
            {
                //Vague error to prevent known users from being discovered and allow attacks.
                ViewData["LoginError"] = "This account might not exist!";
                //updates the view
                return View();
            }
            //Usies the sign in function to sign in the returning user.
            await SignIn(returningUser.Username, returningUser.Role);

            //retunrs the now signed in user to the home page.
            return RedirectToAction("Index", "Home");

        }

        // POST: UserAuthorisations
        //User Registration system
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(string username, string password, string passwordConfirmation)
        {
            //Checks that the Username and Password fields are not empty 
            if (string.IsNullOrWhiteSpace(username) || 
                string.IsNullOrWhiteSpace(password) || 
                string.IsNullOrWhiteSpace(passwordConfirmation) )
            { 
                //supplies a dedicated error message to the viewdatat on the HTML page
                ViewData["RegistrationError"] = "Please fill in all fields";
                //updates the view
                return View();
            }

            //Check the password and and confirmation password matches
            if (password != passwordConfirmation)
            {
                //supplies a dedicated error message to the viewdatat on the HTML page
                ViewData["RegistrationError"] = "Passwords do not match";
                //updates the view
                return View();
            }

            //checks if the username exists in the database (this will result in to having double usernames
            //and might need to be looked at
            var user = await _context.AuthorizationTable
                .FirstOrDefaultAsync(u => u.Username == username);

            //If the user name exists for now we will avoid doubles to keep it simple
            if (user != null)
            {
                //supplies a dedicated error message to the viewdatat on the HTML page
                ViewData["RegistrationError"] = "User already exists";
                //updates the view
                return View();
            }
            
            //prepares the PasswordHasher From the Identity Core library
            var hasher = new PasswordHasher<UserAuthorization>();

            //creates a new user and fills in the new useres roles.
            var newUser = new UserAuthorization
            {
                Username = username,
                Role = "User"
            };
            //generates a hashed password for the newly created users and assigns it to the table
            newUser.Password = hasher.HashPassword(newUser, password);
            //adds the new user to the table
            _context.AuthorizationTable.Add(newUser);
            //waits for the new user to be saved by the table
            await _context.SaveChangesAsync();
            //signs in the user so they dont have to also sign in after account creation
            await SignIn(newUser.Username, newUser.Role);
            //Redirects the user to the home page
            return RedirectToAction("Index", "Home");

        }
        //Sing in function to simplyfy the sign in process in both login and regerstation
        private async Task SignIn(string username, string role)
        {
            //creates a claims list with the users information so they can go through the website while logged in
            var claims = new List<Claim>
            {
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, role ?? "User")
            };

            //stores the claims in the identatiy variable using a cookie authentcation scheme
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //wraps the identity in to the current authenticated user
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

    }

}
