namespace BookShop
{
    public class Constants
    {
        public const byte NameMinLength = 3;
        public const byte NameMaxLength = 30;
        public const string EmailPattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
        public const string PhonePattern = @"^[0-9]{3}-[0-9]{3}-[0-9]{4}$";
        public const decimal MinPrice = 0.01M;
        public const decimal MaxPrice = decimal.MaxValue;
        public const int MinPages = 50;
        public const int MaxPages = 5000;
    }
}
