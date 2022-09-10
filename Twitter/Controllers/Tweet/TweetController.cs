using Common.Enums;
using Common.Models;
using Library.Twitter.Business;
using Library.Twitter.Enums;
using Microsoft.AspNetCore.Mvc;
using Supabase.Gotrue;
using TweetInfo =  Library.Twitter.Models.Tweet;

namespace Twitter.Controllers.Tweet
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        private readonly ITweet _twitter;
        public TweetController(ITweet twitter)
        {
            _twitter = twitter;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(TweetInfo tweet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _twitter.Create(tweet);
                    switch (result)
                    {
                        case TweetActionResponse.Success:
                            return Ok($"Tweet successfully created.");

                        case TweetActionResponse.UnAuthorized:
                            return Unauthorized($"You are not authorized to tweet.");

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

        [HttpDelete]
        [Route("delete/{tweetId}")]
        public async Task<IActionResult> Delete(int tweetId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _twitter.Delete(tweetId);
                    switch (result)
                    {
                        case TweetActionResponse.Success:
                            return Ok($"Tweet successfully deleted.");

                        case TweetActionResponse.UnAuthorized:
                            return Unauthorized($"You are not authorized to delete.");

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

        [HttpGet]
        [Route("read/{tweetId}")]
        public async Task<IActionResult> Read(int tweetId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _twitter.Read(tweetId);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NotFound();
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

        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> Update(TweetInfo tweet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _twitter.Update(tweet);
                    switch (result)
                    {
                        case TweetActionResponse.Success:
                            return Ok($"Tweet successfully updated.");

                        case TweetActionResponse.UnAuthorized:
                            return Unauthorized($"You are not authorized to update.");

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
    }
}
