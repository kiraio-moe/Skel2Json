namespace Skel2Json.Spine.Helpers
{
    public static class StringHelper
    {
        public static string ToCamelCase(string input)
        {
            return char.ToLower(input[0]) + input[1..];
        }

        public static string ToLowerCase(string input)
        {
            return input.ToLower();
        }
    }
}
