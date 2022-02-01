namespace Theatre.DataProcessor
{
    using AutoMapper;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using Theatre.Data;
    using Theatre.Data.Models;
    using Theatre.Data.Models.Enums;
    using Theatre.DataProcessor.ImportDto;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportPlay
            = "Successfully imported {0} with genre {1} and a rating of {2}!";

        private const string SuccessfulImportActor
            = "Successfully imported actor {0} as a {1} character!";

        private const string SuccessfulImportTheatre
            = "Successfully imported theatre {0} with #{1} tickets!";

        public static string ImportPlays(TheatreContext context, string xmlString)
        {
            var root = new XmlRootAttribute("Plays");
            var serializer = new XmlSerializer(typeof(List<PlayInputDto>), root);
            var playsDto = (List<PlayInputDto>)serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();
            var mappedPlays = new List<Play>();

            foreach (var playDto in playsDto)
            {
                if (!IsValid(playDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                else
                {
                    var duration = TimeSpan.Parse(playDto.Duration, CultureInfo.InvariantCulture);
                    var minDuration = TimeSpan.Parse(Constants.MIN_DURATION);

                    if (duration < minDuration)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    if (!Enum.IsDefined(typeof(Genre), playDto.Genre))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    else
                    {
                        mappedPlays.Add(Mapper.Instance.Map<Play>(playDto));
                        sb.AppendLine(string.Format(SuccessfulImportPlay, playDto.Title, playDto.Genre, playDto.Rating));
                    }
                }
            }

            context.Plays.AddRange(mappedPlays);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportCasts(TheatreContext context, string xmlString)
        {
            var root = new XmlRootAttribute("Casts");
            var serializer = new XmlSerializer(typeof(List<CastInputDto>), root);
            var castsDto = (List<CastInputDto>)serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();
            var mappedCasts = new List<Cast>();

            foreach (var castDto in castsDto)
            {
                if (!IsValid(castDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                else
                {
                    if (!(castDto.IsMainCharacter.ToLower() == "true" || castDto.IsMainCharacter.ToLower() == "false"))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    if (!int.TryParse(castDto.PlayId, out _))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    else
                    {
                        mappedCasts.Add(Mapper.Instance.Map<Cast>(castDto));
                        sb.AppendLine(string.Format(SuccessfulImportActor, castDto.FullName, castDto.IsMainCharacter.ToLower() == "true" ? "main" : "lesser"));
                    }
                }
            }

            context.Casts.AddRange(mappedCasts);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }
        public static string ImportTtheatersTickets(TheatreContext context, string jsonString)
        {
            var theatresDto = JsonConvert.DeserializeObject<IEnumerable<TheatreInputDto>>(jsonString);
            var sb = new StringBuilder();
            var mappedTheatres = new List<Theatre>();

            foreach (var theatreDto in theatresDto)
            {
                if (!IsValid(theatreDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                else
                {
                    var validTickets = new List<TicketInputDto>();

                    foreach (var ticketDto in theatreDto.Tickets)
                    {
                        if (!IsValid(ticketDto))
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }
                        else
                        {
                            validTickets.Add(ticketDto);
                        }
                    }
                    
                    theatreDto.Tickets = validTickets;
                    
                    mappedTheatres.Add(Mapper.Instance.Map<Theatre>(theatreDto));
                    sb.AppendLine(string.Format(SuccessfulImportTheatre, theatreDto.Name, theatreDto.Tickets.Count));
                }
            }

            context.Theatres.AddRange(mappedTheatres);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validator = new ValidationContext(dto);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(dto, validator, validationRes, true);
            return result;
        }
    }
}
