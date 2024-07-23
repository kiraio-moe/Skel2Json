namespace Skel2Json.Spine
{
    public static class StringHelper
    {
        public static string ToCamelCase(string input)
        {
            return char.ToLower(input[0]) + input[1..];
        }
    }
}
