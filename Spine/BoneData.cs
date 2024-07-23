using Newtonsoft.Json;

namespace Skel2Json.Spine
{
    [Serializable]
    public class BoneData
    {
        [JsonProperty("name")]
        public required string Name;

        [JsonProperty("parent")]
        public string? Parent;

        // public BoneData? Parent;

        [JsonProperty("length")]
        public float? Length;

        [JsonProperty("rotation")]
        public float? Rotation;

        [JsonProperty("x")]
        public float? X;

        [JsonProperty("y")]
        public float? Y;

        [JsonProperty("scaleX")]
        public float? ScaleX;

        [JsonProperty("scaleY")]
        public float? ScaleY;

        [JsonProperty("shearX")]
        public float? ShearX;

        [JsonProperty("shearY")]
        public float? ShearY;

        // [JsonProperty("length")]
        // public float? Length;

        [JsonProperty("inherit")]
        // public string? Inherit;
        public string? Inherit;

        [JsonProperty("skin")]
        public bool? SkinRequired;

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
            return false;
        }

        public bool ShouldSerializeScaleY()
        {
            return false;
        }

        public bool ShouldSerializeShearX()
        {
            return false;
        }

        public bool ShouldSerializeShearY()
        {
            return false;
        }

        public bool ShouldSerializeInherit()
        {
            return Inherit != null
                && Inherit != StringHelper.ToCamelCase(InheritEnum.Values[0].ToString());
        }

        public bool ShouldSerializeSkinRequired()
        {
            return false;
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

        public bool ShouldSerializeVisible()
        {
            return false;
        }

        //!----- END OF NON ESSENTIALS

        // public BoneData(string name, BoneData parent)
        // {
        //     Name = name ?? throw new ArgumentNullException(nameof(name), "name cannot be null.");
        //     Parent = parent;
        // }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum Inherit
    {
        Normal,
        OnlyTranslation,
        NoRotationOrReflection,
        NoScale,
        NoScaleOrReflection
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
