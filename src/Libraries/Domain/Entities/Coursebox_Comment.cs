namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ʵ���ࣺ�γ�-����
    /// </summary>
    public partial class CourseBox_Comment : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        #region Relationships

        [ForeignKey("CourseBox")]
        public int? CourseBoxId { get; set; }
        [ForeignKey("CourseBoxId")]
        public virtual CourseBox CourseBox { get; set; }

        [ForeignKey("Comment")]
        public int? CommentId { get; set; }
        [ForeignKey("CommentId")]
        public virtual Comment Comment { get; set; }

        #endregion
    }
}
