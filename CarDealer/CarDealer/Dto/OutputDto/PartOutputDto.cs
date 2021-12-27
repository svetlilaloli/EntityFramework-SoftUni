using Newtonsoft.Json;

namespace CarDealer.Dto.OutputDto
{
    public class PartOutputDto
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Price")]
        public string Price { get; set; }
    }
}
