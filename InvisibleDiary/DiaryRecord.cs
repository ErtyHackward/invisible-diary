using System;
using ProtoBuf;

namespace InvisibleDiary
{
    [ProtoContract]
    public class DiaryRecord
    {
        [ProtoMember(1)]
        public string Title { get; set; }

        [ProtoMember(2)]
        public DateTime Created { get; set; }

        [ProtoMember(3)]
        public string Content { get; set; }

        [ProtoMember(4)]
        public string Tags { get; set; }
    }
}
