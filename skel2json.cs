using Newtonsoft.Json;

namespace Skel2Json.Core
{
    public class Skel2Json
    {
        static void Main(string[] args)
        {
            Stream skelData = new FileStream(args[0], FileMode.Open, FileAccess.Read);
            SkeletonReader skeletonReader = new();
            SkeletonData? skeletonData = SkeletonReader.ReadSkeletonBinary(skelData);

            // Serialize to JSON
            string data = JsonConvert.SerializeObject(skeletonData, Formatting.Indented);
            File.WriteAllText($"{Path.GetFileNameWithoutExtension(args[0])}.json", data);
        }
    }
}
