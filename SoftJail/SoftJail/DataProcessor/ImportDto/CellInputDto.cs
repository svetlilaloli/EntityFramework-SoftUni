using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class CellInputDto
    {
        [Range(Constants.Cell_Min_Number, Constants.Cell_Max_Number), Required]
        public int CellNumber { get; set; }
        [Required]
        public bool HasWindow { get; set; }
    }
}
