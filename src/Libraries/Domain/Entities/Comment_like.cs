namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ����-�޵���
    /// </summary>
    public partial class Comment_Like : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
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
        /// ����
        /// </summary>
        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        [ForeignKey("CommentId")]
        public virtual Comment Comment { get; set; }

        /// <summary>
        /// �޴����۵���
        /// </summary>
        [ForeignKey("UserInfo")]
        public int UserInfoId { get; set; }
        [ForeignKey("UserInfoId")]
        public virtual UserInfo UserInfo { get; set; }

        #endregion
    }
}
