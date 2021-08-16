using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Entities;
using UserService.Models.Dtos.Role;
using UserService.Service.Role;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationRolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;


        public ApplicationRolesController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        // GET: api/ApplicationRoles
        /// <summary>
        /// Returns list of all roles
        /// </summary>
        /// <returns>List of roles</returns>
        /// <response code="200">Returns the list</response>
        ///<response code="401">Unauthorized user</response>
        /// <response code="500">Error on the server</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
        {
            try
            {
                var roles = _roleService.GetRoles();
                return Ok(_mapper.Map<List<RoleDto>>(roles));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        // GET: api/ApplicationRoles/5
        /// <summary>
        /// Returns role with given id
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <returns>Role with roleId</returns>
        ///<response code="200">Returns the role</response>
        /// <response code="404">Role with roleId is not found</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="500">Error on the server</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetApplicationRole(Guid id)
        {
            try
            {
                var role = _roleService.GetRoleByRoleId(id);
                if (role == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<RoleDto>(role));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        // PUT: api/ApplicationRoles/5
        /// <summary>
        /// Updates a role
        /// </summary>
        /// <param name="id">Role Id</param>
        /// <param name="applicationRole">Model of a role in the system</param>
        /// <returns>Confirmation of update</returns>
        /// <response code="200">Returns updated role</response>
        /// <response code="400">Role with roleId is not found</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="409">Unique value violation</response>
        /// <response code="500">Error on the server while updating</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationRole(Guid id, RoleUpdateDto applicationRole)
        {
            //TODO: convert dto to entity and change update in service to have 2 args
            try
            {
                ApplicationRole roleWithId = _roleService.GetRoleByRoleId(id);
                if (roleWithId == null)
                {
                    return NotFound();
                }
                var applicationRoleUpdate = _mapper.Map<ApplicationRole>(applicationRole);
                _roleService.UpdateRole(roleWithId, applicationRoleUpdate);
                return Ok(_mapper.Map<RoleDto>(roleWithId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        // POST: api/ApplicationRoles
        /// <summary>
        /// Creates a new role
        /// </summary>
        /// <param name="applicationRole">Model of role</param>
        /// <returns>Confirmation of the creation of role</returns>
        /// <response code="200">Returns the created role</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="409">Unique value violation</response>
        /// <response code="500">There was an error on the server</response>
        [HttpPost]
        public async Task<ActionResult<ApplicationRole>> PostApplicationRole(RoleCreateDto applicationRole)
        {
            try
            {
                var newRole = _mapper.Map<ApplicationRole>(applicationRole);
                var createdRole = _roleService.CreateRole(newRole);
                return Ok(_mapper.Map<RoleDto>(createdRole));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE: api/ApplicationRoles/5
        /// <summary>
        /// Soft deletes role with given id
        /// </summary>
        /// <param name="id">Role Id</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Role succesfully deleted</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Role with roleId not found</response>
        /// <response code="500">Error on the server while deleting</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationRole(Guid id)
        {
            try
            {
                var role = _roleService.GetRoleByRoleId(id);
                if (role == null)
                {
                    return NotFound();
                }
                _roleService.DeleteRole(role);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
