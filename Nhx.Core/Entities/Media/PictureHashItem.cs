using System;

namespace Nhx.Core.Entities.Media
{
    /// <summary>
    /// Helper class for making pictures hashes from DB
    /// </summary>
    public partial class PictureHashItem : BaseEntity, IComparable, IComparable<PictureHashItem>
    {
        public int PictureId { get; set; }

        public byte[] Hash { get; set; }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as PictureHashItem);
        }

        public int CompareTo(PictureHashItem other)
        {
            return other == null ? -1 : PictureId.CompareTo(other.PictureId);
        }
    }
}