using Newtonsoft.Json;
using System.Collections.Generic;

namespace Artillery.DataProcessor.ExportDto
{
    public class ShellOutputDto
    {
        [JsonProperty("ShellWeight")]
        public double ShellWeight { get; set; }
        [JsonProperty("Caliber")]
        public string Caliber { get; set; }
        [JsonProperty("Guns")]
        public List<GunOutputDto> Guns { get; set; }
    }
}
