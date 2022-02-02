using System.Collections.Generic;
using System.Xml.Serialization;
using Theatre.Data.Models.Enums;

namespace Theatre.DataProcessor.ExportDto
{
    [XmlType("Play")]
    public class PlayOutputDto
    {
        [XmlAttribute("Title")]
        public string Title { get; set; }
        [XmlAttribute("Duration")]
        public string Duration { get; set; }
        [XmlAttribute("Rating")]
        public string Rating { get; set; }
        [XmlAttribute("Genre")]
        public Genre Genre { get; set; }
        [XmlArray("Actors")]
        public List<ActorOutputDto> Actors { get; set; }
    }
}
