using Skel2Json.Spine.Helpers;

namespace Skel2Json.Spine
{
    public class SkeletonReader
    {
        public static SkeletonData? ReadSkeletonBinary(Stream stream)
        {
            SkeletonData skeletonData = new();
            SkeletonInput? input = new(stream);
            float scale = 1f;

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
            skeletonData.Skeleton.ReferenceScale = (float)Math.Round(input.ReadFloat() * scale, 2);

            bool nonEssential;
            skeletonData.Skeleton.NonEssential = nonEssential = input.ReadBoolean();
            if (nonEssential)
            {
                skeletonData.Skeleton.Fps = (float)Math.Round(input.ReadFloat(), 0);
                skeletonData.Skeleton.Images = input.ReadString() ?? "";
                skeletonData.Skeleton.Audio = input.ReadString() ?? "";
            }
            //!----- END OF READ SKELETON

            //! WTF IS THIS?
            int n;
            object[]? o;

            // Strings.
            o = input.strings = new string[n = input.ReadInt(true)];
            for (int i = 0; i < n; i++)
                o[i] = input.ReadString() ?? null;

            //!----- READ BONES
            BoneData[] bones = skeletonData.Bones.Resize(n = input.ReadInt(true)).Items;
            for (int i = 0; i < n; i++)
            {
                BoneData bone =
                    new()
                    {
                        Name = input.ReadString() ?? "",
                        Parent = i == 0 ? "" : bones[input.ReadInt(true)].ToString(), // Skip root (zero) parent
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
                        Color = ColorHelper.ToRGBA(input.ReadInt()),
                        Icon = input.ReadString(),
                        Visible = input.ReadBoolean()
                    };
                bones[i] = bone;
            }
            skeletonData.Bones.Items = bones;
            //!----- END OF READ BONES

            //!----- READ SLOTS
            SlotData[] slots = skeletonData.Slots.Resize(n = input.ReadInt(true)).Items;
            for (int i = 0; i < n; i++)
            {
                SlotData slot =
                    new()
                    {
                        Name = input.ReadString(),
                        Bone = bones[input.ReadInt(true)].ToString(),
                        Color = ColorHelper.ToRGBA(input.ReadInt()),
                        DarkColor = ColorHelper.ToRGB(input.ReadInt()),
                        Attachment = input.ReadStringRef(),
                        Blend = StringHelper.ToLowerCase(
                            BlendModeEnum.Values[input.ReadInt(true)].ToString()
                        ),
                        NonEssential = nonEssential,
                        Visible = input.ReadBoolean()
                    };
                slots[i] = slot;
            }
            skeletonData.Slots.Items = slots;
            //!----- END OF READ SLOTS

            //!----- READ IK CONSTRAINTS
            IKConstraintData[] iKConstraints = skeletonData
                .IKConstraints.Resize(n = input.ReadInt(true))
                .Items;
            for (int i = 0; i < n; i++)
            {
                IKConstraintData ik = new()
                {
                    Name = input.ReadString(),
                    Order = input.ReadInt(true),
                    Bones = Enumerable
                    .Range(0, input.ReadInt(true))
                    .Select(_ => bones[input.ReadInt(true)].ToString())
                    .ToList(),
                    Target = bones[input.ReadInt(true)].ToString()
                };
                int flags = input.Read();
                ik.SkinRequired = (flags & 1) != 0;
                ik.BendDirection = (flags & 2) != 0;
                ik.Compress = (flags & 4) != 0;
                ik.Stretch = (flags & 8) != 0;
                ik.Uniform = (flags & 16) != 0;
                ik.Mix = (flags & 32) != 0 ? ((flags & 64) != 0 ? input.ReadFloat() : 1) : ik.Mix;
                ik.Softness = (flags & 128) != 0 ? input.ReadFloat() * scale : ik.Softness;
                iKConstraints[i] = ik;
            }
            skeletonData.IKConstraints.Items = iKConstraints;
            //!----- END OF READ IK CONSTRAINTS

            return skeletonData;
        }
    }
}
