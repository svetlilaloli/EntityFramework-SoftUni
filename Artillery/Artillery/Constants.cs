namespace Artillery
{
    public static class Constants
    {
        //country
        public const int MinCountryNameLength = 4;
        public const int MaxCountryNameLength = 60;
        public const int MinArmySize = 50000;
        public const int MaxArmySize = 10000000;

        //manufacturer
        public const int MinManufacturerNameLength = 4;
        public const int MaxManufacturerNameLength = 40;
        public const int MinFoundedLength = 10;
        public const int MaxFoundedLength = 100;

        //shell
        public const double MinShellWeight = 2;
        public const double MaxShellWeight = 1680;
        public const int MinCaliberLength = 4;
        public const int MaxCaliberLength = 30;

        //gun
        public const int MinGunWeight = 100;
        public const int MaxGunWeight = 1350000;
        public const double MinBarrelLength = 2.00;
        public const double MaxBarrelLength = 35.00;
        public const int MinRange = 1;
        public const int MaxRange = 100000;
    }
}
