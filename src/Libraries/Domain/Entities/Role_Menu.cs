namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Role_Menu : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// ��Ȩʱ��
        /// </summary>
        public DateTime? CreateTime { get; set; }

        #region Relationships

        /// <summary>
        /// ��Ȩ��/������
        /// </summary>
        [ForeignKey("Operator")]
        public int OperatorId { get; set; }
        [ForeignKey("OperatorId")]
        public virtual UserInfo Operator { get; set; }

        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual RoleInfo RoleInfo { get; set; }

        public int MenuId { get; set; }
        [ForeignKey("MenuId")]
        public virtual Sys_Menu Sys_Menu { get; set; }

        #endregion
    }
}
