using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Net;


namespace WebApi_AspNet_Core;

[Authorize]
[ApiController]
[Route("webapi-aspnet-core/")]
public class SuppliersController : BasicController
{
    private readonly ApiDbContext _context;
    private readonly ISuppliersServices _services;

    public SuppliersController(ApiDbContext context, ISuppliersServices services)
    {
        _context = context;
        _services = services;
    }


    // GET: /Suppliers
    /// <remarks>
    /// This route is used to search for all Suppliers registered in the database.
    /// </remarks>
    /// <summary>
    /// Check all Suppliers
    /// </summary>
    /// <response code="200">Returns all Suppliers.</response>
    /// <response code="500">Returns the error message indicating an unforeseen failure in the services. Contact the service provider.</response>
    [AllowAnonymous]
    [HttpGet]
    [Route("/Suppliers")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetSuppliers()
    {
        try
        {
            List<SupplierDto> SupplierDtos = _services.GetSuppliers();
            return CreateResponse(HttpStatusCode.OK, SupplierDtos);
        }
        catch (System.InvalidOperationException ex)
        { return StatusCode(500, ex.Message); }
        catch (System.ArgumentNullException ex)
        { return StatusCode(500, ex.Message); }
        catch (Exception ex)
        { return StatusCode(500, ex.Message); }
    }

    // GET: /Suppliers/{id}
    [AllowAnonymous]
    [HttpGet]
    [Route("Suppliers/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetSupplier(int id)
    {
        int statusCode = 0;
        try
        {
            SupplierDto SupplierDto = _services.GetSupplier(id);
            return CreateResponse(HttpStatusCode.OK, SupplierDto);
        }
        catch (System.InvalidOperationException ex)
        { return StatusCode(500, ex.Message); }
        catch (System.ArgumentNullException ex)
        { return StatusCode(500, ex.Message); }
        catch (Exception ex)
        {
            if (ex.Message.Contains("404"))
            { statusCode = 404; }
            else { statusCode = 500; }
            return StatusCode(statusCode, ex.Message);
        }
    }

    // // POST: /Suppliers
    // [AllowAnonymous]
    // [HttpPost]
    // [Route("/Suppliers")]
    // [ProducesResponseType(StatusCodes.Status201Created)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // public IActionResult PostSupplier(SupplierDtoRequest Supplier)
    // {
    //     try
    //     {
    //         SupplierDto SupplierDto = _services.PostSupplier(Supplier);
    //         return CreateResponse(HttpStatusCode.Created, SupplierDto);
    //     }
    //     catch (System.InvalidOperationException ex)
    //     { return StatusCode(500, ex.Message); }
    //     catch (System.ArgumentNullException ex)
    //     { return StatusCode(500, ex.Message); }
    //     catch (DbUpdateException ex)
    //     { return StatusCode(500, ex.Message); }
    //     catch (OperationCanceledException ex)
    //     { return StatusCode(500, ex.Message); }
    // }


    // // PUT: api/Suppliers/{id}
    // [AllowAnonymous]
    // [HttpPut]
    // [Route("/Suppliers/{id:int}")]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // public IActionResult PutSupplier(int id, SupplierDto SupplierRequest)
    // {
    //     int statusCode = 0;
    //     try
    //     {
    //         SupplierDto SupplierDto = _services.PutSupplier(id, SupplierRequest);
    //         return CreateResponse(HttpStatusCode.NoContent);
    //     }
    //     catch (System.InvalidOperationException ex)
    //     { return StatusCode(500, ex.Message); }
    //     catch (System.ArgumentNullException ex)
    //     { return StatusCode(500, ex.Message); }
    //     catch (DbUpdateException ex)
    //     { return StatusCode(500, ex.Message); }
    //     catch (OperationCanceledException ex)
    //     { return StatusCode(500, ex.Message); }
    //     catch (Exception ex)
    //     {
    //         if (ex.Message.Contains("404"))
    //         { statusCode = 404; }
    //         else { statusCode = 500; }
    //         return StatusCode(statusCode, ex.Message);
    //     }
    // }

    // // DELETE: api/Suppliers/{id}
    // //Authorize(Roles = "Admin")]
    // [AllowAnonymous]
    // [HttpDelete]
    // [Route("Suppliers/{id:int}")]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // public IActionResult DeleteSupplier(int id)
    // {
    //     int statusCode = 0;
    //     try
    //     {
    //         SupplierDto SupplierDto = _services.DeleteSupplier(id);
    //         return CreateResponse(HttpStatusCode.NoContent);
    //     }
    //     catch (System.InvalidOperationException ex)
    //     { return StatusCode(500, ex.Message); }
    //     catch (System.ArgumentNullException ex)
    //     { return StatusCode(500, ex.Message); }
    //     catch (DbUpdateException ex)
    //     { return StatusCode(500, ex.Message); }
    //     catch (OperationCanceledException ex)
    //     { return StatusCode(500, ex.Message); }
    //     catch (Exception ex)
    //     {
    //         if (ex.Message.Contains("404"))
    //         { statusCode = 404; }
    //         else { statusCode = 500; }
    //         return StatusCode(statusCode, ex.Message);
    //     }
    // }

}





