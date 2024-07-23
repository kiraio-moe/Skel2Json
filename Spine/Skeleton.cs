using Newtonsoft.Json;

namespace Skel2Json.Spine
{
    [Serializable]
    public class Skeleton
    {
        [JsonProperty("hash")]
        public string? Hash;

        [JsonProperty("spine")]
        public string? Spine;

        [JsonProperty("x")]
        public float? X;

        [JsonProperty("y")]
        public float? Y;

        [JsonProperty("width")]
        public float? Width;

        [JsonProperty("height")]
        public float? Height;

        [JsonProperty("referenceScale")]
        public float? ReferenceScale;

        //!----- NON ESSENTIALS
        public bool NonEssential;

        [JsonProperty("fps")]
        public float? Fps;

        [JsonProperty("images")]
        public string? Images;

        [JsonProperty("audio")]
        public string? Audio;

        //!----- END OF NON ESSENTIALS

        public static bool ShouldSerializeReferenceScale()
        {
            return false;
        }

        public static bool ShouldSerializeNonEssential()
        {
            return false;
        }

        public static bool ShouldSerializeFps()
        {
            return false;
        }

        public bool ShouldSerializeImages()
        {
            return NonEssential;
        }

        public bool ShouldSerializeAudio()
        {
            return NonEssential;
        }
    }
}
