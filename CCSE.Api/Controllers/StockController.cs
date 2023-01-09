using CCSE.Api.Domain;
using CCSE.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CCSE.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        StockService stockService = new StockService();

        [HttpPost("addStock")]
        public bool AddStock(Stock stock)
        {
            
            return stockService.SaveStock(stock);
        }

        [HttpGet("getStock")]
        public async Task<bool> GetStock()
        {
            return true;
        }
    }
}
