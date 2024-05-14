using System.ComponentModel.DataAnnotations;

namespace WebApi_AspNet_Core;

public class SupplierDto
{
    public SupplierDto(Supplier supplier)
    {
        this.Name = supplier.Name;
        this.Document = supplier.Document;
        this.SupplierType = supplier.SupplierType;
        this.PersonType = supplier.PersonType;
        this.Active = supplier.Active;
    }

    public string? Name { get; set; }
    public string? Document { get; set; }
    public string? SupplierType { get; set; }
    public string? PersonType { get; set; }
    public bool Active { get; set; }

}


