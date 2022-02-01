using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Theatre.DataProcessor.ImportDto
{
    public class TicketInputDto
    {
        [JsonProperty("Price")]
        [Range(Constants.MIN_PRICE, Constants.MAX_PRICE), Required]
        public decimal Price { get; set; }
        [JsonProperty("RowNumber")]
        [Range(Constants.MIN_NUMBER, Constants.MAX_NUMBER), Required]
        public sbyte RowNumber { get; set; }
        [JsonProperty("PlayId"), Required]
        public int PlayId { get; set; }
    }
}
