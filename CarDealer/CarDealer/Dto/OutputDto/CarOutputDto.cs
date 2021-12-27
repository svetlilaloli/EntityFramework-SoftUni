using Newtonsoft.Json;

namespace CarDealer.Dto.OutputDto
{
    public class CarOutputDto
    {
        [JsonProperty("Make")]
        public string Make { get; set; }
        [JsonProperty("Model")]
        public string Model { get; set; }
        [JsonProperty("TravelledDistance")]
        public long TravelledDistance { get; set; }
    }
}
