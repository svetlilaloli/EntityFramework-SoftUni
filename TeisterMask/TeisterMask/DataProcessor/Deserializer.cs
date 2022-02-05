namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.Data.Models;
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.DataProcessor.ImportDto;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var root = new XmlRootAttribute("Projects");
            var serializer = new XmlSerializer(typeof(List<ProjectInputDto>), root);
            var projectsDto = (List<ProjectInputDto>)serializer.Deserialize(new StringReader(xmlString));

            var mappedProjects = new List<Project>();
            var sb = new StringBuilder();

            foreach (var projectDto in projectsDto)
            {
                if (!IsValid(projectDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isProjectOpenDateValid = DateTime.TryParseExact(projectDto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime projectOpenDate);

                if (!isProjectOpenDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!string.IsNullOrWhiteSpace(projectDto.DueDate))
                {
                    bool isProjectDueDateValid = DateTime.TryParseExact(projectDto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime projectDueDate);

                    if (!isProjectDueDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    // open date after due date
                    if (projectOpenDate > projectDueDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                }
                else
                {
                    projectDto.DueDate = null;
                }


                var validTasks = new HashSet<TaskInputDto>();

                foreach (var taskDto in projectDto.Tasks)
                {
                    if (!IsValid(taskDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isTaskOpenDateValid = DateTime.TryParseExact(taskDto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime taskOpenDate);

                    if (!isTaskOpenDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isTaskDueDateValid = DateTime.TryParseExact(taskDto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime taskDueDate);

                    if (!isTaskDueDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    // open date after due date
                    if (taskOpenDate > taskDueDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    // task open date before project open date
                    if (taskOpenDate < projectOpenDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (projectDto.DueDate != null)
                    {
                        DateTime projectDueDate = DateTime.ParseExact(projectDto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                        // task open date after project due date
                        if (taskOpenDate > projectDueDate)
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }
                        // task due date after project due date
                        if (taskDueDate > projectDueDate)
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }

                    }

                    var isExecutionTypeValid = Enum.TryParse(typeof(ExecutionType), taskDto.ExecutionType, true, out _);
                    var isLabelTypeValid = Enum.TryParse(typeof(LabelType), taskDto.LabelType, true, out _);
                    
                    if (!isExecutionTypeValid || !isLabelTypeValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    
                    validTasks.Add(taskDto);
                }

                projectDto.Tasks = validTasks.ToList();
                mappedProjects.Add(Mapper.Instance.Map<Project>(projectDto));
                sb.AppendLine(string.Format(SuccessfullyImportedProject, projectDto.Name, projectDto.Tasks.Count));

            }

            context.Projects.AddRange(mappedProjects);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var employeesDto = JsonConvert.DeserializeObject<IEnumerable<EmployeeInputDto>>(jsonString);
            var sb = new StringBuilder();
            var mappedEmployees = new HashSet<Employee>();

            foreach (var employeeDto in employeesDto)
            {
                if (!IsValid(employeeDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var validTasks = new HashSet<int>();

                foreach (var taskId in employeeDto.Tasks.Distinct())
                {
                    var tasksIds = context.Tasks.Select(t => t.Id).ToHashSet();

                    if (!tasksIds.Contains(taskId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    else
                    {
                        validTasks.Add(taskId);
                    }
                }

                employeeDto.Tasks = validTasks.ToList();
                mappedEmployees.Add(Mapper.Instance.Map<Employee>(employeeDto));
                sb.AppendLine(string.Format(SuccessfullyImportedEmployee, employeeDto.Username, employeeDto.Tasks.Count));
            }

            context.Employees.AddRange(mappedEmployees);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}