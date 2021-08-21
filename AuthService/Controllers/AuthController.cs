using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Entities;
using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    /// <summary>
    /// Contoller with endopoints for handling authentication of users 
    /// </summary>
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService authService;

        public AuthController(IAuthenticationService authService)
        {
            this.authService = authService;
        }
        
        /// <summary>
        /// User authentication
        /// </summary>
        /// <returns>Token if successfully authenticated</returns>
        /// <response code="200">Token</response>
        ///<response code="400">Wrong information for principal</response>
        /// <response code="500">Error on the server</response>
        [HttpPost("api/login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] Principal principal)
        {
            try
            {
                var authResponse = authService.Login(principal);
                if (authResponse.Result.Succes)
                {
                    return Ok(new LoginSuccesResponse
                    {
                        Token = authResponse.Result.Token
                    });
                }
                return BadRequest(new AuthFailResponse
                {
                    Error = authResponse.Result.Error
                });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
        
        /// <summary>
        /// Clearing info from database about once logged in user
        /// </summary>
        /// <returns>Status 200</returns>
        /// <response code="200"></response>
        ///<response code="400">Wrong value in request</response>
        /// <response code="500">Error on the server</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("api/logout")]
        public IActionResult Logout(LogoutRequest body)
        {
            try
            {
                AuthInfo authInfo = authService.GetAuthInfoByToken(body.Token);
                if (authInfo == null)
                {
                    return BadRequest("User with id not found or already logged out");
                }
                authService.Logout(authInfo.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

    }
}
