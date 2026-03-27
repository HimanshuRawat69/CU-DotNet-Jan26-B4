namespace SmartBank.Helpers
{
    public static class AccountNumberGenerator
    {
        public static string Generate(int id)
        {
            var year = DateTime.Now.Year;
            return "SB-" + year + "-" + id.ToString().PadLeft(6, '0');
        }
    }
}
