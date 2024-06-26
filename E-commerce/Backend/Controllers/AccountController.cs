﻿using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared_ViewModels.Account;

namespace Backend.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinManager;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signinManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginVmDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username!");

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

            var roles = await _userManager.GetRolesAsync(user);

            if (roles == null || roles.Count == 0) return Unauthorized("User has no role assigned!");

            return Ok(
                new NewUserVmDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = roles.FirstOrDefault(),
                    Token = _tokenService.CreateToken(user)
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterVmDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };


                var createUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserVmDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Role = "User",
                                Token = _tokenService.CreateToken(appUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterVmDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var createUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "Admin");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserVmDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Role = "Admin",
                                Token = _tokenService.CreateToken(appUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("users-in-role/user")]
        public async Task<IActionResult> GetAllUsersWithUserRole()
        {
            var usersInUserRole = await _userManager.GetUsersInRoleAsync("User");
            var userDtos = new List<UserVmDto>();

            foreach (var user in usersInUserRole)
            {
                userDtos.Add(new UserVmDto
                {
                    UserName = user.UserName,
                    Email = user.Email
                });
            }

            return Ok(userDtos);
        }

        [HttpGet("users-in-role/admin")]
        public async Task<IActionResult> GetAllUsersWithAdminRole()
        {
            var usersInAdminRole = await _userManager.GetUsersInRoleAsync("Admin");
            var userDtos = new List<UserVmDto>();

            foreach (var user in usersInAdminRole)
            {
                userDtos.Add(new UserVmDto
                {
                    UserName = user.UserName,
                    Email = user.Email
                });
            }

            return Ok(userDtos);
        }
    }
}
