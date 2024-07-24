using Newtonsoft.Json;
using Skel2Json.Spine.Enums;
using Skel2Json.Spine.Helpers;

namespace Skel2Json.Spine
{
    [Serializable]
    public class BoneData
    {
        [JsonProperty("name")]
        public string Name = "";

        [JsonProperty("parent")]
        public string Parent = "";

        [JsonProperty("length")]
        public float Length;

        [JsonProperty("rotation")]
        public float Rotation;

        [JsonProperty("x")]
        public float X;

        [JsonProperty("y")]
        public float Y;

        [JsonProperty("scaleX")]
        public float ScaleX = 1f;

        [JsonProperty("scaleY")]
        public float ScaleY = 1f;

        [JsonProperty("shearX")]
        public float ShearX;

        [JsonProperty("shearY")]
        public float ShearY;

        [JsonProperty("inherit")]
        public string Inherit = "normal";

        [JsonProperty("skin")]
        public bool SkinRequired;

        //!----- NON ESSENTIALS
        public bool NonEssential;

        [JsonProperty("color")]
        public string? Color;

        [JsonProperty("icon")]
        public string? Icon;

        [JsonProperty("visible")]
        public bool? Visible;

        public bool ShouldSerializeParent()
        {
            return Parent != null;
        }

        public bool ShouldSerializeLength()
        {
            return Length > 1;
        }

        public bool ShouldSerializeRotation()
        {
            return Rotation != 0;
        }

        public bool ShouldSerializeX()
        {
            return X != 0;
        }

        public bool ShouldSerializeY()
        {
            return Y != 0;
        }

        public bool ShouldSerializeScaleX()
        {
            return ScaleX != 1f;
        }

        public bool ShouldSerializeScaleY()
        {
            return ScaleY != 1f;
        }

        public bool ShouldSerializeShearX()
        {
            return ShearX != 0;
        }

        public bool ShouldSerializeShearY()
        {
            return ShearY != 0;
        }

        public bool ShouldSerializeInherit()
        {
            return Inherit != null
                && Inherit != StringHelper.ToCamelCase(InheritEnum.Values[0].ToString());
        }

        public bool ShouldSerializeSkinRequired()
        {
            return SkinRequired;
        }

        public static bool ShouldSerializeNonEssential()
        {
            return false;
        }

        public bool ShouldSerializeColor()
        {
            return NonEssential && Color != "9b9b9bff";
        }

        public bool ShouldSerializeIcon()
        {
            return NonEssential && Icon != null;
        }

        public static bool ShouldSerializeVisible()
        {
            return false;
        }

        //!----- END OF NON ESSENTIALS

        public override string ToString()
        {
            return Name;
        }
    }

    public class InheritEnum
    {
        public static readonly Inherit[] Values =
        [
            Inherit.Normal,
            Inherit.OnlyTranslation,
            Inherit.NoRotationOrReflection,
            Inherit.NoScale,
            Inherit.NoScaleOrReflection
        ];
    }
}
