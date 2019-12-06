namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Article : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [StringLength(30)]
        public string Title { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(8000)]
        public string Content { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// �Զ���Url
        /// </summary>
        [StringLength(1000)]
        [Column(TypeName = "text")]
        public string CustomUrl { get; set; }

        /// <summary>
        /// ����״̬
        /// </summary>
        public AStatus ArticleStatus { get; set; }

        #region Relationships

        /// <summary>
        /// ����
        /// </summary>
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [ForeignKey("AuthorId")]
        public virtual UserInfo Author { get; set; }

        /// <summary>
        /// ɾ��ʱ�䣺Ϊnull����δɾ��
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        public bool IsDeleted { get; set; }

        #endregion

        #region Helpers

        public enum AStatus
        {
            /// <summary>
            /// ������
            /// </summary>
            Published = 0,

            /// <summary>
            /// �༭�У��ݸ�״̬��
            /// </summary>
            Editing = 1,
        }

        #endregion
    }
}
