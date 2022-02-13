using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dto.Import
{
    [XmlType("Purchase")]
    public class PurchaseInputDto
    {
        [XmlAttribute("title"), Required]
        public string Title { get; set; }
        [XmlElement("Type"), Required]
        public string Type { get; set; }
        [XmlElement("Key"), RegularExpression(Constants.PRODUCT_KEY_PATTERN), Required]
        public string Key { get; set; }
        [XmlElement("Card"), RegularExpression(Constants.CARD_PATTERN), Required]
        public string Card { get; set; }
        [XmlElement("Date"), Required]
        public string Date { get; set; }
    }
}
