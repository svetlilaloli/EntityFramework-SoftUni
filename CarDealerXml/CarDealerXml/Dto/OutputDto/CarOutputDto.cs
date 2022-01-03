using System.Xml.Serialization;

namespace CarDealer.Dto.OutputDto
{
    [XmlType("car")]
    public class CarOutputDto
    {
        [XmlElement("make")]
        public string Make { get; set; }
        [XmlElement("model")]
        public string Model { get; set; }
        [XmlElement("travelled-distance")]
        public long TraveledDistance { get; set; }
    }
}
