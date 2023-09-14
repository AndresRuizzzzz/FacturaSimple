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
    public class FacturaCabController : Controller
    {
        private readonly AppDbContext _context;
        public FacturaCabController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("add")]

        public async Task<ActionResult<FacturaCab>> AddFacturaCab(FacturaCab facturacab)
        {
            _context.FacturaCabecera.Add(facturacab);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        [HttpGet]
        [Route("list")]

        public async Task<ActionResult<IEnumerable<FacturaCab>>> ListFacturaCabs()
        {
            return await _context.FacturaCabecera.ToListAsync();
        }

        [HttpPut]
        [Route("edit/{id}")]

        public async Task<IActionResult> EditFacturaCab(int id, FacturaCab facturacab)
        {
   
            if (id != facturacab.FacturaCabId)
            {
                return BadRequest();
            }


            _context.Entry(facturacab).State = EntityState.Modified;

                await _context.SaveChangesAsync();
           

            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]

        public async Task<IActionResult> DeleteFacturaCab(int id)
        {
            var facturacab = await _context.FacturaCabecera.FindAsync(id);

            if (facturacab == null)
            {
                return NotFound();
            }

            _context.FacturaCabecera.Remove(facturacab);

            await _context.SaveChangesAsync();

            return NoContent();

        }

    }
}
