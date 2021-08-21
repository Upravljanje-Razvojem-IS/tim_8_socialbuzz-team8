using AuthService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase

    {
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Contoller with endopoints for checking accounts when autenticating users 
        /// </summary>
        public AccountsController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
       
        // POST api/<ValuesController>
        // GET: api/Accounts
        /// <summary>
        /// Checks if login info are valid
        /// </summary>
        /// <returns>Information about username and password being valid</returns>
        /// <response code="200">Information for principal</response>
        ///<response code="400">Wrong information for principal</response>
        /// <response code="500">Error on the server while checking principal</response>
        [HttpPost]
        public ActionResult<CheckPrincipalResponse> CheckForAccount([FromBody] Principal request)
        {
            try
            {
                CheckPrincipalResponse response;
                ApplicationUser account = _userManager.FindByEmailAsync(request.Email).Result;
                if (account == null)
                {
                    response = new CheckPrincipalResponse();
                    response.Success = false;
                    response.Message = "Credentientials are incorect";
                    response.AccountInfo = null;
                    return BadRequest(response);
                }
                var passwordValid = _userManager.CheckPasswordAsync(account, request.Password).Result;
                if (!passwordValid)
                {
                    response = new CheckPrincipalResponse();
                    response.Success = false;
                    response.Message = "Credentientials are incorect";
                    response.AccountInfo = null;
                    return BadRequest(response);

                }
                if (!account.AccountIsActive)
                {
                    response = new CheckPrincipalResponse();
                    response.Success = false;
                    response.Message = "Account inactive";
                    response.AccountInfo = null;
                    return BadRequest(response);

                }
                List<string> roles = _userManager.GetRolesAsync(account).Result.ToList();
                AccountInfoDto accountDto = new AccountInfoDto();
                accountDto.Id = account.Id;
                accountDto.Role = roles[0];
                response = new CheckPrincipalResponse();
                response.Success = true;
                response.Message = "Successful check";
                response.AccountInfo = accountDto;
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured");
            }
        }
    }
}
