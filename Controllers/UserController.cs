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

namespace PriceQuationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtHelpers _jwt;

        public UserController(JwtHelpers jwt)
        {
            _jwt = jwt;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login()
        {
            try
            {
                VToken vToken = new VToken()
                {
                    AccessToken =  _jwt.GenerateToken("userName", 180),
                    RefreshToken = _jwt.GenerateToken("userName", 600)
                };
                return Ok(vToken);
            }
            catch (System.Exception)
            {
                
                throw;
            }
            
        }
    }
}