using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi_AspNet_Core;

[Authorize]
[ApiController]
[Route("webapi-aspnet-core/")]
public class SuppliersController : ControllerBase
{
    private readonly ApiDbContext _context;

    public SuppliersController(ApiDbContext context)
    {
        _context = context;
    }

    // GET: api/Suppleirs
    [AllowAnonymous]
    [HttpGet]
    [Route("suppleirs")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
    {
        if (_context.Supliers == null) return NotFound();

        return await _context.Supliers.ToListAsync();
    }

    // GET: api/Suppleirs/{id}
    [HttpGet]
    [Route("suppleirs/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Supplier>> GetSuppleir(int id)
    {
        if (_context.Supliers == null) return NotFound();

        var suppleir = await _context.Supliers.FindAsync(id);

        if (suppleir == null) return NotFound();

        return suppleir;
    }

    // POST: api/Suppleirs
    [HttpPost]
    [Route("suppleirs")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Supplier>> PostSupplier(Supplier supplier)
    {
        if (_context.Supliers == null) return Problem("Error creating the supplier, contact support.");

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        _context.Supliers.Add(supplier);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSuppleir), new { id = supplier.Id }, supplier);
    }

    // PUT: api/Suppleirs/{id}
    [HttpPut]
    [Route("suppleirs/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Supplier>> PutSupplier(int id, Supplier supplier)
    {
        if (id != supplier.Id) return BadRequest();
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        //_context.Products.Update(product);
        _context.Entry(supplier).State = EntityState.Modified; // not necessary!

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DBConcurrencyException ex)
        {
            if (!SupliersExists(id)) return NotFound();
            else throw new Exception(ex.Message);
        }

        return NoContent();
    }

    // DELETE: api/Suppleirs/{id}
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("suppleirs/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Supplier>> DeleteSupplier(int id)
    {
        if (_context.Supliers == null) return NotFound();
        var supplier = await _context.Supliers.FindAsync(id);
        if (supplier == null) return NotFound();

        _context.Supliers.Remove(supplier);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool SupliersExists(int id)
    {
        return _context.Supliers.Any(e => e.Id == id);
    }
}




