namespace SoftJail.DataProcessor
{
    using AutoMapper;
    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data";
        private const string SuccessfullyImportedDepartments = "Imported {0} with {1} cells";
        private const string SuccessfullyImportedPrisoners = "Imported {0} {1} years old";
        private const string SuccessfullyImportedOfficers = "Imported {0} ({1} prisoners)";
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var departmentsDto = JsonConvert.DeserializeObject<IEnumerable<DepartmentCellInputDto>>(jsonString);
            var sb = new StringBuilder();
            var mappedDepartments = new List<Department>();

            foreach (var departmentDto in departmentsDto)
            {
                if (!IsValid(departmentDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (departmentDto.Cells.Length == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isValidCell = true;
                foreach (var cell in departmentDto.Cells)
                {
                    if (!IsValid(cell))
                    {
                        sb.AppendLine(ErrorMessage);
                        isValidCell = false;
                        break;
                    }
                }
                if (!isValidCell)
                {
                    continue;
                }
                mappedDepartments.Add(Mapper.Instance.Map<Department>(departmentDto));
                sb.AppendLine(string.Format(SuccessfullyImportedDepartments, departmentDto.Name, departmentDto.Cells.Length));
            }
            context.Departments.AddRange(mappedDepartments);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var prisonersDto = JsonConvert.DeserializeObject<IEnumerable<PrisonerMailInputDto>>(jsonString);
            var sb = new StringBuilder();
            var mappedPrisoners = new List<Prisoner>();

            foreach (var prisonerDto in prisonersDto)
            {
                if (!IsValid(prisonerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (prisonerDto.FullName == "null")
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                bool isIncarcerationDateValid = DateTime.TryParseExact(prisonerDto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime incarcerationDate);
                if (!isIncarcerationDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime? releaseDate;
                if (prisonerDto.ReleaseDate == null)
                {
                    releaseDate = null;
                }
                else
                {
                    if (prisonerDto.ReleaseDate == "null")
                    {
                        releaseDate = null;
                    }
                    else
                    {
                        bool isReleaseDateValid = DateTime.TryParseExact(prisonerDto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedReleaseDate);
                        if (!isReleaseDateValid)
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }
                        releaseDate = parsedReleaseDate;
                        if (releaseDate < incarcerationDate)
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }
                    }
                }

                bool isValidMail = true;

                foreach (var mail in prisonerDto.Mails)
                {
                    if (!IsValid(mail))
                    {
                        sb.AppendLine(ErrorMessage);
                        isValidMail = false;
                        break;
                    }
                }

                if (!isValidMail)
                {
                    continue;
                }

                mappedPrisoners.Add(new Prisoner
                {
                    FullName = prisonerDto.FullName,
                    Nickname = prisonerDto.Nickname,
                    Age = prisonerDto.Age,
                    IncarcerationDate = incarcerationDate,
                    ReleaseDate = releaseDate,
                    Bail = prisonerDto.Bail,
                    CellId = prisonerDto.CellId,
                    Mails = prisonerDto.Mails.Select(p => new Mail
                    {
                        Description = p.Description,
                        Sender = p.Sender,
                        Address = p.Address
                    }).ToArray()
                });
                sb.AppendLine(string.Format(SuccessfullyImportedPrisoners, prisonerDto.FullName, prisonerDto.Age));
            }

            context.AddRange(mappedPrisoners);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var root = new XmlRootAttribute("Officers");
            var serializer = new XmlSerializer(typeof(List<OfficerPrisonerInputDto>), root);
            var officersDto = (List<OfficerPrisonerInputDto>)serializer.Deserialize(new StringReader(xmlString));
            var sb = new StringBuilder();
            var mappedOfficers = new List<Officer>();

            foreach (var officerDto in officersDto)
            {
                if (!IsValid(officerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isPositionValid = Enum.TryParse(officerDto.Position, true, out Position position);
                if (!isPositionValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                
                bool isWeaponValid = Enum.TryParse(officerDto.Weapon, true, out Weapon weapon);
                if (!isWeaponValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                
                mappedOfficers.Add(Mapper.Instance.Map<Officer>(officerDto));
                sb.AppendLine(string.Format(SuccessfullyImportedOfficers, officerDto.FullName, officerDto.Prisoners.Length));
            }

            context.Officers.AddRange(mappedOfficers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}