using Newtonsoft.Json;
using Skel2Json.Spine.Enums;

namespace Skel2Json.Spine
{
    [Serializable]
    public class PathConstraintData
    {
        [JsonProperty("name")]
        public string Name = "";

        [JsonProperty("order")]
        public int Order;

        [JsonProperty("skin")]
        public bool SkinRequired;

        [JsonProperty("bones")]
        public List<string> Bones = [];

        [JsonProperty("target")]
        public string Target = "";

        [JsonProperty("positionMode")]
        public string PositionMode = "percent";

        [JsonProperty("spacingMode")]
        public string SpacingMode = "length";

        [JsonProperty("rotateMode")]
        public string RotateMode = "tangent";

        [JsonProperty("rotation")]
        public float OffsetRotation;

        [JsonProperty("position")]
        public float Position;

        [JsonProperty("spacing")]
        public float Spacing;

        [JsonProperty("mixRotate")]
        public float MixRotate = 1;

        [JsonProperty("mixX")]
        public float MixX = 1;

        [JsonProperty("mixY")]
        public float MixY = 1;

        public bool ShouldSerializePositionMode()
        {
            return PositionMode != "percent";
        }

        public bool ShouldSerializeSpacingMode()
        {
            return SpacingMode != "length";
        }

        public bool ShouldSerializeRotateMode()
        {
            return RotateMode != "tangent";
        }

        public bool ShouldSerializeOffsetRotation()
        {
            return OffsetRotation != 0;
        }

        public bool ShouldSerializePosition()
        {
            return Position != 0;
        }

        public bool ShouldSerializeSpacing()
        {
            return Spacing != 0;
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
    }

    public class PositionModeEnum
    {
        public static readonly PositionMode[] Values = [PositionMode.Fixed, PositionMode.Percent];
    }

    public class SpacingModeEnum
    {
        public static readonly SpacingMode[] Values =
        [
            SpacingMode.Length,
            SpacingMode.Fixed,
            SpacingMode.Percent,
            SpacingMode.Proportional
        ];
    }

    public class RotateModeEnum
    {
        public static readonly RotateMode[] Values =
        [
            RotateMode.Tangent,
            RotateMode.Chain,
            RotateMode.ChainScale
        ];
    }
}
