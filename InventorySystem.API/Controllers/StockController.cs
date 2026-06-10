using InventorySystem.Application.Features.Stocks.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StockController : ControllerBase
    {
        private readonly GetCurrentStockQueryHandler _getStockHandler;

        public StockController(GetCurrentStockQueryHandler getStockHandler)
        {
            _getStockHandler = getStockHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentStock()
        {
            var result = await _getStockHandler
                .Handle(new GetCurrentStockQuery());
            return Ok(result.Data);
        }
    }
}
