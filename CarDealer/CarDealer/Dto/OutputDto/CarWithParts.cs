using Newtonsoft.Json;
using System.Collections.Generic;

namespace CarDealer.Dto.OutputDto
{
    public class CarWithParts
    {
        [JsonProperty("car")]
        public CarOutputDto Car { get; set; }
        [JsonProperty("parts")]
        public IEnumerable<PartOutputDto> Parts { get; set; }
    }
}
