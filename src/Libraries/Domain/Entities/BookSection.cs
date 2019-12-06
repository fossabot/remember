namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// �Ŀ�-�½�����
    /// </summary>
    public partial class BookSection : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// �½ڱ���
        /// </summary>
        [StringLength(100)]
        [Column(TypeName = "text")]
        public string Title { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column(TypeName = "text")]
        [Required]
        [StringLength(8000)]
        public string Content { get; set; }

        /// <summary>
        /// ����ʱ��
        /// ��
        /// �Ķ���������ʱ��
        /// </summary>
        public long Duration { get; set; }

        /// <summary>
        /// ������
        /// �ڼ��½�
        /// </summary>
        public int SortCode { get; set; }

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
        /// ���� BookInfo
        ///     ���һ
        /// </summary>
        [ForeignKey("BookInfo")]
        public int BookInfoId { get; set; }
        [ForeignKey("BookInfoId")]
        public virtual BookInfo BookInfo { get; set; }

        #endregion
    }
}
