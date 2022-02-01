namespace Theatre
{
    internal class Constants
    {
        // theatre
        internal const int MIN_STRING_LENGTH = 4;
        internal const int MAX_STRING_LENGTH = 30;
        internal const int MIN_NUMBER = 1;
        internal const int MAX_NUMBER = 10;
        // play
        internal const int MAX_TITLE_LENGTH = 50;
        internal const float MIN_RATING = 0f;
        internal const float MAX_RATING = 10.00f;
        internal const int MAX_DESCRIPTION_LENGTH = 700;
        internal const string TIME_FORMAT = @"{hh:mm:ss}";
        internal const string MIN_DURATION = "01:00";
        internal const string MAX_DURATION = "";
        // cast
        internal const string PHONE_FORMAT = @"\+44-\d{2}-\d{3}-\d{4}";
        // ticket
        internal const double MIN_PRICE = 1.00;
        internal const double MAX_PRICE = 100.00;
    }
}
