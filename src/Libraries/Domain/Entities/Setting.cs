namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Setting : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// ��
        /// </summary>
        [Required]
        [StringLength(30)]
        [Column(TypeName = "text")]
        public string SetKey { get; set; }

        /// <summary>
        /// ֵ
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(30)]
        public string SetValue { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        [StringLength(30)]
        public string Name { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(30)]
        public string Remark { get; set; }
    }
}
