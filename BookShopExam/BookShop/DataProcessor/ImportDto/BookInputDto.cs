using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace BookShop.DataProcessor.ImportDto
{
    [XmlType("Book")]
    public class BookInputDto
    {
        [Required, MinLength(Constants.NameMinLength), MaxLength(Constants.NameMaxLength), XmlElement("Name")]
        public string Name { get; set; }
        [Required, XmlElement("Genre")]
        public string Genre { get; set; }
        [Range((double)Constants.MinPrice, (double)Constants.MaxPrice), XmlElement("Price")]
        public decimal? Price { get; set; }
        [Range(Constants.MinPages, Constants.MaxPages), XmlElement("Pages")]
        public int? Pages { get; set; }
        [Required, XmlElement("PublishedOn")]
        public string PublishedOn { get; set; }
    }
}
