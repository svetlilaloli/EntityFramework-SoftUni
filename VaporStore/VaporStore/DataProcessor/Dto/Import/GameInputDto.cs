using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class GameInputDto
    {
        [Required]
        public string Name { get; set; }
        [Range(Constants.MIN_PRICE, Constants.MAX_PRICE), Required]
        public decimal Price { get; set; }
        [Required]
        public string ReleaseDate { get; set; }
        [Required]
        public string Developer { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required, MinLength(1)]
        public string[] Tags { get; set; }
    }
}
