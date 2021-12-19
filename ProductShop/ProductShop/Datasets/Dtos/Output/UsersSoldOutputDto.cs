using System.Collections.Generic;

namespace ProductShop.Datasets.Dtos.Output
{
    public class UsersSoldOutputDto
    {
        public UsersSoldOutputDto()
        {
            SoldProducts = new List<SoldProductsOutputDto>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<SoldProductsOutputDto> SoldProducts { get; set; }
    }
}
