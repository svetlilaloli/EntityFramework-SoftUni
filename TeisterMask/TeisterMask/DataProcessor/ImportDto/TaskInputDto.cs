using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Task")]
    public class TaskInputDto
    {
        [XmlElement("Name")]
        [MinLength(Constants.MIN_NAME_LENGTH), MaxLength(Constants.MAX_NAME_LENGTH), Required]
        public string Name { get; set; }
        [XmlElement("OpenDate"), Required]
        public string OpenDate { get; set; }
        [XmlElement("DueDate"), Required]
        public string DueDate { get; set; }
        [XmlElement("ExecutionType"), Required]
        public int ExecutionType { get; set; }
        [XmlElement("LabelType"), Required]
        public int LabelType { get; set; }
    }
}
