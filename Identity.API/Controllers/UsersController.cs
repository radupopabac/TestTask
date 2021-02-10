using AutoMapper;
using Identity.Core.Entities;
using Identity.Core.Interfaces.Services;
using Identity.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManagerService _userManager;
        private readonly IMapper mapper;

        public UsersController(IUserManagerService userManager, IMapper mapper)
        {
            _userManager = userManager;
            this.mapper = mapper;
        }

        /// <summary>
        /// Creates a new user into the platform
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> CreateAsync([FromBody] UserRequestModel request)
        {
            var errors = await _userManager.CreateAsync(mapper.Map<UserEntity>(request), request.Password);
            if (errors == null) return Ok();
            return new BadRequestObjectResult(errors);
        }

        /// <summary>
        /// Deletes the current user
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteAsync()
        {
            var errors = await _userManager.DeleteAsync();
            if (errors == null) return Ok();
            return new BadRequestObjectResult(errors);
        }
    }
}