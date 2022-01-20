using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P03_SalesDatabase.Data.Models
{
    public class Product
    {
        public Product()
        {
            Sales = new HashSet<Sale>();
        }
        [Key]
        public int ProductId { get; set; }
        [StringLength(50), Required]
        public string Name { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        [StringLength(250), Required]
        public string Description { get; set; } = "No description";
        public ICollection<Sale> Sales { get; set; }
    }
}
