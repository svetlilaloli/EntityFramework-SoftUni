using Newtonsoft.Json;

namespace CarDealer.Dto.InputDto
{
    public class SupplierInputDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("isImporter")]
        public bool IsImporter { get; set; }
    }
}
