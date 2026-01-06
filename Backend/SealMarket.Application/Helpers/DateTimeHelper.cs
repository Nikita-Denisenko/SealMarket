namespace SealMarket.Application.Helpers
{
    public static class DateTimeHelper
    {
        public static readonly DateTime MinDateToFilter = new(2024, 1, 1);
        public static DateTime MaxDateToFilter => DateTime.Today;
    }
}
