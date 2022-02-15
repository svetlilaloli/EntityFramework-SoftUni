using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dto.Export
{
    [XmlType("User")]
    public class UserOutputDto
    {
        [XmlAttribute("username")]
        public string Username { get; set; }
        [XmlArray("Purchases")]
        public PurchaseOutputDto[] Purchases { get; set; }
        [XmlElement("TotalSpent")]
        public decimal TotalSpent { get; set; }
    }
}
