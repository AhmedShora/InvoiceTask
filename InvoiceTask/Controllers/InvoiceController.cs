using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvoiceTask.Models;
using InvoiceTask.Dto;
using System.Reflection.PortableExecutable;

namespace InvoiceTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly ShaTaskContext _context;

        public InvoiceController(ShaTaskContext context)
        {
            _context = context;
        }

        // GET: api/Invoice
        [HttpGet]
        public async Task<IActionResult> GetInvoiceHeaders()
        {
            if (_context.InvoiceHeaders == null)
            {
                return NotFound();
            }
            var headers = await _context.InvoiceHeaders.Include(d => d.InvoiceDetails).ToListAsync();
            IEnumerable<InvoiceToReturn> invoices = headers.Select(a => new InvoiceToReturn
            {
                Id = a.Id,
                CustomerName = a.CustomerName,
                Invoicedate = a.Invoicedate,
                InvoiceDetails = a.InvoiceDetails.Select(a => new InvoiceItemToReturn
                {
                    Id = a.Id,
                    ItemCount = a.ItemCount,
                    ItemName = a.ItemName,
                    ItemPrice = a.ItemPrice,
                    InvoiceHeaderId = a.InvoiceHeaderId
                }).ToList()

            }).ToList();

            return Ok(invoices);
        }

        // GET: api/Invoice/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceHeader(int id)
        {
            if (_context.InvoiceHeaders == null)
            {
                return NotFound();
            }
            var invoiceHeader = await _context.InvoiceHeaders
                .Include(b => b.InvoiceDetails).FirstOrDefaultAsync(a => a.Id == id);

            if (invoiceHeader == null)
            {
                return NotFound();
            }
            var invoiceToReturn = new InvoiceToReturn()
            {
                Id = invoiceHeader.Id,
                CustomerName = invoiceHeader.CustomerName,
                Invoicedate = invoiceHeader.Invoicedate,
                InvoiceDetails = invoiceHeader.InvoiceDetails.Select(a => new InvoiceItemToReturn
                {
                    Id = a.Id,
                    ItemCount = a.ItemCount,
                    ItemName = a.ItemName,
                    ItemPrice = a.ItemPrice,
                    InvoiceHeaderId = a.InvoiceHeaderId
                }).ToList()
            };
            return Ok(invoiceToReturn);
        }

        // PUT: api/Invoice/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceHeader(int id, [FromForm] InvoiceToCreate UinvoiceHeader)
        {
            var invoiceheader = _context.InvoiceHeaders.FirstOrDefault(b => b.Id == id);
            if (invoiceheader == null)
            {
                return NotFound();
            }

            invoiceheader.CustomerName = UinvoiceHeader.CustomerName;
            invoiceheader.Invoicedate = UinvoiceHeader.Invoicedate;
            //_context.Entry(invoiceHeader).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(invoiceheader);
        }

        // POST: api/Invoice
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostInvoiceHeader(InvoiceToCreate invoiceHeader)
        {
            if (invoiceHeader == null)
            {
                return BadRequest("Error");
            }
            _context.InvoiceHeaders.Add(new InvoiceHeader()
            {
                CustomerName = invoiceHeader.CustomerName,
               // Invoicedate = invoiceHeader.Invoicedate,
               BranchId=invoiceHeader.BranchId,
               CashierId=invoiceHeader.CashierId
            });
            await _context.SaveChangesAsync();

            return Ok(invoiceHeader);
        }

        // DELETE: api/Invoice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceHeader(long id)
        {
            if (_context.InvoiceHeaders == null)
            {
                return NotFound();
            }
            var invoiceHeader = await _context.InvoiceHeaders.FindAsync(id);
            if (invoiceHeader == null)
            {
                return NotFound();
            }

            _context.InvoiceHeaders.Remove(invoiceHeader);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
