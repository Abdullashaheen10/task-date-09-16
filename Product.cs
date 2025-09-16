using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P02_SalesDatabase.Models;

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
