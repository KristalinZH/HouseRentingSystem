namespace HouseRentingSystem.Common
{
    public static class EntityValidationConstants
    {
        public static class Category
        {
            public const int NameMinLenght = 2;
            public const int NameMaxLenght = 50; 
        }
        public static class House
        {
            public const int TitleMinLenght = 10;
            public const int TitleMaxLenght = 50;

            public const int AddressMinLenght = 30;
            public const int AddressMaxLenght = 150;


            public const int DescriptionMinLenght = 50;
            public const int DescriptionMaxLenght = 500;

            public const string PricePerMonthMinValue = "0";
            public const string PricePerMonthMaxValue = "2000";

            public const int ImageUrlMaxLenght = 2048;
        }
        public static class Agent
        {
            public const int PhoneNumberMinLenght = 7;
            public const int PhoneNumberMaxLenght = 15;
        }
        public static class ApplicationUser 
        {
            public const int FirstNameMinLenght = 1;
            public const int FirstNameMaxLenght = 15;

            public const int LastNameMinLenght = 1;
            public const int LastNameMaxLenght = 15;
        }

    }
}
