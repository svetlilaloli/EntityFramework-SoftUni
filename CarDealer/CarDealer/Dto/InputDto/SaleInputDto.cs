using Newtonsoft.Json;

namespace CarDealer.Dto.InputDto
{
    public class SaleInputDto
    {
        [JsonProperty("carId")]
        public int CarId { get; set; }
        [JsonProperty("customerId")]
        public int CustomerId { get; set; }
        [JsonProperty("discount")]
        public int Discount { get; set; }
    }
}
