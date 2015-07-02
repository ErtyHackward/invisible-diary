using System.Collections.Generic;
using System.Linq;
using ProtoBuf;

namespace InvisibleDiary
{
    [ProtoContract]
    public class DiaryHeader
    {
        [ProtoMember(1)]
        public byte[] PublicKey { get; set; }

        [ProtoMember(2)]
        public byte[] PrivateKeyEncrypted { get; set; }

        public override bool Equals(object obj)
        {
            var header = obj as DiaryHeader;

            if (header == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;
            
            return ByteArrayCompare(PublicKey, header.PublicKey) && ByteArrayCompare(PrivateKeyEncrypted, header.PrivateKeyEncrypted);
        }

        static bool ByteArrayCompare(byte[] a1, byte[] a2)
        {

            if (a1 == null && a2 == null)
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            for (int i = 0; i < a1.Length; i++)
                if (a1[i] != a2[i])
                    return false;

            return true;
        }
    }
}
