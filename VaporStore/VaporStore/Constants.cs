namespace VaporStore
{
    public static class Constants
    {
        // Game
        public const double MIN_PRICE = 0;
        public const double MAX_PRICE = double.MaxValue;
        // User
        public const int USERNAME_MIN_LENGTH = 3;
        public const int USERNAME_MAX_LENGTH = 20;
        public const string FULL_NAME_PATTERN = @"^[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+$";
        public const int MIN_AGE = 3;
        public const int MAX_AGE = 103;
        // Card
        public const string CARD_PATTERN = @"^\d{4} \d{4} \d{4} \d{4}$";
        public const string CVC_PATTERN = @"^\d{3}$";
        // Purchase
        public const string PRODUCT_KEY_PATTERN = @"^[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$";
    }
}
