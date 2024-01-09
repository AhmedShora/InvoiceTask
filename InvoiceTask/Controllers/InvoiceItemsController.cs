using InvoiceTask.Dto;
using InvoiceTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceItemsController : ControllerBase
    {
        private readonly ShaTaskContext _context;

        public InvoiceItemsController(ShaTaskContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _context.InvoiceDetails.ToListAsync();

            var itemsToReturn = items.Select(a => new InvoiceItemToReturn()
            {
                Id = a.Id,
                InvoiceHeaderId = a.InvoiceHeaderId,
                ItemCount = a.ItemCount,
                ItemName = a.ItemName,
                ItemPrice = a.ItemPrice
            });

            return Ok(itemsToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllItems(int id)
        {
            var items = await _context.InvoiceDetails.FirstOrDefaultAsync(a => a.Id == id);
            if (items == null)
                return NotFound();

            var itemsToReturn = new InvoiceItemToReturn()
            {
                Id = items.Id,
                InvoiceHeaderId = items.InvoiceHeaderId,
                ItemCount = items.ItemCount,
                ItemName = items.ItemName,
                ItemPrice = items.ItemPrice
            };
            return Ok(itemsToReturn);
        }
        [HttpGet("items/{id}")]
        public async Task<IActionResult> GetAllItemsbyHeaderId(int id)
        {
            var items = await _context.InvoiceDetails.ToListAsync();

            var itemsToReturn = items.Where(aa => aa.InvoiceHeaderId == id)
                .Select(a => new InvoiceItemToReturn()
                {
                    Id = a.Id,
                    InvoiceHeaderId = a.InvoiceHeaderId,
                    ItemCount = a.ItemCount,
                    ItemName = a.ItemName,
                    ItemPrice = a.ItemPrice
                });

            return Ok(itemsToReturn);
        }


        [HttpPost]
        public async Task<IActionResult> CreateItem(InvoiceItemToCreate invoiceItem)
        {
            if (invoiceItem == null)
                return BadRequest();

            _context.InvoiceDetails.Add(new InvoiceDetail()
            {
                InvoiceHeaderId = invoiceItem.InvoiceHeaderId,
                ItemCount = invoiceItem.ItemCount,
                ItemName = invoiceItem.ItemName,
                ItemPrice = invoiceItem.ItemPrice,
            });
            await _context.SaveChangesAsync();

            return Ok(invoiceItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditInvoiceItem(int id, [FromForm] InvoiceItemToEdit UinvoiceItem)
        {
            var invoiceItem = _context.InvoiceDetails.FirstOrDefault(b => b.Id == id);
            if (invoiceItem == null)
            {
                return NotFound();
            }
            invoiceItem.ItemName = UinvoiceItem.ItemName;
            invoiceItem.ItemPrice = UinvoiceItem.ItemPrice;
            invoiceItem.ItemCount = UinvoiceItem.ItemCount;

            await _context.SaveChangesAsync();

            return Ok(invoiceItem);
        }
    }
}
