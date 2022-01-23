﻿using System.Collections.Generic;

namespace Artillery.DataProcessor.ImportDto
{
    public class GunInputDto
    {
        public int ManufacturerId { get; set; }
        public int GunWeight { get; set; }
        public double BarrelLength { get; set; }
        public int? NumberBuild { get; set; }
        public int Range { get; set; }
        public string GunType { get; set; }
        public int ShellId { get; set; }
        public List<CountryIdDto> Countries { get; set; }
    }
}
