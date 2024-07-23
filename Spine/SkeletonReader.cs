namespace Skel2Json.Spine
{
    public class SkeletonReader
    {
        public static SkeletonData? ReadSkeletonBinary(Stream stream)
        {
            SkeletonData skeletonData = new();
            SkeletonInput input = new(stream);
            float scale = 1f;
            bool nonEssential = false;

            //!----- READ SKELETON
            byte[]? skeletonHashBytes = BitConverter.GetBytes(input.ReadLong());
            if (BitConverter.IsLittleEndian && skeletonHashBytes.Length > 0) // Check endianness and reverse if necessary
                Array.Reverse(skeletonHashBytes);
            skeletonData.Skeleton.Hash =
                Convert.ToBase64String(skeletonHashBytes).TrimEnd('=') ?? "";

            skeletonData.Skeleton.Spine = input.ReadString() ?? "";
            if (skeletonData.Skeleton.Spine.Length > 13)
                return null; // early return for old 3.8 format instead of reading past the end

            skeletonData.Skeleton.X = (float)Math.Round(input.ReadFloat(), 2);
            skeletonData.Skeleton.Y = (float)Math.Round(input.ReadFloat(), 2);
            skeletonData.Skeleton.Width = (float)Math.Round(input.ReadFloat(), 2);
            skeletonData.Skeleton.Height = (float)Math.Round(input.ReadFloat(), 2);
            skeletonData.Skeleton.ReferenceScale = (float)Math.Round(input.ReadFloat() * scale, 2); // timed by 1, reference from SkeletonLoader

            skeletonData.Skeleton.NonEssential = nonEssential = input.ReadBoolean();
            if (nonEssential)
            {
                skeletonData.Skeleton.Fps = (float)Math.Round(input.ReadFloat(), 0);
                skeletonData.Skeleton.Images = input.ReadString() ?? "";
                skeletonData.Skeleton.Audio = input.ReadString() ?? "";
            }
            //!----- END READ SKELETON

            //! WTF IS THIS?
            int n;
            string[] o;

            // Strings.
            o = input.strings = new string[n = input.ReadInt(true)];
            for (int i = 0; i < n; i++)
                o[i] = input.ReadString();

            //!----- READ BONES
            BoneData[] bones = skeletonData.Bones.Resize(n = input.ReadInt(true)).Items;
            for (int i = 0; i < n; i++)
            {
                BoneData bone =
                    new()
                    {
                        Name = input.ReadString(),
                        Parent = i == 0 ? null : bones[input.ReadInt(true)].ToString(), // Skip root (zero) parent
                        Rotation = (float)Math.Round(input.ReadFloat(), 2),
                        X = (float)Math.Round(input.ReadFloat(), 2) * scale,
                        Y = (float)Math.Round(input.ReadFloat(), 2) * scale,
                        ScaleX = (float)Math.Round(input.ReadFloat(), 2),
                        ScaleY = (float)Math.Round(input.ReadFloat(), 2),
                        ShearX = (float)Math.Round(input.ReadFloat(), 2),
                        ShearY = (float)Math.Round(input.ReadFloat(), 2),
                        Length = (float)Math.Round(input.ReadFloat(), 2) * scale,
                        Inherit = StringHelper.ToCamelCase(
                            InheritEnum.Values[input.ReadInt(true)].ToString()
                        ),
                        SkinRequired = input.ReadBoolean(),
                        NonEssential = nonEssential,
                        Color = ColorHelper.ToHex(input.ReadInt()),
                        Icon = input.ReadString(),
                        Visible = input.ReadBoolean()
                    };
                bones[i] = bone;
            }
            //!----- END READ BONES

            return skeletonData;
        }
    }
}
