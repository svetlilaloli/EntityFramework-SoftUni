using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VaporStore.Data.Models.Enums;

namespace VaporStore.Data.Models
{
    public class Card
    {
        public Card()
        {
            Purchases = new HashSet<Purchase>();
        }
        [Key]
        public int Id { get; set; }
        [RegularExpression(Constants.CARD_PATTERN), Required]
        public string Number { get; set; }
        [RegularExpression(Constants.CVC_PATTERN), Required]
        public string Cvc { get; set; }
        [Required]
        public CardType Type { get; set; }
        [ForeignKey(nameof(User)), Required]
        public int UserId { get; set; }
        [Required]
        public virtual User User { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}