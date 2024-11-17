using ExchangeCoin_Back.Data.Entities;
using ExchangeCoin_Back.Data.Models_DTOs;
using ExchangeCoin_Back.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExchangeCoin_Back.Controllers
{
    public class CoinController
    {
        [Route("api/[controller]")]
        [ApiController]
        [Authorize]
        public class MonedaController : ControllerBase
        {
            private CoinService _CoinServices;

            public MonedaController(CoinService CoinService)
            {
                _CoinServices = CoinService;

            }


            [HttpGet("GetCoinsForAdmin")]
            public IActionResult GetCoins()
            {
                return Ok(_CoinServices.GetCoins());
            }

            [HttpPost("GetCoinsByID")]
            public IActionResult GetCoinsByID(int CoinID)
            {
                return Ok(_CoinServices.GetCoinsById(CoinID));
            }

            [HttpPut("Exchange")]

            public IActionResult Exchange([FromBody] ExchangeResultDTO request)
            {
                int UserID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

                ExchangeResultDTO result = new ExchangeResultDTO();

                result.cointochangeName = request.cointochangeName;
                result.coinchangedName = request.coinchangedName;
                result.amount = request.amount;
                result.result = (_CoinServices.Exchange(UserID, request.cointochangeName, request.coinchangedName, request.amount));

                return Ok(result);


            }

            [HttpGet("GetCoinList")]
            public IActionResult GetCoinList()
            {
                CoinListDTO list = new CoinListDTO();
                list.coins = _CoinServices.GetCoinsList();

                return Ok(list);


            }

            [HttpPost]
            public IActionResult CreateCoin(CreateandUpdateCoinDTO CreateCoinDTO)
            {
                int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
                _CoinServices.CreateCoin(CreateCoinDTO);
                return Created("Created", CreateCoinDTO);
            }

            [HttpPut]

            public IActionResult UpdateCoin(CreateandUpdateCoinDTO dto, int CoinId)
            {
                _CoinServices.UpdateCoin(dto, CoinId);
                return NoContent();
            }


        }
    }
}
