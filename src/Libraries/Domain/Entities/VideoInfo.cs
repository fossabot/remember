namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ��Ƶ�μ�
    /// </summary>
    public partial class VideoInfo : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [StringLength(100)]
        [Column(TypeName = "text")]
        public string Title { get; set; }

        /// <summary>
        /// ��Ƶ: ��Ƶurl
        /// </summary>
        [Column(TypeName = "text")]
        [Required]
        [StringLength(2000)]
        public string PlayUrl { get; set; }

        /// <summary>
        /// ��Ƶ����Ļurl
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(100)]
        public string SubTitleUrl { get; set; }

        /// <summary>
        /// ����ʱ��
        /// ����
        /// ��Ƶ���˿μ����貥��ʱ��
        /// ���ӣ��Ķ�������ʱ��
        /// </summary>
        public long Duration { get; set; }

        /// <summary>
        /// ��Ƶ�ļ���С
        /// �ֽ�B
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// ������
        /// ����Ƶ���ڿγ̵ĵڼ���/ҳ
        /// </summary>
        public int Page { get; set; }

        #region Relationships

        /// <summary>
        /// ���� CourseBox
        ///     ���һ
        /// </summary>
        [ForeignKey("CourseBox")]
        public int? CourseBoxId { get; set; }
        [ForeignKey("CourseBoxId")]
        public virtual CourseBox CourseBox { get; set; }

        #endregion
    }
}
