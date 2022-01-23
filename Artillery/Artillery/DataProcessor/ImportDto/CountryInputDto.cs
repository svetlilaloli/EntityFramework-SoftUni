using System.Xml.Serialization;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType("Country")]
    public class CountryInputDto
    {
        [XmlElement("CountryName")]
        public string CountryName { get; set; }
        [XmlElement("ArmySize")]
        public int ArmySize { get; set; }
    }
}
