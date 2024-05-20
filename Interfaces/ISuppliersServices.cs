using Microsoft.AspNetCore.Mvc;

namespace WebApi_AspNet_Core;

public interface ISuppliersServices
{
    List<SupplierDto> GetSuppliers();
    SupplierDto GetSupplier(int id);
    SupplierDto PostSupplier(SupplierDtoRequest SupplierDtoRequest);
    // SupplierDto PutSupplier(int id, SupplierDto SupplierDto);
    // SupplierDto DeleteSupplier(int id);


}
