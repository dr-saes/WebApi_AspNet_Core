
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WebApi_AspNet_Core;


public class ValidationState
{
    public class AllowedSupplierTypeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is not int SupplierType)
            {
                return false;
            }

            SuppliersTypeEnum? supplierTypeEnum = Enum.TryParse<SuppliersTypeEnum>(SupplierType.ToString(), out var parsedEnum)
                ? parsedEnum
                : null;


            return supplierTypeEnum != null && Enum.IsDefined(typeof(SuppliersTypeEnum), supplierTypeEnum);
        }
    }


}
