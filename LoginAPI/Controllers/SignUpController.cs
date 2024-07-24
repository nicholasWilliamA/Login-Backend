using LoginAPI.Model;
using LoginAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoginAPI.Controllers
{
    [Route("api/v1/signup")]
    public class SignUpController : Controller
    {
        private readonly SignUpService SignUpServices;
        public SignUpController(SignUpService signUpServices)
        {
            this.SignUpServices = signUpServices;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateNewAccount([FromBody] SignUpModel param)
        {
            try
            {
                var result = await SignUpServices.CreateNewAccount(param);
                if(result == "Username has been taken!")
                {
                    return BadRequest(result);
                }
                return Ok(result);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
