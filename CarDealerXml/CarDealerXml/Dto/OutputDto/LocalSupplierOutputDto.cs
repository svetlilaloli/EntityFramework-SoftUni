using System.Xml.Serialization;

namespace CarDealer.Dto.OutputDto
{
    [XmlType("supplier")]
    public class LocalSupplierOutputDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("parts-count")]
        public int PartsCount { get; set; }
    }
}
