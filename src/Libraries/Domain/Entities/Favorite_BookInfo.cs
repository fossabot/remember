namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// �ղؼ�-�Ŀ�
    /// </summary>
    public partial class Favorite_BookInfo : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// ɾ��ʱ�䣺Ϊnull����δɾ��
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        public bool IsDeleted { get; set; }

        #region Relationships

        [ForeignKey("Favorite")]
        public int FavoriteId { get; set; }
        [ForeignKey("FavoriteId")]
        public virtual Favorite Favorite { get; set; }

        [ForeignKey("BookInfo")]
        public int BookInfoId { get; set; }
        [ForeignKey("BookInfoId")]
        public virtual BookInfo BookInfo { get; set; }

        #endregion
    }
}
