namespace Theatre.DataProcessor
{
    using AutoMapper;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Theatre.Data;
    using Theatre.Data.Models;
    using Theatre.DataProcessor.ExportDto;

    public class Serializer
    {
        public static string ExportTheatres(TheatreContext context, int numbersOfHalls)
        {
            var numberOfTickets = 20;
            var theatres = context.Theatres.Where(t => t.NumberOfHalls >= numbersOfHalls && t.Tickets.Count >= numberOfTickets)
                                              .Select(t => new TheatreOutputDto
                                              {
                                                  Name = t.Name,
                                                  Halls = t.NumberOfHalls,
                                                  TotalIncome = t.Tickets.Where(ti => ti.RowNumber >= 1 && ti.RowNumber <= 5).Sum(ti => ti.Price),
                                                  Tickets = t.Tickets.Where(ti => ti.RowNumber >= 1 && ti.RowNumber <= 5).Select(ti => new TicketOutputDto
                                                  {
                                                      Price = ti.Price,
                                                      RowNumber = ti.RowNumber
                                                  })
                                                  .OrderByDescending(ti => ti.Price)
                                                  .ToList()
                                              })
                                              .OrderByDescending(t => t.Halls)
                                              .ThenBy(t => t.Name)
                                              .ToList();
            
            var mappedTheatres = Mapper.Map<IEnumerable<TheatreOutputDto>>(theatres);
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            return JsonConvert.SerializeObject(mappedTheatres, jsonSettings);
        }

        public static string ExportPlays(TheatreContext context, double rating)
        {
            var plays = context.Plays.Where(p => p.Rating <= rating)
                                     .Select(p => new PlayOutputDto
                                     {
                                         Title = p.Title,
                                         Duration = p.Duration.ToString("c"),
                                         Rating = p.Rating == 0 ? "Premier" : p.Rating.ToString(),
                                         Genre = p.Genre,
                                         Actors = p.Casts.Where(c => c.IsMainCharacter == true)
                                                          .Select(c => new ActorOutputDto
                                                          {
                                                              FullName = c.FullName,
                                                              MainCharacter = string.Format("Plays main character in '{0}'.", p.Title)
                                                          })
                                                          .OrderByDescending(c => c.FullName)
                                                          .ToList()
                                     })
                                     .OrderBy(p => p.Title)
                                     .ThenByDescending(p => p.Genre)
                                     .ToList();

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var root = new XmlRootAttribute("Plays");
            var serializer = new XmlSerializer(typeof(List<PlayOutputDto>), root);
            var sb = new StringBuilder();

            using (var sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, plays, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
    }
}
