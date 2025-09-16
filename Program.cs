// File: Models/Product.cs
using Microsoft.EntityFrameworkCore;
using P02_SalesDatabase.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_SalesDatabase.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public double Quantity { get; set; }

        public decimal Price { get; set; }

        public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();
    }
}


namespace P02_SalesDatabase.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(80)]
        public string Email { get; set; }

        public string CreditCardNumber { get; set; }

        public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();
    }
}

// File: Models/Store.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P02_SalesDatabase.Models
{
    public class Store
    {
        public int StoreId { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();
    }
}



namespace P02_SalesDatabase.Models
{
    public class Sale
    {
        public int SaleId { get; set; }

        public DateTime Date { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }
    }
}


namespace P02_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=SalesDatabase;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(e =>
            {
                e.Property(x => x.Name).IsUnicode(true);
            });

            modelBuilder.Entity<Customer>(e =>
            {
                e.Property(x => x.Name).IsUnicode(true);
                e.Property(x => x.Email).IsUnicode(false);
            });

            modelBuilder.Entity<Store>(e =>
            {
                e.Property(x => x.Name).IsUnicode(true);
            });
        }

        public void Seed()
        {
            var p1 = new Product { Name = "Product1", Quantity = 5.5, Price = 10m };
            var c1 = new Customer { Name = "Customer1", Email = "email@test.com", CreditCardNumber = "12345" };
            var s1 = new Store { Name = "Store1" };
            this.Products.Add(p1);
            this.Customers.Add(c1);
            this.Stores.Add(s1);
            this.SaveChanges();

            var sale1 = new Sale { Date = DateTime.Now, ProductId = p1.ProductId, CustomerId = c1.CustomerId, StoreId = s1.StoreId };
            this.Sales.Add(sale1);
            this.SaveChanges();
        }
    }
}