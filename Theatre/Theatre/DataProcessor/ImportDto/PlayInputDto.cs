using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Theatre.DataProcessor.ImportDto
{
    [XmlType("Play")]
    public class PlayInputDto
    {
        [XmlElement("Title")]
        [MinLength(Constants.MIN_STRING_LENGTH), MaxLength(Constants.MAX_TITLE_LENGTH), Required]
        public string Title { get; set; }
        [XmlElement("Duration")]
        [Required]
        public string Duration { get; set; }
        [XmlElement("Rating")]
        [Range(Constants.MIN_RATING, Constants.MAX_RATING), Required]
        public float Rating { get; set; }
        [XmlElement("Genre"), Required]
        public string Genre { get; set; }
        [XmlElement("Description")]
        [MaxLength(Constants.MAX_DESCRIPTION_LENGTH), Required]
        public string Description { get; set; }
        [XmlElement("Screenwriter")]
        [MinLength(Constants.MIN_STRING_LENGTH), MaxLength(Constants.MAX_STRING_LENGTH), Required]
        public string Screenwriter { get; set; }
    }
}
