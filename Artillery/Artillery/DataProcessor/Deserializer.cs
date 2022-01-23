namespace Artillery.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Artillery.Data;
    using Artillery.Data.Models;
    using Artillery.Data.Models.Enums;
    using Artillery.DataProcessor.ImportDto;
    using AutoMapper;
    using Newtonsoft.Json;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage =
                "Invalid data.";
        private const string SuccessfulImportCountry =
            "Successfully import {0} with {1} army personnel.";
        private const string SuccessfulImportManufacturer =
            "Successfully import manufacturer {0} founded in {1}.";
        private const string SuccessfulImportShell =
            "Successfully import shell caliber #{0} weight {1} kg.";
        private const string SuccessfulImportGun =
            "Successfully import gun {0} with a total weight of {1} kg. and barrel length of {2} m.";

        public static string ImportCountries(ArtilleryContext context, string xmlString)
        {
            var root = new XmlRootAttribute("Countries");
            var serializer = new XmlSerializer(typeof(List<CountryInputDto>), root);
            var countriesDto = (List<CountryInputDto>)serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();
            var validCountries = new List<CountryInputDto>();

            foreach (var countryDto in countriesDto)
            {
                if (IsValidCountry(countryDto))
                {
                    validCountries.Add(countryDto);
                    sb.AppendLine(string.Format(SuccessfulImportCountry, countryDto.CountryName, countryDto.ArmySize));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            var mappedCountries = new List<Country>();

            foreach (var countryDto in validCountries)
            {
                mappedCountries.Add(Mapper.Instance.Map<Country>(countryDto));
            }

            context.Countries.AddRange(mappedCountries);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportManufacturers(ArtilleryContext context, string xmlString)
        {
            var root = new XmlRootAttribute("Manufacturers");
            var serializer = new XmlSerializer(typeof(List<ManufacturerInputDto>), root);
            var manufacturersDto = (List<ManufacturerInputDto>)serializer.Deserialize(new StringReader(xmlString));
            var sb = new StringBuilder();
            var validManufacturers = new List<ManufacturerInputDto>();

            foreach (var manufacturerDto in manufacturersDto)
            {
                if (IsValidManufacturer(manufacturerDto))
                {
                    if (!validManufacturers.Any(n => n.ManufacturerName == manufacturerDto.ManufacturerName))
                    {
                        validManufacturers.Add(manufacturerDto);
                        var foundedPlace = manufacturerDto.Founded.Split(", ");
                        string founded = string.Join(", ", foundedPlace[^2], foundedPlace[^1]);
                        sb.AppendLine(string.Format(SuccessfulImportManufacturer, manufacturerDto.ManufacturerName, founded));
                    }
                    else
                    {
                        sb.AppendLine(ErrorMessage);
                    }
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            var mappedManufacturers = new List<Manufacturer>();

            foreach (var manufacturerDto in validManufacturers)
            {
                mappedManufacturers.Add(Mapper.Instance.Map<Manufacturer>(manufacturerDto));
            }

            context.Manufacturers.AddRange(mappedManufacturers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportShells(ArtilleryContext context, string xmlString)
        {
            var root = new XmlRootAttribute("Shells");
            var serializer = new XmlSerializer(typeof(List<ShellInputDto>), root);
            var shellsDto = (List<ShellInputDto>)serializer.Deserialize(new StringReader(xmlString));
            var sb = new StringBuilder();
            var validShells = new List<ShellInputDto>();

            foreach (var shellDto in shellsDto)
            {
                if (IsValidShell(shellDto))
                {
                    validShells.Add(shellDto);
                    sb.AppendLine(string.Format(SuccessfulImportShell, shellDto.Caliber, shellDto.ShellWeight));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            var mappedShells = new List<Shell>();

            foreach (var shellDto in validShells)
            {
                mappedShells.Add(Mapper.Instance.Map<Shell>(shellDto));
            }

            context.Shells.AddRange(mappedShells);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportGuns(ArtilleryContext context, string jsonString)
        {
            var gunsDto = JsonConvert.DeserializeObject<IEnumerable<GunInputDto>>(jsonString);
            var sb = new StringBuilder();
            var validGuns = new List<GunInputDto>();

            foreach (var gunDto in gunsDto)
            {
                if (IsValidGun(gunDto))
                {
                    validGuns.Add(gunDto);
                    sb.AppendLine(string.Format(SuccessfulImportGun, gunDto.GunType, gunDto.GunWeight, gunDto.BarrelLength));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            var mappedGuns = new List<Gun>();

            foreach (var gunDto in validGuns)
            {
                mappedGuns.Add(Mapper.Instance.Map<Gun>(gunDto));
            }

            context.Guns.AddRange(mappedGuns);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }
        private static bool IsValidCountry(CountryInputDto country)
        {
            if (country.CountryName != null)
            {
                if (country.CountryName.Length >= Constants.MinCountryNameLength && country.CountryName.Length <= Constants.MaxCountryNameLength)
                {
                    if (country.ArmySize >= Constants.MinArmySize && country.ArmySize <= Constants.MaxArmySize)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        private static bool IsValidManufacturer(ManufacturerInputDto manufacturer)
        {
            if (manufacturer.ManufacturerName != null)
            {
                if (manufacturer.ManufacturerName.Length >= Constants.MinManufacturerNameLength && manufacturer.ManufacturerName.Length <= Constants.MaxManufacturerNameLength)
                {
                    if (manufacturer.Founded != null)
                    {
                        if (manufacturer.Founded.Length >= Constants.MinFoundedLength && manufacturer.Founded.Length <= Constants.MaxFoundedLength)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        private static bool IsValidShell(ShellInputDto shell)
        {
            if (shell.ShellWeight >= Constants.MinShellWeight && shell.ShellWeight <= Constants.MaxShellWeight)
            {
                if (shell.Caliber != null)
                {
                    if (shell.Caliber.Length >= Constants.MinCaliberLength && shell.Caliber.Length <= Constants.MaxCaliberLength)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        private static bool IsValidGun(GunInputDto gun)
        {
            if (gun.GunWeight >= Constants.MinGunWeight && gun.GunWeight <= Constants.MaxGunWeight)
            {
                if (gun.BarrelLength >= Constants.MinBarrelLength && gun.BarrelLength <= Constants.MaxBarrelLength)
                {
                    if (gun.Range >= Constants.MinRange && gun.Range <= Constants.MaxRange)
                    {
                        if (gun.GunType != null && Enum.IsDefined(typeof(GunType), gun.GunType))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
