namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    /// <summary>
    /// �ղؼ�
    /// </summary>
    public partial class Favorite : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// �ղؼ���
        /// </summary>
        [StringLength(30)]
        public string Name { get; set; }

        /// <summary>
        /// �ղؼ�����
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// �Ƿ񹫿�
        /// </summary>
        public bool? IsOpen { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? CreateTime { get; set; }

        #region Relationships

        /// <summary>
        /// �ղؼеĴ�����
        /// </summary>
        [ForeignKey("Creator")]
        public int? CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public virtual UserInfo Creator { get; set; }

        /// <summary>
        /// �ղصĿγ��б�
        /// </summary>
        public virtual ICollection<Favorite_CourseBox> Favorite_CourseBoxes { get; set; }

        /// <summary>
        /// �ղصĿ�Ƭ���б�
        /// </summary>
        public virtual ICollection<Favorite_CardBox> Favorite_CardBoxes { get; set; }

        #endregion

        #region Helpers

        [NotMapped]
        public IList<CourseBox> CourseBoxes
        {
            get
            {
                IList<CourseBox> courseBoxes = new List<CourseBox>();
                if (this.Favorite_CourseBoxes != null && this.Favorite_CourseBoxes.Count >= 1)
                {
                    courseBoxes = this.Favorite_CourseBoxes.Select(m => m.CourseBox)?.ToList();
                }

                return courseBoxes;
            }
        }

        #endregion
    }
}
