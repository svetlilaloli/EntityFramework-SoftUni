using Newtonsoft.Json;

namespace CarDealer.Dto.OutputDto
{
    public class CustomerBoughtCarOutputDto
    {
        [JsonProperty("fullName")]
        public string FullName { get; set; }
        [JsonProperty("boughtCars")]
        public int BoughtCars { get; set; }
        [JsonProperty("spentMoney")]
        public decimal SpentMoney { get; set; }
    }
}
