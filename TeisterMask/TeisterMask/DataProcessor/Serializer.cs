namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var projects = context.Projects.Where(p => p.Tasks.Count > 0)
                                           .ToArray()
                                           .Select(p => new ProjectTasksOutputDto
                                           {
                                               TaskCount = p.Tasks.Count,
                                               ProjectName = p.Name,
                                               HasEndDate = p.DueDate == null ? "No" : "Yes",
                                               Tasks = p.Tasks.Select(t => new TaskProjectOutputDto
                                               {
                                                   Name = t.Name,
                                                   Label = t.LabelType.ToString()
                                               })
                                               .OrderBy(t => t.Name)
                                               .ToArray(),
                                           })
                                           .OrderByDescending(p => p.TaskCount)
                                           .ThenBy(p => p.ProjectName)
                                           .ToArray();

            var root = new XmlRootAttribute("Projects");
            var serializer = new XmlSerializer(typeof(ProjectTasksOutputDto[]), root);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var sb = new StringBuilder();

            using (StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, projects, namespaces);
            }
            
            return sb.ToString().TrimEnd();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context.Employees.Where(e => e.EmployeesTasks.Any(t => t.Task.OpenDate >= date))
                                             .ToArray()
                                             .Select(e => new EmployeeTasksOutputDto
                                             {
                                                 Username = e.Username,
                                                 Tasks = e.EmployeesTasks.Select(et => et.Task)
                                                                         .Where(t => t.OpenDate >= date)
                                                                         .OrderByDescending(t => t.DueDate)
                                                                         .ThenBy(t => t.Name)
                                                                         .Select(t => new TaskOutputDto
                                                                         {
                                                                             TaskName = t.Name,
                                                                             OpenDate = t.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                                                                             DueDate = t.DueDate.ToString("d", CultureInfo.InvariantCulture),
                                                                             LabelType = t.LabelType.ToString(),
                                                                             ExecutionType = t.ExecutionType.ToString()
                                                                         })
                                                                         .ToArray()
                                             })
                                             .OrderByDescending(e => e.Tasks.Length)
                                             .ThenBy(e => e.Username)
                                             .Take(10)
                                             .ToArray();


            return JsonConvert.SerializeObject(employees, Formatting.Indented).TrimEnd();
        }
    }
}