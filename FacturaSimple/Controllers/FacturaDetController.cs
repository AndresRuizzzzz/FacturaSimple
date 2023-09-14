using Microsoft.AspNetCore.Mvc;
using FacturaSimple.Context;
using FacturaSimple.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace FacturaSimple.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaDetController : Controller
    {
        private readonly AppDbContext _context;
        public FacturaDetController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("add")]

        public async Task<ActionResult<FacturaDet>> AddFacturaDet(FacturaDet facturadet)
        {
            _context.FacturaDetalle.Add(facturadet);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        [HttpGet]
        [Route("list")]

        public async Task<ActionResult<IEnumerable<FacturaDet>>> ListFacturaDet()
        {
            return await _context.FacturaDetalle.ToListAsync();
        }

        [HttpPut]
        [Route("edit/{id}")]

        public async Task<IActionResult> EditFacturaDet(int id, FacturaDet facturadet)
        {

            if (id != facturadet.FacturaDetId)
            {
                return BadRequest();
            }


            _context.Entry(facturadet).State = EntityState.Modified;

            await _context.SaveChangesAsync();


            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]

        public async Task<IActionResult> DeleteFacturaDet(int id)
        {
            var facturadet = await _context.FacturaDetalle.FindAsync(id);

            if (facturadet == null)
            {
                return NotFound();
            }

            _context.FacturaDetalle.Remove(facturadet);

            await _context.SaveChangesAsync();

            return NoContent();

        }

    }
}
