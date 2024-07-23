namespace Skel2Json.Spine
{
    public static class ColorHelper
    {
        /// <summary>
        /// Convert Integer to Hexadecimal RGBA color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToHex(int color)
        {
            byte a = (byte)((color >> 24) & 0xFF);
            byte r = (byte)((color >> 16) & 0xFF);
            byte g = (byte)((color >> 8) & 0xFF);
            byte b = (byte)(color & 0xFF);

            return $"{a:X2}{r:X2}{g:X2}{b:X2}".ToLower();
        }
    }
}
