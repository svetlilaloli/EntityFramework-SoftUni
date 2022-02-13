using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class CardInputDto
    {
        [RegularExpression(Constants.CARD_PATTERN), Required]
        public string Number { get; set; }
        [RegularExpression(Constants.CVC_PATTERN), Required]
        public int CVC { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
