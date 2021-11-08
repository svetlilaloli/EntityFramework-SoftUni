using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Collections.Generic;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using var context = new SoftUniContext();
            //Console.WriteLine(GetEmployeesFullInformation(context));
            //Console.WriteLine(GetEmployeesWithSalaryOver50000(context));
            //Console.WriteLine(GetEmployeesFromResearchAndDevelopment(context));
            //Console.WriteLine(AddNewAddressToEmployee(context));
            //Console.WriteLine(GetEmployeesInPeriod(context));
            //Console.WriteLine(GetAddressesByTown(context));
            //Console.WriteLine(GetEmployee147(context));
            //Console.WriteLine(GetDepartmentsWithMoreThan5Employees(context));
            //Console.WriteLine(GetLatestProjects(context));
            //Console.WriteLine(IncreaseSalaries(context));
            //Console.WriteLine(GetEmployeesByFirstNameStartingWithSa(context));
            //Console.WriteLine(DeleteProjectById(context));
            Console.WriteLine(RemoveTown(context));
            //var employee = GetEmployee();
            //var dbContext = new SoftUniContext();
            //employee.FirstName = "Marty";
            //var entry = dbContext.Entry(employee);
            //entry.State = EntityState.Modified;
            //dbContext.SaveChanges();
        }
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employees = context.Employees.OrderBy(x => x.EmployeeId)
                                .Select(x => new { x.FirstName, x.LastName, x.MiddleName, x.JobTitle, x.Salary })
                                .ToList();
            StringBuilder sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
            }
            return sb.ToString();
        }
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var salary = 50000;

            var employees = context.Employees.OrderBy(x => x.FirstName)
                                .Where(x => x.Salary > salary)
                                .Select(x => new { x.FirstName, x.Salary })
                                .ToList();
            StringBuilder sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} - {employee.Salary:f2}");
            }
            return sb.ToString();
        }
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var department = "Research and Development";

            var employees = context.Employees.OrderBy(x => x.Salary)
                                .ThenByDescending(x => x.FirstName)
                                .Where(x => x.Department.Name == department)
                                .Select(x => new { x.FirstName, x.LastName, x.Department.Name, x.Salary })
                                .ToList();
            StringBuilder sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.Name} - ${employee.Salary:f2}");
            }
            return sb.ToString();
        }
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var address = new Models.Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            var nakovEmployee = context.Employees.First(x => x.LastName == "Nakov");

            nakovEmployee.Address = address;

            context.SaveChanges();

            var addresses = context.Employees.OrderByDescending(x => x.AddressId)
                                            .Select(x => x.Address.AddressText)
                                            .ToList().Take(10);

            StringBuilder sb = new StringBuilder();

            foreach (var addr in addresses)
            {
                sb.AppendLine(addr);
            }

            return sb.ToString();
        }
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder content = new StringBuilder();
            int start = 2001;
            int end = 2003;

            var employees = context.Employees
                                   .Include(x => x.Manager)
                                   .Include(x => x.EmployeesProjects)
                                   .ThenInclude(x => x.Project)
                                   .Where(e => e.EmployeesProjects
                                                .Any(p => p.Project.StartDate.Year >= start
                                                       && p.Project.StartDate.Year <= end))
                                   .Take(10)
                                   .ToList();

            foreach (var e in employees)
            {
                content.AppendLine($"{e.FirstName} {e.LastName} - Manager: {e.Manager.FirstName} {e.Manager.LastName}");

                foreach (var p in e.EmployeesProjects)
                {
                    content.AppendLine($"--{p.Project.Name} -" +
                                       $" {p.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)} - " +
                                       $"{(p.Project.EndDate == null ? "not finished" : p.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture))}");
                }
            }

            return content.ToString().TrimEnd();
        }
        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var addresses = context.Addresses
                                   .Include(a => a.Employees)
                                   .Include(t => t.Town)
                                   .OrderByDescending(a => a.Employees.Count)
                                   .ThenBy(t => t.Town.Name)
                                   .ThenBy(a => a.AddressText)
                                   .Take(10)
                                   .ToList();

            foreach (var a in addresses)
            {
                sb.AppendLine($"{a.AddressText}, {a.Town.Name} - {a.Employees.Count} employees");
            }

            return sb.ToString().TrimEnd();
        }
        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            int id = 147;

            var employee = context.Employees.Include(e => e.EmployeesProjects)
                                            .ThenInclude(ep => ep.Project)
                                            .Where(e => e.EmployeeId == id)
                                            .First();

            sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

            foreach (var p in employee.EmployeesProjects.OrderBy(x => x.Project.Name))
            {
                sb.AppendLine($"{p.Project.Name}");
            }
            return sb.ToString().TrimEnd();
        }
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            int count = 5;

            var departments = context.Departments.Include(d => d.Employees)
                                                 .Where(d => d.Employees.Count > count)
                                                 .OrderBy(d => d.Employees.Count)
                                                 .ThenBy(d => d.Name)
                                                 .ToList();
            foreach (var d in departments)
            {
                sb.AppendLine($"{d.Name} - {d.Manager.FirstName} {d.Manager.LastName}");
                foreach (var e in d.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                {
                    sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                }
            }

            return sb.ToString().TrimEnd();
        }
        public static string GetLatestProjects(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            int projectCount = 10;

            var projects = context.Projects
                                  .OrderByDescending(p => p.StartDate)
                                  .Take(projectCount)
                                  .OrderBy(p => p.Name)
                                  .ToList();

            foreach (var p in projects)
            {
                sb.AppendLine($"{p.Name}");
                sb.AppendLine(p.Description);
                sb.AppendLine(p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
            }

            return sb.ToString().TrimEnd();
        }
        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            int increaseBy = 12;
            List<string> departments = new List<string>
            { "Engineering", "Tool Design", "Marketing", "Information Services" };

            var employees = context.Employees
                                   .Include(e => e.Department)
                                   .Where(d => departments.Contains(d.Department.Name))
                                   .ToList();

            foreach (var e in employees)
            {
                e.Salary += e.Salary * increaseBy / 100;
            }
            foreach (var e in employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:f2})");
            }
            return sb.ToString().TrimEnd();
        }
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            string substr = "Sa";

            var employees = context.Employees.Where(e => e.FirstName.ToLower().StartsWith(substr.ToLower()))
                                             .OrderBy(e => e.FirstName)
                                             .ThenBy(e => e.LastName)
                                             .ToList();
            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})");
            }

            return sb.ToString().TrimEnd();
        }
        public static string DeleteProjectById(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            int id = 2;
            
            var project = context.Projects.Find(id);
            var projectsToDelete = context.EmployeesProjects
                                             .Where(ep => ep.ProjectId == project.ProjectId)
                                             .ToList();

            context.RemoveRange(projectsToDelete);
            context.Projects.Remove(project);
            context.SaveChanges();
            
            var projects = context.Projects.Take(10).ToList();

            foreach (var p in projects)
            {
                sb.AppendLine(p.Name);
            }
            return sb.ToString().TrimEnd();
        }
        public static string RemoveTown(SoftUniContext context)
        {
            string result;
            string town = "Seattle";
            int count = 0;
            var townToDelete = context.Towns.FirstOrDefault(t => t.Name.ToLower() == town.ToLower());

            if (townToDelete != null)
            {
                var addressesToDelete = context.Addresses.Where(t => t.TownId == townToDelete.TownId).ToList();
                var employees = context.Employees.Where(a => addressesToDelete.Contains(a.Address)).ToList();
                count = addressesToDelete.Count;

                foreach (var e in employees)
                {
                    e.AddressId = null;
                }

                context.RemoveRange(addressesToDelete);
                context.Towns.Remove(townToDelete);
                context.SaveChanges();
            }

            result = $"{count} addresses in {town} were deleted";

            return result;
        }
        //public static Employee GetEmployee()
        //{
        //    using var context = new SoftUniContext();
        //    return context.Employees.First();
        //}
    }
}
