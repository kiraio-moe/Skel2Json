using Newtonsoft.Json;

namespace Skel2Json.Spine
{
    [Serializable]
    public class SkeletonData
    {
        [JsonProperty("skeleton")]
        public Skeleton Skeleton = new();

        [JsonProperty("bones")]
        public ExposedList<BoneData> Bones = [];

        [JsonProperty("slots")]
        public ExposedList<SlotData> Slots = [];

        [JsonProperty("ik")]
        public ExposedList<IKConstraintData> IKConstraints = [];

        [JsonProperty("transform")]
        public ExposedList<TransformConstraintData> TransformConstraints = [];

        [JsonProperty("path")]
        public ExposedList<PathConstraintData> PathConstraints = [];

        [JsonProperty("physics")]
        public ExposedList<PhysicsConstraintData> PhysicsConstraints = [];

        public bool ShouldSerializePathConstraints()
        {
            return PathConstraints.Count > 0;
        }

        public bool ShouldSerializePhysicsConstraints()
        {
            return PhysicsConstraints.Count > 0;
        }
    }
}
