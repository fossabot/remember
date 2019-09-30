namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment_Like : BaseEntity
    {
        public int ID { get; set; }

        public DateTime? CreateTime { get; set; }

        #region Relationships

        [ForeignKey("Comment")]
        public int? CommentId { get; set; }
        [ForeignKey("CommentId")]
        public virtual Comment Comment { get; set; }

        [ForeignKey("UserInfo")]
        public int? UserInfoId { get; set; }
        [ForeignKey("UserInfoId")]
        public virtual UserInfo UserInfo { get; set; }

        #endregion
    }
}
