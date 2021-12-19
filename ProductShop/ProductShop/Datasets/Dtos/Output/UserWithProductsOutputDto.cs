namespace ProductShop.Datasets.Dtos.Output
{
    public class UserWithProductsOutputDto
    {
        public string LastName { get; set; }
        public int? Age { get; set; }
        public ProductsCountOutputDto SoldProducts { get; set; }
    }
}
