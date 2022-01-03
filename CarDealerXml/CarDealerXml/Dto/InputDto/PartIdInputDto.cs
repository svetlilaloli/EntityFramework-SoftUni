using System.Xml.Serialization;

namespace CarDealer.Dto.InputDto
{
    [XmlType("partId")]
    public class PartIdInputDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
