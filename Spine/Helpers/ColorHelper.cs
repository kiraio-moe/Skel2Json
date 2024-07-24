using System.Drawing;

namespace Skel2Json.Spine.Helpers
{
    public static class ColorHelper
    {
        /// <summary>
        /// Convert Integer to Hexadecimal RGBA color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToRGBA(int color)
        {
            byte r = (byte)((color >> 24) & 0xFF);
            byte g = (byte)((color >> 16) & 0xFF);
            byte b = (byte)((color >> 8) & 0xFF);
            byte a = (byte)(color & 0xFF);

            return $"{r:X2}{g:X2}{b:X2}{a:X2}".ToLower();
        }

        /// <summary>
        /// Convert Integer to Hexadecimal RGB color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToRGB(int color)
        {
            byte r = (byte)((color >> 16) & 0xFF);
            byte g = (byte)((color >> 8) & 0xFF);
            byte b = (byte)(color & 0xFF);

            return $"{r:X2}{g:X2}{b:X2}".ToLower();
        }
    }
}
