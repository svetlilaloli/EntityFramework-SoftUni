namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using Artillery.DataProcessor.ExportDto;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportShells(ArtilleryContext context, double shellWeight)
        {
            var shells = context.Shells.Where(sh => sh.ShellWeight > shellWeight)
                                       .Select(sh => new
                                       {
                                           sh.ShellWeight,
                                           sh.Caliber,
                                           Guns = sh.Guns.Where(g => g.GunType == Data.Models.Enums.GunType.AntiAircraftGun)
                                                         .Select(g => new 
                                                         { 
                                                             g.GunType, 
                                                             g.GunWeight, 
                                                             g.BarrelLength, 
                                                             Range = g.Range > 3000 ? "Long-range" : "Regular range"
                                                         })
                                                         .OrderByDescending(g => g.GunWeight)
                                                         .ToList()
                                       })
                                       .OrderBy(sh => sh.ShellWeight)
                                       .ToList();
            
            var mappedShells = Mapper.Map<IEnumerable<ShellOutputDto>>(shells);
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            return JsonConvert.SerializeObject(mappedShells, jsonSettings);
        }

        public static string ExportGuns(ArtilleryContext context, string manufacturer)
        {
            var guns = context.Guns.Where(g => g.Manufacturer.ManufacturerName == manufacturer)
                                             .Select(g => new ManufacturerGunsOutputDto
                                             {
                                                 Manufacturer = g.Manufacturer.ManufacturerName,
                                                 GunType = g.GunType.ToString(),
                                                 GunWeight = g.GunWeight,
                                                 BarrelLength = g.BarrelLength,
                                                 Range = g.Range,
                                                 Countries = g.CountriesGuns.Select(c => new CountryOutputDto
                                                 {
                                                     Country = c.Country.CountryName,
                                                     ArmySize = c.Country.ArmySize
                                                 }).Where(c => c.ArmySize > 4500000).OrderBy(c => c.ArmySize).ToList()
                                             })
                                             .OrderBy(g => g.BarrelLength)
                                             .ToList();

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var root = new XmlRootAttribute("Guns");
            var serializer = new XmlSerializer(typeof(List<ManufacturerGunsOutputDto>), root);
            var sb = new StringBuilder();

            using (var sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, guns, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
    }
}
