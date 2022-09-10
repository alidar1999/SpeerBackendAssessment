using Common.Enums;
using Library.StockMarketTracker.Business.Wallet;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace StockMarketTracker.Controllers.Wallet
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWallet _wallet;
        public WalletController(IWallet wallet)
        {
            _wallet = wallet;
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] Library.StockMarketTracker.Models.Wallet wallet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _wallet.UpdateWallet(wallet);
                    if (result)
                    {
                        return Ok($"Wallet updated.");
                    }
                    else
                    {
                        return NotFound($"Your wallet does not exists.");
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

        [HttpPut]
        [Route("create")]
        public async Task<IActionResult> Create()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _wallet.CreateWallet();
                    if (result)
                    {
                        return Ok($"Wallet created.");
                    }
                    else
                    {
                        return Conflict($"Your wallet already exists.");
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
        [Route("get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _wallet.GetCurrentWallet();
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NotFound($"Your wallet does not exists.");
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
