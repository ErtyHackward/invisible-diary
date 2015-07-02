using System;
using System.Collections.Generic;
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

        private sealed class DiaryRecordEqualityComparer : IEqualityComparer<DiaryRecord>
        {
            public bool Equals(DiaryRecord x, DiaryRecord y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return string.Equals(x.Title, y.Title) && x.Created.Equals(y.Created) && string.Equals(x.Content, y.Content) && string.Equals(x.Tags, y.Tags);
            }

            public int GetHashCode(DiaryRecord obj)
            {
                unchecked
                {
                    int hashCode = (obj.Title != null ? obj.Title.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ obj.Created.GetHashCode();
                    hashCode = (hashCode * 397) ^ (obj.Content != null ? obj.Content.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.Tags != null ? obj.Tags.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }

        private static readonly IEqualityComparer<DiaryRecord> DiaryRecordComparerInstance = new DiaryRecordEqualityComparer();

        public static IEqualityComparer<DiaryRecord> DiaryRecordComparer
        {
            get { return DiaryRecordComparerInstance; }
        }
    }
}
