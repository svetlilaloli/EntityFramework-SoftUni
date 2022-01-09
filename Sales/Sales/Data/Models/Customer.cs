using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P03_SalesDatabase.Data.Models
{
    public class Customer
    {
        public Customer()
        {
            Sales = new HashSet<Sale>();
        }
        [Key]
        public int CustomerId { get; set; }
        [StringLength(100), Required]
        public string Name { get; set; }
        [StringLength(80), Required]
        public string Email { get; set; }
        public string CreditCardNumber { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
