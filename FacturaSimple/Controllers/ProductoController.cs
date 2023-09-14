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
    public class ProductoController : Controller
    {
        private readonly AppDbContext _context;
        public ProductoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("add")]

        public async Task<ActionResult<Producto>> AddProduct(Producto producto)
        {
            _context.Producto.Add(producto);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        [HttpGet]
        [Route("list")]

        public async Task<ActionResult<IEnumerable<Producto>>> ListProducts()
        {
            return await _context.Producto.ToListAsync();
        }

        [HttpPut]
        [Route("edit/{id}")]

        public async Task<IActionResult> EditProduct(int id, Producto producto)
        {

            var validator = new ProductoValidator();
            var validationResult = await validator.ValidateAsync(producto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }


            if (id != producto.ProductoId){
                return BadRequest();
            }


            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var producto = await _context.Producto.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            _context.Producto.Remove(producto);

            await _context.SaveChangesAsync();

            return NoContent();

        }

        private bool TaskExists(int id)
        {
            return _context.Producto.Any(e => e.ProductoId == id);
        }


    }
}
