namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class CourseBox : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// �γ���
        /// </summary>
        [Required]
        [Column(TypeName = "text")]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(2000)]
        public string Description { get; set; }

        /// <summary>
        /// ����ͼ
        /// </summary>
        [StringLength(100)]
        [Column(TypeName = "text")]
        public string PicUrl { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// �������ʱ��
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// �Ƿ񹫿�
        /// </summary>
        public bool? IsOpen { get; set; }

        /// <summary>
        /// ��Ч���� - ��ʼʱ��
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// ��Ч���� - ����ʱ��
        /// </summary>
        public DateTime? EndTime { get; set; }

        public int? LikeNum { get; set; }

        public int? DislikeNum { get; set; }

        public int? CommentNum { get; set; }

        public int? ShareNum { get; set; }

        #region Relationships

        /// <summary>
        /// �γ̵Ĵ�����
        ///     ���һ
        /// </summary>
        [ForeignKey("Creator")]
        public int? CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public virtual UserInfo Creator { get; set; }

        /// <summary>
        /// �γ̰�������Ƶ���б�
        ///     һ�Զ�
        /// </summary>
        public virtual ICollection<VideoInfo> VideoInfos { get; set; }

        /// <summary>
        /// ������Щ�ղؼ�
        /// </summary>
        public virtual ICollection<Favorite_CourseBox> Favorite_CourseBoxes { get; set; }

        #endregion

        #region Helpers

        /// <summary>
        /// ѧϰ�˿γ�����ʱ��
        /// </summary>
        public long Duration
        {
            get
            {
                long duration = 0;
                if (this.VideoInfos != null && this.VideoInfos.Count >= 1)
                {
                    duration = this.VideoInfos.Select(m => m.Duration ?? 0).Sum();
                }

                return duration;
            }
        }

        #endregion
    }
}
