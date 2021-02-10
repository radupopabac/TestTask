using Identity.Core.Interfaces.Services;
using Identity.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthorizationService authorizationService;

        public AuthorizeController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }


        /// <summary>
        /// Returns jwt token
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Route("login")]
        public async Task<ActionResult> Login(EmailAndPasswordModel loginDto)
        {
            var jwt = await authorizationService.AuthorizeAsync(loginDto);
            return Ok(new { token = jwt });
        }
    }
}
