using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class DepartmentCellInputDto
    {
        [MinLength(Constants.Department_Name_Min_Length), MaxLength(Constants.Department_Name_Max_Length), Required]
        public string Name { get; set; }
        [Required]
        public CellInputDto[] Cells { get; set; }
    }
}
