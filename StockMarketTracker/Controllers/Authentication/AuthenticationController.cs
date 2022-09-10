using Common.Business.Authentication;
using Common.Enums;
using Common.Extension;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace StockMarketTracker.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuth _auth;
        public AuthenticationController(IAuth auth)
        {
            _auth = auth;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _auth.Login(user);
                    switch (result)
                    {
                        case LoginResponse.Valid:
                            SetSession(user);
                            return Ok($"{user.Username} Welcome!!!");

                        case LoginResponse.IncorrectPassword:
                            return Unauthorized($"Given password is invalid.");

                        case LoginResponse.IncorrectUsername:
                            return Unauthorized($"Who the hell are you?");

                        default:
                            return Ok();
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _auth.Register(user);
                    switch (result)
                    {
                        case RegisterResponse.Valid:
                            SetSession(user);
                            return Ok($"{user.Username} Welcome!!!");

                        case RegisterResponse.UserAlreadyExist:
                            return Conflict($"User already exists.");

                        default:
                            return Ok();
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpContext.Session.Remove("userInfo");
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private bool SetSession(User user)
        {
            if (HttpContext != null)
                HttpContext.Session.Set<User>("userInfo", user);
            return true;

        }
    }
}
