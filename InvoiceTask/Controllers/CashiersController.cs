using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvoiceTask.Models;
using InvoiceTask.Dto;
using AutoMapper;

namespace InvoiceTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashiersController : ControllerBase
    {
        private readonly ShaTaskContext _context;
        private readonly IMapper _mapper;

        public CashiersController(ShaTaskContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Cashiers
        [HttpGet]
        public async Task<IActionResult> GetCashiers()
        {
            if (_context.Cashiers == null)
            {
                return NotFound();
            }
            var cashiers = await _context.Cashiers.Include(aa => aa.Branch).ToListAsync();

            var data = _mapper.Map<IEnumerable<CashierToReturn>>(cashiers);

            return Ok(data);
        }

        // GET: api/Cashiers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCashier(int id)
        {
            if (_context.Cashiers == null)
            {
                return NotFound();
            }
            var cashier = await _context.Cashiers.Include(aa => aa.Branch).SingleOrDefaultAsync(b => b.Id == id);

            if (cashier == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<CashierToReturn>(cashier);

            return Ok(data);
        }

        // POST: api/Cashiers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostCashier(CashierToCreate cashier)
        {
            if (cashier == null)
            {
                return BadRequest("Error");
            }
            _context.Cashiers.Add(new Cashier()
            {
                BranchId = cashier.BranchId,
                CashierName = cashier.CashierName
            });
            await _context.SaveChangesAsync();
            return Ok(cashier);
        }

        // PUT: api/Cashiers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCashier(int id, [FromForm] CashierToCreate Ucashier)
        {
            var cashier = _context.Cashiers.Include(b => b.Branch).FirstOrDefault(a => a.Id == id);
            if (cashier == null)
            {
                return BadRequest();
            }
            cashier.CashierName = Ucashier.CashierName;
            cashier.BranchId = Ucashier.BranchId;

            var data = _mapper.Map<CashierToReturn>(cashier);

            await _context.SaveChangesAsync();
            return Ok(data);
        }

        // DELETE: api/Cashiers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCashier(int id)
        {

            var cashier = await _context.Cashiers.FindAsync(id);
            if (cashier == null)
            {
                return NotFound();
            }

            _context.Cashiers.Remove(cashier);
            await _context.SaveChangesAsync();

            return Ok("Deleted");
        }


    }
}
