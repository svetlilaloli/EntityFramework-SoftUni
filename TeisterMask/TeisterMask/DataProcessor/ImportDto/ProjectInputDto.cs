using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Project")]
    public class ProjectInputDto
    {
        [XmlElement("Name")]
        [MinLength(Constants.MIN_NAME_LENGTH), MaxLength(Constants.MAX_NAME_LENGTH), Required]
        public string Name { get; set; }
        [XmlElement("OpenDate"), Required]
        public string OpenDate { get; set; }
        [XmlElement("DueDate")]
        public string DueDate { get; set; }
        [XmlArray("Tasks")]
        public List<TaskInputDto> Tasks { get; set; }
    }
}
