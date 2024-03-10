using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApi_AspNet_Core;

public class ApiDbContext : IdentityDbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }

    public ApiDbContext()
    {

    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Supliers { get; set; }
}

