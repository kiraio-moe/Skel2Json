using Newtonsoft.Json;

namespace Skel2Json.Spine
{
    [Serializable]
    public class PhysicsConstraintData
    {
        [JsonProperty("name")]
        public string Name = "";

        [JsonProperty("order")]
        public int Order;

        [JsonProperty("bone")]
        public string Bone = "";

        [JsonProperty("skin")]
        public bool SkinRequired;

        [JsonProperty("x")]
        public float X;

        [JsonProperty("y")]
        public float Y;

        [JsonProperty("rotate")]
        public float Rotate;

        [JsonProperty("scaleX")]
        public float ScaleX;

        [JsonProperty("shearX")]
        public float ShearX;

        [JsonProperty("limit")]
        public float Limit = 5000;

        [JsonProperty("fps")]
        public int Step = 60;

        [JsonProperty("inertia")]
        public float Inertia = 1;

        [JsonProperty("strength")]
        public float Strength = 100;

        [JsonProperty("damping")]
        public float Damping = 1;

        [JsonProperty("mass")]
        public float MassInverse = 1;

        [JsonProperty("wind")]
        public float Wind;

        [JsonProperty("gravity")]
        public float Gravity;

        [JsonProperty("inertiaGlobal")]
        public bool InertiaGlobal;

        [JsonProperty("strengthGlobal")]
        public bool StrengthGlobal;

        [JsonProperty("dampingGlobal")]
        public bool DampingGlobal;

        [JsonProperty("massGlobal")]
        public bool MassGlobal;

        [JsonProperty("windGlobal")]
        public bool WindGlobal;

        [JsonProperty("gravityGlobal")]
        public bool GravityGlobal;

        [JsonProperty("mixGlobal")]
        public bool MixGlobal;

        [JsonProperty("mix")]
        public float Mix = 1;

        public bool ShouldSerializeOrder()
        {
            return Order != 0;
        }

        public bool ShouldSerializeSkin()
        {
            return SkinRequired;
        }

        public bool ShouldSerializeX()
        {
            return X != 0;
        }

        public bool ShouldSerializeY()
        {
            return Y != 0;
        }

        public bool ShouldSerializeRotate()
        {
            return Rotate != 0;
        }

        public bool ShouldSerializeScaleX()
        {
            return ScaleX != 0;
        }

        public bool ShouldSerializeShearX()
        {
            return ShearX != 0;
        }

        public bool ShouldSerializeLimit()
        {
            return Limit != 5000;
        }

        public bool ShouldSerializeStep()
        {
            return Step != 60;
        }

        public bool ShouldSerializeInertia()
        {
            return Inertia != 1;
        }

        public bool ShouldSerializeStrength()
        {
            return Strength != 100;
        }

        public bool ShouldSerializeDamping()
        {
            return Damping != 1;
        }

        public bool ShouldSerializeMassInverse()
        {
            return MassInverse != 1;
        }

        public bool ShouldSerializeWind()
        {
            return Wind != 0;
        }

        public bool ShouldSerializeGravity()
        {
            return Gravity != 0;
        }

        public bool ShouldSerializeInertiaGlobal()
        {
            return InertiaGlobal;
        }

        public bool ShouldSerializeStrengthGlobal()
        {
            return StrengthGlobal;
        }

        public bool ShouldSerializeDampingGlobal()
        {
            return DampingGlobal;
        }

        public bool ShouldSerializeMassGlobal()
        {
            return MassGlobal;
        }

        public bool ShouldSerializeWindGlobal()
        {
            return WindGlobal;
        }

        public bool ShouldSerializeGravityGlobal()
        {
            return GravityGlobal;
        }

        public bool ShouldSerializeMixGlobal()
        {
            return MixGlobal;
        }

        public bool ShouldSerializeMix()
        {
            return Mix != 1;
        }
    }
}
