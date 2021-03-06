using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeisterMask.DataProcessor.ImportDto
{
    public class EmployeeInputDto
    {
        [JsonProperty("Username")]
        [MinLength(Constants.MIN_USERNAME_LENGTH), MaxLength(Constants.MAX_NAME_LENGTH), RegularExpression(Constants.USERNAME_REGEX), Required]
        public string Username { get; set; }
        [JsonProperty("Email")]
        [EmailAddress, Required]
        public string Email { get; set; }
        [JsonProperty("Phone")]
        [DataType(DataType.PhoneNumber), RegularExpression(Constants.PHONE_REGEX), Required]
        public string Phone { get; set; }
        [JsonProperty("Tasks")]
        public List<int> Tasks { get; set; }
    }
}
