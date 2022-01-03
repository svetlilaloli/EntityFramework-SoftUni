using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public int Discount { get; set; }
        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }
        public Car Car { get; set; }
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
