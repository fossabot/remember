namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    /// <summary>
    /// �Ŀ�
    /// </summary>
    public partial class BookInfo : BaseEntity
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
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// �������ʱ��
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// �Ƿ񹫿�
        /// </summary>
        public bool IsOpen { get; set; }

        public int LikeNum { get; set; }

        public int DislikeNum { get; set; }

        public int CommentNum { get; set; }

        public int ShareNum { get; set; }

        #region Relationships

        /// <summary>
        /// �Ŀ�Ĵ�����
        ///     ���һ
        /// </summary>
        [ForeignKey("Creator")]
        public int CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public virtual UserInfo Creator { get; set; }

        /// <summary>
        /// �γ̰�������Ƶ���б�
        ///     һ�Զ�
        /// </summary>
        public virtual ICollection<BookSection> BookSections { get; set; }

        /// <summary>
        /// ������Щ�ղؼ�
        /// </summary>
        public virtual ICollection<Favorite_BookInfo> Favorite_BookInfos { get; set; }

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

        /// <summary>
        /// ѧϰ�˿γ�����ʱ��
        /// </summary>
        [NotMapped]
        public long Duration
        {
            get
            {
                long duration = 0;
                if (this.BookSections != null && this.BookSections.Count >= 1)
                {
                    duration = this.BookSections.Select(m => m.Duration).Sum();
                }

                return duration;
            }
        }

        [NotMapped]
        public IList<Favorite> Favorites
        {
            get
            {
                IList<Favorite> favorites = new List<Favorite>();
                if (this.Favorite_BookInfos != null && this.Favorite_BookInfos.Count >= 1)
                {
                    favorites = this.Favorite_BookInfos.Select(m => m.Favorite).ToList();
                }

                return favorites;
            }
        }

        #endregion
    }
}
