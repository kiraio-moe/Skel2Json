using Newtonsoft.Json;

namespace Skel2Json.Core
{
    [Serializable]
    public class SkeletonData
    {
        [JsonProperty("skeleton")]
        public Dictionary<string, object> Skeleton = [];
    }
}
