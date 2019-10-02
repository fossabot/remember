namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ThemeTemplate : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// ģ����
        /// </summary>
        [Required]
        [StringLength(30)]
        [Column(TypeName = "text")]
        public string TemplateName { get; set; }

        /// <summary>
        /// ģ�����
        /// </summary>
        [Required]
        [StringLength(30)]
        [Column(TypeName = "text")]
        public string Title { get; set; }

        /// <summary>
        /// ״̬
        ///     0: ����
        ///     1: ����
        /// </summary>
        public int? IsOpen { get; set; }
    }
}
