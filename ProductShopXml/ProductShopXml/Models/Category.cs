using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductShop.Models
{
    public class Category
    {
        public Category()
        {
            CategoryProducts = new List<CategoryProduct>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MinLength(3), MaxLength(15)]
        public string Name { get; set; }
        public ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}