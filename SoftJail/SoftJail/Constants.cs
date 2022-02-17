namespace SoftJail
{
    public static class Constants
    {
        // prisoner
        public const int Name_Min_Length = 3;
        public const int Name_Max_Length = 20;
        public const string Nickname_Pattern = @"The [A-Z][a-z]+";
        public const int Min_Age = 18;
        public const int Max_Age = 65;
        public const double Bail_Min_Value = 0;
        public const double Bail_Max_Value = double.MaxValue;
        // officer
        public const int OfficerName_Min_Length = 0;
        public const int OfficerName_Max_Length = 30;
        public const double Min_Salary = 0;
        public const double Max_Salary = double.MaxValue;
        // cell
        public const int Cell_Min_Number = 1;
        public const int Cell_Max_Number = 1000;
        // mail
        public const string Address_Pattern = @"[a-z A-Z 0-9]+[str.]+$";
        // department
        public const int Department_Name_Min_Length = 3;
        public const int Department_Name_Max_Length = 25;
    }
}
