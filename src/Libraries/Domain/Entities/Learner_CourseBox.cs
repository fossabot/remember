namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ʵ���ࣺѧϰ��-�γ�
    /// </summary>
    public partial class Learner_CourseBox : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// ����ѧϰ��ʱ��
        /// </summary>
        public DateTime? JoinTime { get; set; }

        /// <summary>
        /// ��ѧϰ���ڴ˿γ���ѧϰʱ��: ����ʱ��
        /// ��
        /// <para>ע�⣺��һ����ѧϰ���ڴ˿γ��ϵ����пμ��Ľ���ʱ�䣬��Ϊ�γ̴��ڷ�������������ʱ��ҲҪ��������</para>
        /// </summary>
        public long? SpendTime { get; set; }

        #region Relationships

        /// <summary>
        /// ���²�����Ƶ
        /// </summary>
        [ForeignKey("LastPlayVideoInfo")]
        public int? LastPlayVideoInfoId { get; set; }
        [ForeignKey("LastPlayVideoInfoId")]
        public virtual VideoInfo LastPlayVideoInfo { get; set; }

        /// <summary>
        /// ѧϰ��
        /// </summary>
        [ForeignKey("Learner")]
        public int? LearnerId { get; set; }
        [ForeignKey("Learner")]
        public virtual UserInfo Learner { get; set; }

        /// <summary>
        /// �γ̺�
        /// </summary>
        [ForeignKey("CourseBox")]
        public int? CourseBoxId { get; set; }
        [ForeignKey("CourseBoxId")]
        public virtual CourseBox CourseBox { get; set; }

        #endregion
    }
}
