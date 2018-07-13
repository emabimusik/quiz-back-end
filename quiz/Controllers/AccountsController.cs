using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace quiz.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        readonly UserManager<IdentityUser> _userManager;
        readonly SignInManager<IdentityUser> _signInManager;
        public AccountsController (UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager){
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        // POST api/Accounts
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] Credentials credentials)
        {
            var user = new IdentityUser { UserName = credentials.Email, Email = credentials.Email };
            var result = await _userManager.CreateAsync(user, credentials.Password);
            if(!result.Succeeded){
                return BadRequest(result.Errors);
            }
            await _signInManager.SignInAsync(user, isPersistent: false);
            // add user id to then register user
            /**
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is the secret phrases"));
            var signigCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(signingCredentials:signigCredentials,claims:claims);
            return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
            */
            //refactored code
            return Ok(CreateToken(user));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Credentials credentials)
        {
            var result = await _signInManager.PasswordSignInAsync(credentials.Email, credentials.Password,false,false);
            if (!result.Succeeded)
                return BadRequest();
            var user = await _userManager.FindByEmailAsync(credentials.Email);

            return Ok(CreateToken(user));

        }

        string CreateToken (IdentityUser user){
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is the secret phrases"));
            var signigCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(signingCredentials: signigCredentials, claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
            
        }
    }


    public class Credentials
    {
        public string Email
        {
            get;
            set;
       }
        public string Password
        {
            get;
            set;
        } 
    }
        
}
