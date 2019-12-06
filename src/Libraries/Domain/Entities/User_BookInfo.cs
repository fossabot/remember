namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ʵ���ࣺ�û�-�Ŀ�
    /// </summary>
    public partial class User_BookInfo : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// ����ʱ��-�û���ʼ�Ķ����Ŀ��ʱ��
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// ���û��ڴ��Ŀ����Ķ�ʱ��: ����ʱ��
        /// ��
        /// </summary>
        public long SpendTime { get; set; }

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
        /// ����Ķ��½�
        /// </summary>
        [ForeignKey("LastViewSection")]
        public int? LastViewSectionId { get; set; }
        [ForeignKey("LastViewSectionId")]
        public virtual BookSection LastViewSection { get; set; }

        /// <summary>
        /// �û�-�Ķ���
        /// </summary>
        [ForeignKey("Reader")]
        public int ReaderId { get; set; }
        [ForeignKey("Reader")]
        public virtual UserInfo Reader { get; set; }

        /// <summary>
        /// �Ŀ�
        /// </summary>
        [ForeignKey("BookInfo")]
        public int BookInfoId { get; set; }
        [ForeignKey("BookInfoId")]
        public virtual BookInfo BookInfo { get; set; }

        #endregion
    }
}
