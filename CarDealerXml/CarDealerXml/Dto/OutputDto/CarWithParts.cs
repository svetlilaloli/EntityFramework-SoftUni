using System.Collections.Generic;
using System.Xml.Serialization;

namespace CarDealer.Dto.OutputDto
{
    [XmlType("car")]
    public class CarWithParts
    {
        [XmlAttribute("make")]
        public string Make { get; set; }
        [XmlAttribute("model")]
        public string Model { get; set; }
        [XmlAttribute("travelled-distance")]
        public long TraveledDistance { get; set; }
        [XmlArray("parts")]
        public List<PartOutputDto> Parts { get; set; }
    }
}
