namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ʵ���ࣺ֪ʶ��Ƭ
    /// </summary>
    public partial class CardInfo : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Column(TypeName = "text")]
        [Required]
        [StringLength(30)]
        public string Content { get; set; }

        #region Relationships

        /// <summary>
        /// ���� CardBox
        ///     ���һ
        /// </summary>
        [ForeignKey("CardBox")]
        public int? CardBoxId { get; set; }
        /// <summary>
        /// ���� CardBox
        ///     ���һ
        /// </summary>
        [ForeignKey("CardBoxId")]
        public virtual CardBox CardBox { get; set; }

        #endregion
    }
}
