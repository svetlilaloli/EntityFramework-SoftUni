using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theatre.Data.Models
{
    public class Cast
    {
        [Key]
        public int Id { get; set; }
        [MinLength(Constants.MIN_STRING_LENGTH), MaxLength(Constants.MAX_STRING_LENGTH), Required]
        public string FullName { get; set; }
        [Required]
        public bool IsMainCharacter { get; set; }
        [RegularExpression(Constants.PHONE_FORMAT), Required]
        public string PhoneNumber { get; set; }
        [ForeignKey(nameof(Play)), Required]
        public int PlayId { get; set; }
        public virtual Play Play { get; set; }
    }
}