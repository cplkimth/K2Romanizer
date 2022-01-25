namespace K2Romanizer
{
    public static class Extensions
    {
        public static string Capitalize(this string value)
        {
            return char.ToUpper(value[0]) + value.Substring(1);
        }
    }
}