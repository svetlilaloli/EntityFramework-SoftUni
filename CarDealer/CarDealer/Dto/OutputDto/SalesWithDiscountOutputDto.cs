using Newtonsoft.Json;

namespace CarDealer.Dto.OutputDto
{
    public class SalesWithDiscountOutputDto
    {
        [JsonProperty("car")]
        public CarOutputDto Car { get; set; }
        [JsonProperty("customerName")]
        public string CustomerName { get; set; }
        [JsonProperty("Discount")]
        public string Discount { get; set; }
        [JsonProperty("price")]
        public string Price { get; set; }
        [JsonProperty("priceWithDiscount")]
        public string PriceWithDiscount { get; set; }
    }
}
