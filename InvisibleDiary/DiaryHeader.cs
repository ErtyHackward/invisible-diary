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
    }
}
