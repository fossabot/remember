namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// �Ŀ�-�ȵ���
    /// </summary>
    public partial class BookInfo_Dislike : BaseEntity
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

        /// <summary>
        /// �Ŀ�
        /// </summary>
        [ForeignKey("BookInfo")]
        public int BookInfoId { get; set; }
        [ForeignKey("BookInfoId")]
        public virtual BookInfo BookInfo { get; set; }

        /// <summary>
        /// �ȴ��Ŀ����
        /// </summary>
        [ForeignKey("UserInfo")]
        public int UserInfoId { get; set; }
        [ForeignKey("UserInfoId")]
        public virtual UserInfo UserInfo { get; set; }

        #endregion
    }
}
