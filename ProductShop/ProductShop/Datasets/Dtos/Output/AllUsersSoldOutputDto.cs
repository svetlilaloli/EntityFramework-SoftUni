using System.Collections.Generic;

namespace ProductShop.Datasets.Dtos.Output
{
    public class AllUsersSoldOutputDto
    {
        public AllUsersSoldOutputDto()
        {
            Users = new List<UserWithProductsOutputDto>();
        }
        public int UsersCount { get; set; }
        public IEnumerable<UserWithProductsOutputDto> Users { get; set; }
    }
}
