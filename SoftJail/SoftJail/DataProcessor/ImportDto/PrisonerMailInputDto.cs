using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class PrisonerMailInputDto
    {
        [MinLength(Constants.Name_Min_Length), MaxLength(Constants.Name_Max_Length), Required]
        public string FullName { get; set; }
        [RegularExpression(Constants.Nickname_Pattern), Required]
        public string Nickname { get; set; }
        [Range(Constants.Min_Age, Constants.Max_Age), Required]
        public int Age { get; set; }
        [Required]
        public string IncarcerationDate { get; set; }
        public string ReleaseDate { get; set; }
        [Range(Constants.Bail_Min_Value, Constants.Bail_Max_Value)]
        public decimal? Bail { get; set; }
        public int? CellId { get; set; }
        public MailInputDto[] Mails { get; set; }
    }
}
