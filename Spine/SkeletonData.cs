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
    }
}
