using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_AspNet_Core;

public class Supplier
{
    public Supplier() { }
    public Supplier(SupplierDtoRequest supplierDtoRequest)
    {
        this.Name = supplierDtoRequest.Name;
        this.Document = supplierDtoRequest.Document;

        if (supplierDtoRequest.SupplierType == 0) this.SupplierType = SuppliersTypeEnum.TECHNOLOGY;
        if (supplierDtoRequest.SupplierType == 1) this.SupplierType = SuppliersTypeEnum.CLOTHING;
        if (supplierDtoRequest.SupplierType == 2) this.SupplierType = SuppliersTypeEnum.CHILDREN;
        if (supplierDtoRequest.SupplierType == 3) this.SupplierType = SuppliersTypeEnum.OTHERS;

        if (supplierDtoRequest.Document.Count() >= 14)
            this.PersonType = PersonTypeEnum.LEGALPERSON;
        else
            this.PersonType = PersonTypeEnum.NATURALPERSON;
        this.Active = supplierDtoRequest.Active;
    }

    [Key]
    new public int Id { get; set; }
    public string? Name { get; set; }
    public string? Document { get; set; }
    public SuppliersTypeEnum SupplierType { get; set; }
    public PersonTypeEnum PersonType { get; set; }
    public bool Active { get; set; }

}
