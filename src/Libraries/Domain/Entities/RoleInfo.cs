namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ʵ����: ��ɫ
    /// </summary>
    public partial class RoleInfo : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// ��ɫ��
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        [StringLength(30)]
        public string Remark { get; set; }


        #region Relationships

        /// <summary>
        /// ��ɫ-�û�
        /// </summary>
        public virtual ICollection<Role_User> Role_Users { get; set; }

        /// <summary>
        /// ��ɫ-�˵�
        /// </summary>
        public virtual ICollection<Role_Menu> Role_Menus { get; set; }

        /// <summary>
        /// ��ɫ-Ȩ��
        /// </summary>
        public virtual ICollection<Role_Function> Role_Functions { get; set; }

        #endregion
    }
}
