namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Serializable]
    public partial class Role_Function : BaseEntity
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
        public int? OperatorId { get; set; }
        [ForeignKey("OperatorId")]
        public virtual UserInfo Operator { get; set; }

        public int RoleInfoId { get; set; }
        [ForeignKey("RoleInfoId")]
        public virtual RoleInfo RoleInfo { get; set; }

        public int FunctionInfoId { get; set; }
        [ForeignKey("FunctionInfoId")]
        public virtual FunctionInfo FunctionInfo { get; set; }

        #endregion
    }
}
