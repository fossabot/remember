namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ʵ���ࣺ��Ƭ�У����кܶ࿨Ƭ
    /// </summary>
    public partial class CardBox : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(30)]
        public string Description { get; set; }

        /// <summary>
        /// ����ͼ
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(30)]
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

        #region Relationships

        /// <summary>
        /// ��Ƭ�еĴ�����
        ///     ���һ
        /// </summary>
        [ForeignKey("Creator")]
        public int? CreatorId { get; set; }
        /// <summary>
        /// ��Ƭ�еĴ�����
        ///     ���һ
        /// </summary>
        [ForeignKey("CreatorId")]
        public virtual UserInfo Creator { get; set; }

        #endregion
    }
}
