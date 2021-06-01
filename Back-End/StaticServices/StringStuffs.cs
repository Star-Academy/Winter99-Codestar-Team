namespace Back_End.StaticServices
{
    public static class StringStuffs
    {
        public static string MakeCamelCase(string text)
        {
            return char.ToLowerInvariant(text[0]) + text[1..];
        }
    }
}