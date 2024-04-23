using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace WebApi_AspNet_Core
{
    public class InMemoryDatabaseInitializer
    {
        public static void InitializeDB(ApiDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
            {
                return; // O banco de dados já foi populado
            }

            var products = new List<Product>
            {
                new Product { Name = "Product 1", Price = 10.99m, StockQuantity = 100, Description = "Description 1" },
                new Product { Name = "Product 2", Price = 20.50m, StockQuantity = 50, Description = "Description 2" },
                new Product { Name = "Product 3", Price = 15.75m, StockQuantity = 75, Description = "Description 3" }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        public static void InitializeNullDB(ApiDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
            {
                return;
            }

            var products = new List<Product>();

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
