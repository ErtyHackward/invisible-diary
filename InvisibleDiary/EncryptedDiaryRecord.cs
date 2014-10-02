using ProtoBuf;

namespace InvisibleDiary
{
    [ProtoContract]
    public class EncryptedDiaryRecord
    {
        [ProtoMember(1)]
        public byte[] Header { get; set; }
        [ProtoMember(2)]
        public byte[] Body { get; set; }
    }
}
