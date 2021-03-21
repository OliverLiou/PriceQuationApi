using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using PriceQuationApi.Model;
using PriceQuationApi.Services;
using PriceQuationApi.Helpers;
using PriceQuationApi.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace PriceQuationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtHelpers _jwt;
        private readonly UserManager<AdminUser> _userManager;
        private IPasswordHasher<AdminUser> _passwordHasher;
        private IUserService _service;
        private IMapper _mapper;

        public UserController(JwtHelpers jwt, UserManager<AdminUser> userManager, IPasswordHasher<AdminUser> passwordHasher,IUserService userService,IMapper mapper)
        {
            _jwt = jwt;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _service = userService;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(VUser vUser)
        {
            try
            {
                var userName = vUser.UserName;
                var userPassword = vUser.Password;
                var user = await _userManager.FindByNameAsync(userName);

                if(user != null)
                {
                    if(user.PasswordHash != null && 
                       _passwordHasher.VerifyHashedPassword(user,user.PasswordHash,userPassword) == PasswordVerificationResult.Success)
                    {
                        VToken vToken = new VToken()
                        {
                            AccessToken =  _jwt.GenerateToken("userName", 180),
                            RefreshToken = _jwt.GenerateToken("userName", 600)
                        };
                        return Ok(vToken);
                    }
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
            
        }

        [HttpGet("GetUserInfo")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            try
            {
                var adminUser = await _userManager.FindByNameAsync(User.Identity.Name);
                if (adminUser != null)
                {
                    return Ok(adminUser);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPost("CreateUser")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<ActionResult<AdminUser>> CreateUser(VUser vUser)
        {
            try
            {
                AdminUser user = _mapper.Map<AdminUser>(vUser);
                await _service.CreateUser(user);
                VUser tempUser = _mapper.Map<VUser>(user);
                return CreatedAtAction(
                    nameof(GetUser),
                    new { UserId = user.Id },
                    tempUser
                );
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("DeleteUser/{userId}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                var user = await _service.GetUser(userId);
                if (user == null)
                {
                    return NotFound();
                }
                await _service.DeleteUser(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPut("UpdateUser/{userId}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> UpdateUser(string userId, VUser vUser)
        {
            try
            {
                if (userId != vUser.Id)
                {
                    return BadRequest(ModelState);
                }
                var user = await _service.GetUser(userId);
                if (user == null)
                {
                    return NotFound();
                }

                // user = _mapper.Map<AdminUser>(vUser); //不能這樣做
                user.UserName = vUser.UserName;
                if (vUser.Password != null)
                    user.PasswordHash = _passwordHasher.HashPassword(user, vUser.Password);
                user.Email = vUser.Email;
                user.DepartmentId = vUser.DepartmentId;
                user.EmployeeId = vUser.EmployeeId;
                user.EmployeeName = vUser.EmployeeName;
                await _service.UpdateUser(user, vUser.RoleNames);

                return NoContent();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpGet("GetUser/{userId}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<ActionResult<VUser>> GetUser(string userId)
        {
            try
            {
                var user = await _service.GetUser(userId);
                if (user == null)
                {
                    return NotFound();
                }
                VUser vUser = _mapper.Map<VUser>(user);
                return vUser;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}