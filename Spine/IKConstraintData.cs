using Newtonsoft.Json;

namespace Skel2Json.Spine
{
    [Serializable]
    public class IKConstraintData()
    {
        [JsonProperty("name")]
        public string? Name;

        [JsonProperty("order")]
        public int Order;

        [JsonProperty("bones")]
        public List<string>? Bones;

        [JsonProperty("target")]
        public string? Target;

        [JsonProperty("skin")]
        public bool SkinRequired;

        [JsonProperty("bendPositive")]
        public bool BendDirection = true;

        [JsonProperty("compress")]
        public bool Compress;

        [JsonProperty("stretch")]
        public bool Stretch;

        [JsonProperty("uniform")]
        public bool Uniform;

        [JsonProperty("mix")]
        public float Mix = 1f;

        [JsonProperty("softness")]
        public float Softness;

        public bool ShouldSerializeOrder()
        {
            return Order != 0;
        }
        public bool ShouldSerializeSkinRequired()
        {
            return SkinRequired;
        }
        public bool ShouldSerializeBendDirection()
        {
            return !BendDirection;
        }
        public bool ShouldSerializeCompress()
        {
            return Compress;
        }
        public bool ShouldSerializeStretch()
        {
            return Stretch;
        }
        public bool ShouldSerializeUniform()
        {
            return Uniform;
        }
        public bool ShouldSerializeMix()
        {
            return Mix != 1f;
        }
        public bool ShouldSerializeSoftness()
        {
            return Softness != 0;
        }
    }
}
