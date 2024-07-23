namespace Skel2Json.Core
{
    public class SkeletonReader
    {
        public static SkeletonData? ReadSkeletonBinary(Stream stream)
        {
            SkeletonData skeletonData = new();
            SkeletonInput input = new(stream);

            //!----- READ SKELETON
            byte[]? skeletonHashBytes = BitConverter.GetBytes(input.ReadLong());
            if (BitConverter.IsLittleEndian && skeletonHashBytes.Length > 0) // Check endianness and reverse if necessary
                Array.Reverse(skeletonHashBytes);
            skeletonData.Skeleton.Add(
                "hash",
                Convert.ToBase64String(skeletonHashBytes).TrimEnd('=') ?? ""
            );

            skeletonData.Skeleton.Add("spine", input.ReadString() ?? "");
            if (skeletonData.Skeleton["spine"].ToString()?.Length > 13)
                return null; // early return for old 3.8 format instead of reading past the end

            skeletonData.Skeleton.Add("x", (float)Math.Round(input.ReadFloat(), 2));
            skeletonData.Skeleton.Add("y", (float)Math.Round(input.ReadFloat(), 2));
            skeletonData.Skeleton.Add("width", (float)Math.Round(input.ReadFloat(), 2));
            skeletonData.Skeleton.Add("height", (float)Math.Round(input.ReadFloat(), 2));
            skeletonData.Skeleton.Add("referenceScale", (float)Math.Round(input.ReadFloat(), 2)); // timed by 1, reference from SkeletonLoader

            bool nonEssential = input.ReadBoolean();
            if (nonEssential)
            {
                skeletonData.Skeleton.Add("fps", (float)Math.Round(input.ReadFloat(), 1));
                skeletonData.Skeleton.Add("images", input.ReadString() ?? "");
                skeletonData.Skeleton.Add("audio", input.ReadString() ?? "");
            }
            //!----- END READ SKELETON

            return skeletonData;
        }
    }
}
