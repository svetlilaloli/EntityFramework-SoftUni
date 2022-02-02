namespace TeisterMask
{
    internal class Constants
    {
        // employee
        internal const int MIN_USERNAME_LENGTH = 3;
        internal const string USERNAME_REGEX = @"[a-zA-Z0-9]";
        internal const string PHONE_REGEX = @"\d{3}-\d{3}-\d{4}";
        // project, task
        internal const int MIN_NAME_LENGTH = 2;
        internal const int MAX_NAME_LENGTH = 40;
    }
}
