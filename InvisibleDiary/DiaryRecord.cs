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

        protected bool Equals(DiaryRecord other)
        {
            return string.Equals(Title, other.Title) && Created.Equals(other.Created) && string.Equals(Content, other.Content) && string.Equals(Tags, other.Tags);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DiaryRecord)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Title != null ? Title.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Created.GetHashCode();
                hashCode = (hashCode * 397) ^ (Content != null ? Content.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Tags != null ? Tags.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(DiaryRecord left, DiaryRecord right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DiaryRecord left, DiaryRecord right)
        {
            return !Equals(left, right);
        }
    }
}
