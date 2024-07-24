using Newtonsoft.Json;
using Skel2Json.Spine.Enums;

namespace Skel2Json.Spine
{
    [Serializable]
    public class SlotData
    {
        [JsonProperty("name")]
        public string Name = "";

        [JsonProperty("bone")]
        public string Bone = "";

        [JsonProperty("color")]
        public string? Color;

        [JsonProperty("dark")]
        public string? DarkColor;

        [JsonProperty("attachment")]
        public string? Attachment;

        [JsonProperty("blend")]
        public string Blend = "normal";

        public bool NonEssential;

        [JsonProperty("visible")]
        public bool? Visible;

        public bool ShouldSerializeColor()
        {
            return Color != null && Color != "ffffffff";
        }
        public bool ShouldSerializeDarkColor()
        {
            return DarkColor != null && DarkColor != "ffffff";
        }
        public bool ShouldSerializeAttachment()
        {
            return Attachment != null;
        }
        public bool ShouldSerializeBlend()
        {
            return Blend != null && Blend != "normal";
        }
        public static bool ShouldSerializeNonEssential()
        {
            return false;
        }
        public static bool ShouldSerializeVisible()
        {
            return false;
        }
    }

    public class BlendModeEnum
    {
        public static readonly BlendMode[] Values =
        [
            BlendMode.Normal,
            BlendMode.Additive,
            BlendMode.Multiply,
            BlendMode.Screen
        ];
    }
}
