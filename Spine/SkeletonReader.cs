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
                o[i] = input.ReadString() ?? "";

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
                        Name = input.ReadString() ?? "",
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
                IKConstraintData ik =
                    new()
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

            //!----- READ TRANSFORM CONSTRAINTS
            TransformConstraintData[] transformConstraints = skeletonData
                .TransformConstraints.Resize(n = input.ReadInt(true))
                .Items;
            for (int i = 0; i < n; i++)
            {
                TransformConstraintData transform =
                    new()
                    {
                        Name = input.ReadString() ?? "",
                        Order = input.ReadInt(true),
                        Bones = Enumerable
                            .Range(0, input.ReadInt(true))
                            .Select(_ => bones[input.ReadInt(true)].ToString())
                            .ToList(),
                        Target = bones[input.ReadInt(true)].ToString()
                    };
                int flags = input.Read();
                transform.SkinRequired = (flags & 1) != 0;
                transform.Local = (flags & 2) != 0;
                transform.Relative = (flags & 4) != 0;
                transform.OffsetRotation = (float)
                    Math.Round((flags & 8) != 0 ? input.ReadFloat() : transform.OffsetRotation, 2);
                transform.OffsetX = (float)
                    Math.Round(
                        (flags & 16) != 0 ? input.ReadFloat() * scale : transform.OffsetX,
                        2
                    );
                transform.OffsetY = (float)
                    Math.Round(
                        (flags & 32) != 0 ? input.ReadFloat() * scale : transform.OffsetY,
                        2
                    );
                transform.OffsetScaleX = (float)
                    Math.Round((flags & 64) != 0 ? input.ReadFloat() : transform.OffsetScaleX, 2);
                transform.OffsetScaleY = (float)
                    Math.Round((flags & 128) != 0 ? input.ReadFloat() : transform.OffsetScaleY, 2);
                flags = input.Read();
                transform.OffsetShearY = (float)
                    Math.Round((flags & 1) != 0 ? input.ReadFloat() : transform.OffsetShearY, 2);
                transform.MixRotate = (float)
                    Math.Round((flags & 2) != 0 ? input.ReadFloat() : transform.MixRotate, 2);
                transform.MixX = (float)
                    Math.Round((flags & 4) != 0 ? input.ReadFloat() : transform.MixX, 2);
                transform.MixY = (float)
                    Math.Round((flags & 8) != 0 ? input.ReadFloat() : transform.MixY, 2);
                transform.MixScaleX = (float)
                    Math.Round((flags & 16) != 0 ? input.ReadFloat() : transform.MixScaleX, 2);
                transform.MixScaleY = (float)
                    Math.Round((flags & 32) != 0 ? input.ReadFloat() : transform.MixScaleY, 2);
                transform.MixShearY = (float)
                    Math.Round((flags & 64) != 0 ? input.ReadFloat() : transform.MixShearY, 2);
                transformConstraints[i] = transform;
            }
            skeletonData.TransformConstraints.Items = transformConstraints;
            //!----- END OF READ TRANSFORM CONSTRAINTS

            //!----- READ PATH CONSTRAINTS
            PathConstraintData[] pathConstraints = skeletonData
                .PathConstraints.Resize(n = input.ReadInt(true))
                .Items;
            for (int i = 0; i < n; i++)
            {
                PathConstraintData path =
                    new()
                    {
                        Name = input.ReadString() ?? "",
                        Order = input.ReadInt(true),
                        SkinRequired = input.ReadBoolean(),
                        Bones = Enumerable
                            .Range(0, input.ReadInt(true))
                            .Select(_ => bones[input.ReadInt(true)].ToString())
                            .ToList(),
                        Target = slots[input.ReadInt(true)].ToString()
                    };
                int flags = input.Read();
                path.PositionMode = PositionModeEnum.Values[flags & 1].ToString().ToLower();
                path.SpacingMode = SpacingModeEnum.Values[(flags >> 1) & 3].ToString().ToLower();
                path.RotateMode = RotateModeEnum.Values[(flags >> 3) & 3].ToString().ToLower();
                path.OffsetRotation = (float)
                    Math.Round((flags & 128) != 0 ? input.ReadFloat() : path.OffsetRotation, 2);
                path.Position = (float)
                    Math.Round(
                        path.PositionMode == "fixed"
                            ? input.ReadFloat() * scale
                            : input.ReadFloat(),
                        2
                    );
                path.Spacing = (float)
                    Math.Round(
                        (path.SpacingMode == "length" || path.SpacingMode == "fixed")
                            ? input.ReadFloat() * scale
                            : input.ReadFloat(),
                        2
                    );
                path.MixRotate = (float)Math.Round(input.ReadFloat(), 2);
                path.MixX = (float)Math.Round(input.ReadFloat(), 2);
                path.MixY = (float)Math.Round(input.ReadFloat(), 2);
                pathConstraints[i] = path;
            }
            skeletonData.PathConstraints.Items = pathConstraints;
            //!----- END OF PATH CONSTRAINTS

            //!----- READ PHYSICS CONSTRAINTS
            PhysicsConstraintData[] physicsConstraints = skeletonData
                .PhysicsConstraints.Resize(n = input.ReadInt(true))
                .Items;
            for (int i = 0; i < n; i++)
            {
                PhysicsConstraintData physic =
                    new()
                    {
                        Name = input.ReadString() ?? "",
                        Order = input.ReadInt(true),
                        Bone = bones[input.ReadInt(true)].ToString()
                    };
                int flags = input.Read();
                physic.SkinRequired = (flags & 1) != 0;
                physic.X = (flags & 2) != 0 ? input.ReadFloat() : physic.X;
                physic.Y = (flags & 4) != 0 ? input.ReadFloat() : physic.Y;
                physic.Rotate = (flags & 8) != 0 ? input.ReadFloat() : physic.Rotate;
                physic.ScaleX = (flags & 16) != 0 ? input.ReadFloat() : physic.ScaleX;
                physic.ShearX = (flags & 32) != 0 ? input.ReadFloat() : physic.ShearX;
                physic.Limit = ((flags & 64) != 0 ? input.ReadFloat() : physic.Limit) * scale;
                physic.Step = (int)(1f / input.ReadUByte());
                physic.Inertia = input.ReadFloat();
                physic.Strength = input.ReadFloat();
                physic.Damping = input.ReadFloat();
                physic.MassInverse = (flags & 128) != 0 ? input.ReadFloat() : physic.MassInverse;
                physic.Wind = input.ReadFloat();
                physic.Gravity = input.ReadFloat();
                flags = input.Read();
                physic.InertiaGlobal = (flags & 1) != 0;
                physic.StrengthGlobal = (flags & 2) != 0;
                physic.DampingGlobal = (flags & 4) != 0;
                physic.MassGlobal = (flags & 8) != 0;
                physic.WindGlobal = (flags & 16) != 0;
                physic.GravityGlobal = (flags & 32) != 0;
                physic.MixGlobal = (flags & 64) != 0;
                physic.Mix = (flags & 128) != 0 ? input.ReadFloat() : physic.Mix;
                physicsConstraints[i] = physic;
            }
            skeletonData.PhysicsConstraints.Items = physicsConstraints;
            //!----- END OF READ PHYSICS CONSTRAINTS

            return skeletonData;
        }
    }
}
