using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theatre.Data.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        [Range(Constants.MIN_PRICE, Constants.MAX_PRICE), Required]
        public decimal Price { get; set; }
        [Range(Constants.MIN_NUMBER, Constants.MAX_NUMBER), Required]
        public sbyte RowNumber { get; set; }
        [ForeignKey(nameof(Play)), Required]
        public int PlayId { get; set; }
        public virtual Play Play { get; set; }
        [ForeignKey(nameof(Theatre)), Required]
        public int TheatreId { get; set; }
        public virtual Theatre Theatre { get; set; }
    }
}