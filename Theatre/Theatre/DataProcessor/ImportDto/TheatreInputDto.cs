using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Theatre.DataProcessor.ImportDto
{
    public class TheatreInputDto
    {
        [JsonProperty("Name")]
        [MinLength(Constants.MIN_STRING_LENGTH), MaxLength(Constants.MAX_STRING_LENGTH), Required]
        public string Name { get; set; }
        [JsonProperty("NumberOfHalls")]
        [Range(Constants.MIN_NUMBER, Constants.MAX_NUMBER), Required]
        public sbyte NumberOfHalls { get; set; }
        [JsonProperty("Director")]
        [MinLength(Constants.MIN_STRING_LENGTH), MaxLength(Constants.MAX_STRING_LENGTH), Required]
        public string Director { get; set; }
        [JsonProperty("Tickets")]
        public List<TicketInputDto> Tickets { get; set; }
    }
}
