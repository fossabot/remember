namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RoleInfo : BaseEntity
    {
        public int ID { get; set; }

        /// <summary>
        /// ��ɫ��
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public bool? IsLog { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }


        #region Relationships

        /// <summary>
        /// �û��б�
        ///     ��Զ��ϵ
        /// </summary>
        public virtual ICollection<UserInfo> UserInfos { get; set; }

        /// <summary>
        /// ��ɫ�˵�Ȩ��
        ///     ��Զ��ϵ
        /// </summary>
        public virtual ICollection<Sys_Menu> Sys_Menus { get; set; }

        /// <summary>
        /// ��ɫ����Ȩ��
        ///     ��Զ��ϵ
        /// </summary>
        public virtual ICollection<FunctionInfo> FunctionInfos { get; set; }

        #endregion
    }
}
