using System.Xml.Serialization;

namespace CarDealer.Dto.OutputDto
{
    [XmlType("car")]
    public class BmwCarOutputDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlAttribute("model")]
        public string Model { get; set; }
        [XmlAttribute("travelled-distance")]
        public long TraveledDistance { get; set; }
    }
}
