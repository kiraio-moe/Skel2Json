namespace Skel2Json.Spine
{
    // Obtained from https://github.com/EsotericSoftware/spine-runtimes/blob/1aa50ca9684d42c24ccdf1c0d76d7cfdfaa45325/spine-csharp/src/SkeletonBinary.cs#L1213
    public class SkeletonInput(Stream input)
    {
        private readonly byte[] chars = new byte[32];
        private readonly byte[] bytesBigEndian = new byte[8];
        internal string[]? strings;
        readonly Stream input = input;

        public int Read()
        {
            return input.ReadByte();
        }

        /// <summary>Explicit unsigned byte variant to prevent pitfalls porting Java reference implementation
        /// where byte is signed vs C# where byte is unsigned.</summary>
        public byte ReadUByte()
        {
            return (byte)input.ReadByte();
        }

        /// <summary>Explicit signed byte variant to prevent pitfalls porting Java reference implementation
        /// where byte is signed vs C# where byte is unsigned.</summary>
        public sbyte ReadSByte()
        {
            int value = input.ReadByte();
            if (value == -1)
                throw new EndOfStreamException();
            return (sbyte)value;
        }

        public bool ReadBoolean()
        {
            return input.ReadByte() != 0;
        }

        public float ReadFloat()
        {
            input.Read(bytesBigEndian, 0, 4);
            chars[3] = bytesBigEndian[0];
            chars[2] = bytesBigEndian[1];
            chars[1] = bytesBigEndian[2];
            chars[0] = bytesBigEndian[3];
            return BitConverter.ToSingle(chars, 0);
        }

        public int ReadInt()
        {
            input.Read(bytesBigEndian, 0, 4);
            return (bytesBigEndian[0] << 24)
                + (bytesBigEndian[1] << 16)
                + (bytesBigEndian[2] << 8)
                + bytesBigEndian[3];
        }

        public long ReadLong()
        {
            input.Read(bytesBigEndian, 0, 8);
            return ((long)bytesBigEndian[0] << 56)
                + ((long)bytesBigEndian[1] << 48)
                + ((long)bytesBigEndian[2] << 40)
                + ((long)bytesBigEndian[3] << 32)
                + ((long)bytesBigEndian[4] << 24)
                + ((long)bytesBigEndian[5] << 16)
                + ((long)bytesBigEndian[6] << 8)
                + bytesBigEndian[7];
        }

        public int ReadInt(bool optimizePositive)
        {
            int b = input.ReadByte();
            int result = b & 0x7F;
            if ((b & 0x80) != 0)
            {
                b = input.ReadByte();
                result |= (b & 0x7F) << 7;
                if ((b & 0x80) != 0)
                {
                    b = input.ReadByte();
                    result |= (b & 0x7F) << 14;
                    if ((b & 0x80) != 0)
                    {
                        b = input.ReadByte();
                        result |= (b & 0x7F) << 21;
                        if ((b & 0x80) != 0)
                            result |= (input.ReadByte() & 0x7F) << 28;
                    }
                }
            }
            return optimizePositive ? result : ((result >> 1) ^ -(result & 1));
        }

        public string? ReadString()
        {
            int byteCount = ReadInt(true);
            switch (byteCount)
            {
                case 0:
                    return null;
                case 1:
                    return "";
            }
            byteCount--;
            byte[] buffer = this.chars;
            if (buffer.Length < byteCount)
                buffer = new byte[byteCount];
            ReadFully(buffer, 0, byteCount);
            return System.Text.Encoding.UTF8.GetString(buffer, 0, byteCount);
        }

        /// <return>May be null.</return>
        public string? ReadStringRef()
        {
            int index = ReadInt(true);
            return index == 0 ? null : strings?[index - 1];
        }

        public void ReadFully(byte[] buffer, int offset, int length)
        {
            while (length > 0)
            {
                int count = input.Read(buffer, offset, length);
                if (count <= 0)
                    throw new EndOfStreamException();
                offset += count;
                length -= count;
            }
        }

        /// <summary>Returns the version string of binary skeleton data.</summary>
        public string GetVersionString()
        {
            try
            {
                // try reading 4.0+ format
                long initialPosition = input.Position;
                ReadLong(); // long hash

                long stringPosition = input.Position;
                int stringByteCount = ReadInt(true);
                input.Position = stringPosition;
                if (stringByteCount <= 13)
                {
                    string? version = ReadString();
                    if (char.IsDigit(version[0]))
                        return version;
                }
                // fallback to old version format
                input.Position = initialPosition;
                return GetVersionStringOld3X();
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    "Stream does not contain valid binary Skeleton Data.\n" + e,
                    "input"
                );
            }
        }

        /// <summary>Returns old 3.8 and earlier format version string of binary skeleton data.</summary>
        public string GetVersionStringOld3X()
        {
            // Hash.
            int byteCount = ReadInt(true);
            if (byteCount > 1)
                input.Position += byteCount - 1;

            // Version.
            byteCount = ReadInt(true);
            if (byteCount > 1 && byteCount <= 13)
            {
                byteCount--;
                byte[] buffer = new byte[byteCount];
                ReadFully(buffer, 0, byteCount);
                return System.Text.Encoding.UTF8.GetString(buffer, 0, byteCount);
            }
            throw new ArgumentException("Stream does not contain valid binary Skeleton Data.");
        }
    }
}
