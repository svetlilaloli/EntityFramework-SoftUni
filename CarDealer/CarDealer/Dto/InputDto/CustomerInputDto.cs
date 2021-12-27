﻿using Newtonsoft.Json;
using System;

namespace CarDealer.Dto.InputDto
{
    public class CustomerInputDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("birthDate")]
        public DateTime BirthDate { get; set; }
        [JsonProperty("isYoungDriver")]
        public bool IsYoungDriver { get; set; }
    }
}
