using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class MailInputDto
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string Sender { get; set; }
        [RegularExpression(Constants.Address_Pattern), Required]
        public string Address { get; set; }
    }
}
