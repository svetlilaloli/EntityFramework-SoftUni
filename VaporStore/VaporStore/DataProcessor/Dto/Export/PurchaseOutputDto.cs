using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dto.Export
{
    [XmlType("Purchase")]
    public class PurchaseOutputDto
    {
        [XmlElement("Card")]
        public string Card { get; set; }
        [XmlElement("Cvc")]
        public string Cvc { get; set; }
        [XmlElement("Date")]
        public string Date { get; set; }
        [XmlElement("Game")]
        public PurchaseGameOutputDto Game { get; set; }
    }
}
