namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ʵ���ࣺ��������Ϣ
    /// </summary>
    public partial class ParticipantInfo : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// ���ν�ɫ����
        /// eg: ["����", "����", "����"]
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(30)]
        public string RoleNames { get; set; }

        /// <summary>
        /// ��������
        /// <para>�ڴ˴���������ʲô</para>
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(30)]
        public string Description { get; set; }
    }
}
