﻿using System.Xml.Serialization;

namespace ProductShop.Dto.Output
{
    [XmlType("Product")]
    public class ProductOutputDto
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}
