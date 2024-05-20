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
    public SupplierDto PostSupplier(SupplierDtoRequest SupplierDtoRequest)
    {
        if (!ModelState.IsValid)
            throw new Exception($"Bad Rquest - The Supplier is invalid. (400)");
        var Supplier = new Supplier(SupplierDtoRequest);
        {
            _context.Suppliers.Add(Supplier);
            _context.SaveChanges();
            var SupplierDto = new SupplierDto(Supplier);
            return SupplierDto;
        }
    }

    // //PutId
    // public SupplierDto PutSupplier(int id, SupplierDto SupplierDto)
    // {

    //     var Supplier = _context.Suppliers.Find(id);
    //     if (Supplier == null)
    //         throw new Exception($"The Supplier with ID {id} was not found. (404)");
    //     else
    //     {
    //         Supplier.Name = string.IsNullOrEmpty(SupplierDto.Name) ? Supplier.Name : SupplierDto.Name;
    //         Supplier.Description = string.IsNullOrEmpty(SupplierDto.Description) ? Supplier.Description : SupplierDto.Description;
    //         Supplier.Price = SupplierDto.Price == 0 ? Supplier.Price : SupplierDto.Price;
    //         Supplier.StockQuantity = SupplierDto.StockQuantity == 0 ? Supplier.StockQuantity : SupplierDto.StockQuantity;
    //         _context.Suppliers.Update(Supplier);
    //         _context.SaveChanges();
    //         var SupplierDtoNew = new SupplierDto(Supplier);
    //         return SupplierDtoNew;
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
