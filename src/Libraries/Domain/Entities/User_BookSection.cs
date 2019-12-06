namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ʵ���ࣺѧϰ��-��Ƶ�μ�
    /// </summary>
    public partial class User_BookSection : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// ������IP
        /// </summary>
        [StringLength(20)]
        public string LastAccessIp { get; set; }

        /// <summary>
        /// ����Ķ�λ��
        /// �Ķ��ڵڼ����ַ�
        /// </summary>
        public long LastViewAt { get; set; }

        /// <summary>
        /// ����Ķ�ʱ��
        /// </summary>
        public DateTime LastViewTime { get; set; }

        /// <summary>
        /// �Ķ�����-��Զ���Ķ�λ��
        /// ������Ϊ�ض�������ʧ����
        /// </summary>
        public long ProgressAt { get; set; }

        /// <summary>
        /// ɾ��ʱ�䣺Ϊnull����δɾ��
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        public bool IsDeleted { get; set; }

        #region Relationships

        /// <summary>
        /// �Ķ���
        /// </summary>
        [ForeignKey("Reader")]
        public int ReaderId { get; set; }
        [ForeignKey("ReaderId")]
        public virtual UserInfo Reader { get; set; }

        /// <summary>
        /// �Ŀ��½�
        /// </summary>
        [ForeignKey("BookSection")]
        public int BookSectionId { get; set; }
        [ForeignKey("BookSectionId")]
        public virtual BookSection BookSection { get; set; }

        #endregion

        #region Helpers

        /// <summary>
        /// ���Ȱٷֱ�
        /// Ϊ1�������
        /// </summary>
        [NotMapped]
        public float Percent
        {
            get
            {
                float percent = 0;
                if (this.BookSection != null && this.BookSection.Content.Length != 0)
                {
                    percent = (float)ProgressAt / (float)this.BookSection.Content.Length;
                }

                return percent;
            }
        }

        #endregion
    }
}
