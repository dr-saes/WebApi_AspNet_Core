using Microsoft.AspNetCore.Mvc;

namespace WebApi_AspNet_Core;

public class SuppliersServices : ControllerBase, ISuppliersServices

{
    private readonly ApiDbContext? _context;
    public SuppliersServices(ApiDbContext context)
    {
        _context = context;
    }

    //GetAll
    public List<SupplierDto> GetSuppliers()
    {
        List<SupplierDto> SuppliersDto = new List<SupplierDto>();
        var Suppliers = _context.Suppliers.ToList();
        if (Suppliers == null || Suppliers.Count == 0)
        { return new List<SupplierDto>(); }
        else
        {
            foreach (var Supplier in Suppliers)
            {
                var SupplierDto = new SupplierDto(Supplier);
                SuppliersDto.Add(SupplierDto);
            }
            return SuppliersDto;
        }

    }

    //GetId
    public SupplierDto GetSupplier(int id)
    {
        var Supplier = _context.Suppliers.Find(id);
        if (Supplier == null)
            throw new Exception($"The Supplier with ID {id} was not found. (404)");
        var SupplierDto = new SupplierDto(Supplier);
        return SupplierDto;
    }

    //Post
    public SupplierDto PostSupplier(SupplierDtoRequest supplierDtoRequest)
    {
        if (!ModelState.IsValid)
            throw new Exception($"Bad Rquest - The Supplier is invalid. (400)");
        var Supplier = new Supplier(supplierDtoRequest);
        {
            _context.Suppliers.Add(Supplier);
            _context.SaveChanges();
            var SupplierDto = new SupplierDto(Supplier);
            return SupplierDto;
        }
    }

    //PutId
    // public SupplierDto PutSupplier(int id, SupplierDtoRequest supplierDtoRequest)
    // {
    //     var Supplier = _context.Suppliers.Find(id);
    //     if (Supplier == null)
    //         throw new Exception($"The Supplier with ID {id} was not found. (404)");
    //     else
    //     {

    //         Supplier.Name = string.IsNullOrEmpty(supplierDtoRequest.Name) ? Supplier.Name : supplierDtoRequest.Name;
    //         Supplier.Document = string.IsNullOrEmpty(supplierDtoRequest.Document) ? Supplier.Document : supplierDtoRequest.Name;
    //         Supplier.SupplierType = string.IsNullOrEmpty(supplierDtoRequest.SupplierType) ? Supplier.SupplierType : supplierDtoRequest.SupplierType;



    //         _context.Suppliers.Update(Supplier);
    //         _context.SaveChanges();
    //         var SupplierDto = new SupplierDto(Supplier);
    //         return SupplierDto;
    //     }
    // }

    // public SupplierDto DeleteSupplier(int id)
    // {
    //     var Supplier = _context.Suppliers.Find(id);
    //     if (Supplier == null)
    //         throw new Exception($"The Supplier with ID {id} was not found. (404)");
    //     _context.Suppliers.Remove(Supplier);
    //     _context.SaveChanges();
    //     var SupplierDto = new SupplierDto(Supplier);
    //     return SupplierDto;
    // }
}
