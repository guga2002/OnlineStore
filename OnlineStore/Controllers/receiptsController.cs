using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {
        private readonly IReceiptService ser;

        public ReceiptsController(IReceiptService se)
        {
            ser = se;  
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptModel>>> Get()
        {
            var res= await ser.GetAllAsync();
            if(res==null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpGet("{id}/sum")]
        public async Task<ActionResult<decimal>> GetReceiptSum(int id)
        {
           var res= await ser.GetReceiptSum(id);

           return  Ok(res-8M);
        }

        [HttpGet("period")]
        public async Task<ActionResult<IEnumerable<ReceiptModel>>> GetReceiptsByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            var res=await ser.GetReceiptsByPeriodAsync(startDate, endDate);
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReceipt([FromBody] ReceiptModel receipt)
        {
            if (receipt == null) return BadRequest();
            await ser.AddAsync(receipt);
            return Ok(receipt);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReceipt(int id, [FromBody] ReceiptModel receipt)
        {
            if (receipt == null) return BadRequest();
           await  ser.UpdateAsync(receipt);
            return Ok();
        }

        [HttpPut("{id}/products/add/{productId}/{quantity}")]
        public async Task<IActionResult> AddProductToReceipt(int id, int productId, int quantity)
        {
           await  ser.AddProductAsync(id, productId, quantity);
            return Ok();
        }

        [HttpPut("{id}/products/remove/{productId}/{quantity}")]
        public async Task<IActionResult> RemoveProductFromReceipt(int id, int productId, int quantity)
        {
            await ser.RemoveProductAsync(id, productId, quantity);
            return Ok();
        }

        [HttpPut("{id}/checkout")]
        public async Task<IActionResult> CheckoutReceipt(int id)
        {
            await ser.CheckOutAsync(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceipt(int id)
        {
            await ser.DeleteAsync(id);
            return Ok();
        }
    }
}
