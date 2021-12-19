using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductShop.Models
{
    public class Product
    {
        public Product()
        {
            CategoryProducts = new List<CategoryProduct>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MinLength(3)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required, ForeignKey(nameof(Seller))]
        public int SellerId { get; set; }
        public User Seller { get; set; }
        [ForeignKey(nameof(Buyer))]
        public int? BuyerId { get; set; }
        public User Buyer { get; set; }
        public ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}