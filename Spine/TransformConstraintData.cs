using Newtonsoft.Json;

namespace Skel2Json.Spine
{
    [Serializable]
    public class TransformConstraintData
    {
        [JsonProperty("name")]
        public string Name = "";

        [JsonProperty("order")]
        public int Order;

        [JsonProperty("bones")]
        public List<string>? Bones;

        [JsonProperty("target")]
        public string Target = "";

        [JsonProperty("skin")]
        public bool SkinRequired;

        [JsonProperty("local")]
        public bool Local;

        [JsonProperty("relative")]
        public bool Relative;

        [JsonProperty("rotation")]
        public float OffsetRotation;

        [JsonProperty("x")]
        public float OffsetX;

        [JsonProperty("y")]
        public float OffsetY;

        [JsonProperty("scaleX")]
        public float OffsetScaleX;

        [JsonProperty("scaleY")]
        public float OffsetScaleY;

        [JsonProperty("shearY")]
        public float OffsetShearY;

        [JsonProperty("mixRotate")]
        public float MixRotate = 1;

        [JsonProperty("mixX")]
        public float MixX = 1;

        [JsonProperty("mixY")]
        public float MixY = 1;

        [JsonProperty("mixScaleX")]
        public float MixScaleX = 1;

        [JsonProperty("mixScaleY")]
        public float MixScaleY = 1;

        [JsonProperty("mixShearY")]
        public float MixShearY = 1;

        public bool ShouldSerializeOrder()
        {
            return Order != 0;
        }

        public bool ShouldSerializeSkinRequired()
        {
            return SkinRequired;
        }

        public bool ShouldSerializeLocal()
        {
            return Local;
        }

        public bool ShouldSerializeRelative()
        {
            return Relative;
        }

        public bool ShouldSerializeOffsetRotation()
        {
            return OffsetRotation != 0;
        }

        public bool ShouldSerializeOffsetX()
        {
            return OffsetX != 0;
        }

        public bool ShouldSerializeOffsetY()
        {
            return OffsetY != 0;
        }

        public static bool ShouldSerializeOffsetScaleX()
        {
            return false;
        }

        public static bool ShouldSerializeOffsetScaleY()
        {
            return false;
        }

        public bool ShouldSerializeOffsetShearY()
        {
            return OffsetShearY != 0;
        }

        public bool ShouldSerializeMixRotate()
        {
            return MixRotate != 1;
        }

        public bool ShouldSerializeMixX()
        {
            return MixX != 1;
        }

        public bool ShouldSerializeMixY()
        {
            return MixY != 1;
        }

        public bool ShouldSerializeMixScaleX()
        {
            return MixScaleX != 1;
        }

        public bool ShouldSerializeMixScaleY()
        {
            return MixScaleY != 1;
        }

        public bool ShouldSerializeMixShearY()
        {
            return MixShearY != 1;
        }
    }
}
