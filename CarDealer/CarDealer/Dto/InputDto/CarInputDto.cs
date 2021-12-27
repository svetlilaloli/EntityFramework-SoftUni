using Newtonsoft.Json;
using System.Collections.Generic;

namespace CarDealer.Dto.InputDto
{
    public class CarInputDto
    {
        [JsonProperty("make")]
        public string Make { get; set; }
        [JsonProperty("model")]
        public string Model { get; set; }
        [JsonProperty("travelledDistance")]
        public int TravelledDistance { get; set; }
        [JsonProperty("partsId")]
        public List<int> PartsId { get; set; }
    }
}
