﻿using D8M7Q2_SportEvents.Data;
using D8M7Q2_SportEvents.Entities.Dto.User;
using D8M7Q2_SportEvents.Logic.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace D8M7Q2_SportEvents.Endpoint.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserManager<AppUser> userManager;
        RoleManager<IdentityRole> roleManager;
        DtoProvider dtoProvider;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, DtoProvider dtoProvider)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.dtoProvider = dtoProvider;
        }

        [HttpGet("grantadmin/{userid}")]
        //[Authorize(Roles = "Admin")]
        public async Task GrantAdmin(string userid)
        {
            var user = await userManager.FindByIdAsync(userid);
            if (user == null)
                throw new ArgumentException("User not found!");
            await userManager.AddToRoleAsync(user, "Admin");
        }

        [HttpGet("revokeadmin/{userid}")]
        [Authorize(Roles = "Admin")]
        public async Task RevokeAdmin(string userid)
        {
            var user = await userManager.FindByIdAsync(userid);
            if (user == null)
                throw new ArgumentException("User not found!");
            await userManager.RemoveFromRoleAsync(user, "Admin");
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IEnumerable<UserViewDto> GetUsers()
        {
            return userManager.Users.Select(t =>
                dtoProvider.Mapper.Map<UserViewDto>(t)
            );
        }

        [HttpPost("register")]
        public async Task Register(UserInputDto dto)
        {
            var user = new AppUser(dto.UserName);
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            await userManager.CreateAsync(user, dto.Password);
            if (userManager.Users.Count() == 1)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserInputDto dto)
        {
            var user = await userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                throw new ArgumentException("User not found!");
            }
            else
            {
                var result = await userManager.CheckPasswordAsync(user, dto.Password);
                if (!result)
                {
                    throw new ArgumentException("Incorrect password!");
                }
                else
                {
                    var claim = new List<Claim>();
                    claim.Add(new Claim(ClaimTypes.Name, user.UserName!));
                    claim.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

                    foreach (var role in await userManager.GetRolesAsync(user))
                    {
                        claim.Add(new Claim(ClaimTypes.Role, role));
                    }

                    int expiryInMinutes = 24 * 60;
                    var token = GenerateAccessToken(claim, expiryInMinutes);

                    return Ok(new LoginResultDto()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = DateTime.Now.AddMinutes(expiryInMinutes)
                    });
                }
            }
        }

        private JwtSecurityToken GenerateAccessToken(IEnumerable<Claim>? claims, int expiryInMinutes)
        {
            var signinKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes("NagyonhosszútitkosítókulcsNagyonhosszútitkosítókulcsNagyonhosszútitkosítókulcsNagyonhosszútitkosítókulcsNagyonhosszútitkosítókulcsNagyonhosszútitkosítókulcs"));

            return new JwtSecurityToken(
                  issuer: "sportevent.com",
                  audience: "sportevent.com",
                  claims: claims?.ToArray(),
                  expires: DateTime.Now.AddMinutes(expiryInMinutes),
                  signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                );
        }
    }
}
