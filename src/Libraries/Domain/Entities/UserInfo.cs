namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserInfo : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// �û���(Ψһ���ɸģ�����Ϊ��¼ʹ��)
        /// </summary>
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Required]
        [StringLength(60)]
        public string Password { get; set; }

        /// <summary>
        /// ˢ��Toke
        /// </summary>
        // TODO: ʵ���Ѿ�����,���Ժ�ʵ�� OAuth2.0
        [StringLength(100)]
        [Column(TypeName = "text")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// ����¼ʱ��
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// ѡ�������ģ��
        /// </summary>
        [StringLength(100)]
        [Column(TypeName = "text")]
        public string TemplateName { get; set; }

        /// <summary>
        /// �û�ͷ��Url��ַ
        /// </summary>
        [StringLength(100)]
        [Column(TypeName = "text")]
        public string Avatar { get; set; }

        /// <summary>
        /// ����(Ψһ���ɸģ�����Ϊ��¼ʹ��)
        /// </summary>
        [StringLength(30)]
        public string Email { get; set; }

        /// <summary>
        /// �ֻ���(Ψһ���ɸģ�����Ϊ��¼ʹ��)
        /// </summary>
        [StringLength(30)]
        public string Phone { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// Ӳ����
        /// </summary>
        public long? Coin { get; set; }

        /// <summary>
        /// ע��ʱ��
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(30)]
        public string Remark { get; set; }

        #region Relationships

        /// <summary>
        /// ��ɫ-�û�
        /// </summary>
        public virtual ICollection<Role_User> Role_Users { get; set; }

        #endregion
    }
}
