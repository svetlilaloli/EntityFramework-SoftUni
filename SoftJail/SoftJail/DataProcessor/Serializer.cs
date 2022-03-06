namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor.ExportDto;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context.Prisoners.Where(p => ids.Contains(p.Id)).ToList()
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.FullName,
                    CellNumber = p.Cell.CellNumber,
                    Officers = p.PrisonerOfficers.Select(po => new
                    {
                        OfficerName = po.Officer.FullName,
                        Department = po.Officer.Department.Name
                    }).ToList().OrderBy(o => o.OfficerName),
                    TotalOfficerSalary = p.PrisonerOfficers.Sum(o => o.Officer.Salary),
                })
                .ToList()
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id);

            return JsonConvert.SerializeObject(prisoners, Formatting.Indented);
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var prisonersDto = context.Prisoners.Where(p => prisonersNames.Contains(p.FullName))
                                         .Select(p => new PrisonerMailOutputDto
                                         {
                                             Id = p.Id,
                                             FullName = p.FullName,
                                             IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd"),
                                             Messages = p.Mails.Select(m => new MailOutputDto
                                             {
                                                 Description = new string(m.Description.Reverse().ToArray()),
                                             }).ToArray()
                                         })
                                         .OrderBy(p => p.FullName)
                                         .ThenBy(p => p.Id)
                                         .ToArray();

            var root = new XmlRootAttribute("Prisoners");
            var serializer = new XmlSerializer(typeof(PrisonerMailOutputDto[]), root);
            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var sb = new StringBuilder();

            using (StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, prisonersDto, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
    }
}