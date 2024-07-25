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
        public float X;

        [JsonProperty("y")]
        public float Y;

        [JsonProperty("width")]
        public float Width;

        [JsonProperty("height")]
        public float Height;

        [JsonProperty("referenceScale")]
        public float? ReferenceScale = 100f;

        //!----- NON ESSENTIALS
        public bool NonEssential;

        [JsonProperty("fps")]
        public float? Fps = 30;

        [JsonProperty("images")]
        public string Images = "";

        [JsonProperty("audio")]
        public string Audio = "";

        //!----- END OF NON ESSENTIALS

        public bool ShouldSerializeReferenceScale()
        {
            return ReferenceScale != 100;
        }

        public static bool ShouldSerializeNonEssential()
        {
            return false;
        }

        public bool ShouldSerializeFps()
        {
            return Fps != 30;
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
