using System.Collections.Generic;

namespace ProductShop.Datasets.Dtos.Output
{
    public class ProductsCountOutputDto
    {
        //public ProductsCountOutputDto()
        //{
        //    SoldProducts = new List<ProductOutputDto>();
        //}
        public int Count { get; set; }
        public IEnumerable<ProductOutputDto> Products { get; set; }
    }
}
