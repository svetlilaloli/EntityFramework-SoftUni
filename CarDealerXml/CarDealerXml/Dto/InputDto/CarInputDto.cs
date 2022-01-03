using System.Xml.Serialization;

namespace CarDealer.Dto.InputDto
{
    [XmlType("Car")]
    public class CarInputDto
    {
        [XmlElement("make")]
        public string Make { get; set; }
        [XmlElement("model")]
        public string Model { get; set; }
        [XmlElement("TraveledDistance")]
        public long TraveledDistance { get; set; }
        [XmlArray("parts")]
        public PartIdInputDto[] Parts { get; set; }
        [XmlIgnore]
        public int[] PartIds { get; set; }
    }
}
