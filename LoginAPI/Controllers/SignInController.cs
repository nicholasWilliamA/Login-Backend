using LoginAPI.Model;
using LoginAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoginAPI.Controllers
{
    [Route("api/v1/signin")]
    public class SignInController : Controller
    {
        private readonly SignInService SignInServices;
        public SignInController(SignInService signInServices)
        {
            this.SignInServices = signInServices;
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> SignIn([FromBody] SignInModel param)
        {
            var token = await SignInServices.SignInAccount(param);
            if (token == null)
            {
                return BadRequest("Wrong username or password!");
            }
            else if (token != null && token.Token == "Wrong password")
            {
                return BadRequest("Wrong password for username " + param.Username);
            }
            return Ok(token);
        }
    }
}
