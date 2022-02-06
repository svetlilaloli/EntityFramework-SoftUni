using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
    public class ProjectTasksOutputDto
    {
        [XmlAttribute("TasksCount")]
        public int TaskCount { get; set; }
        [XmlElement("ProjectName")]
        public string ProjectName { get; set; }
        [XmlElement("HasEndDate")]
        public string HasEndDate { get; set; }
        [XmlArray("Tasks")]
        public TaskProjectOutputDto[] Tasks { get; set; }
    }
}
