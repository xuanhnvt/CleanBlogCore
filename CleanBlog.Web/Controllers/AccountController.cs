using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CleanBlog.Web.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nhx.Core.Entities.Users;

namespace CleanBlog.Web.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<object> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                // using below method will generate identity cookie, I don't want
                //var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                // just check password
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded)
                {
                    return await GenerateJwtToken(user);
                }
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("AddRoleClaim")]
        public async Task<object> AddRoleClaim([FromBody] AddRoleClaimDto model)
        {
            // check if claim existed or not

            // if not, create claim record

            // 
            //throw new Exception("AddRoleClaim");
            //var user = await _userManager.FindByNameAsync(model.Email);

            var adminRole = await _roleManager.FindByNameAsync(model.Role);
            //throw new Exception(adminRole.Name);
            var roleClaim = new RoleClaim();
            roleClaim.ClaimType = model.Type;
            roleClaim.ClaimValue = model.Value;
            roleClaim.ClaimRecordId = 2;
            if (adminRole != null)
            {
                await _roleManager.AddClaimAsync(adminRole, roleClaim.ToClaim());
                //throw new Exception();
            }

            /*if (!await _userManager.IsInRoleAsync(user, adminRole.Name))
            {
                await _userManager.AddToRoleAsync(user, adminRole.Name);
            }*/

            return Ok();

            /*var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Email);

                // Get valid claims and pass them into JWT
                var claims = await GetValidClaims(user);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

                // Create the JWT security token and encode it.
                var jwt = new JwtSecurityToken(
                    issuer: _configuration["JwtIssuer"],
                    audience: _configuration["JwtIssuer"],
                    claims: claims,
                    //notBefore: _jwtOptions.NotBefore,
                    expires: expires,
                    signingCredentials: creds);
                //...
            }
            else
            {
                throw new Exception('Wrong username or password');
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                return await GenerateJwtToken(model.Email, appUser);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");*/
        }

        [HttpPost("Register")]
        public async Task<object> Register([FromBody] RegisterDto model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //await _signInManager.SignInAsync(user, false);
                //var principal = await _signInManager.ClaimsFactory.CreateAsync(user);
                return await GenerateJwtToken(user);
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

        [Authorize]
        //[ClaimRequirement()]
        [HttpGet]
        public async Task<object> Protected()
        {
            return "Protected area";
        }

        private async Task<object> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_configuration.IssuedAt).ToString(), ClaimValueTypes.Integer64)
            };

            var principal = await _signInManager.CreateUserPrincipalAsync(user);
            claims.AddRange(principal.Claims.Select(c => { return new Claim(c.Type, c.Value); }).ToList());

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            // Create the JWT security token and encode it.
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtIssuer"],
                audience: _configuration["JwtIssuer"],
                claims: claims,
                //notBefore: _configuration.NotBefore,
                expires: expires,
                signingCredentials: creds);

            return new { claims = claims, token = new JwtSecurityTokenHandler().WriteToken(token) };
        }

        private async Task<List<Claim>> GetValidClaims(User user)
        {
            IdentityOptions _options = new IdentityOptions();
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
            new Claim(_options.ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
            new Claim(_options.ClaimsIdentity.UserNameClaimType, user.UserName)
        };
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            claims.AddRange(userClaims);
            foreach (var userRole in userRoles)
            {
                //_roleManager.
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (Claim roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }
            return claims;
        }

        public class LoginDto
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }

        }

        public class AddRoleClaimDto
        {

            [Required]
            public string Email { get; set; }


            [Required]
            public string Role { get; set; }

            [Required]
            public string Type { get; set; }

            [Required]
            public string Value { get; set; }

        }

        public class RegisterDto
        {
            [Required]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
            public string Password { get; set; }
        }
    }
}