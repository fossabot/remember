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
    public partial class Learner_VideoInfo : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// ������IP
        /// </summary>
        [StringLength(30)]
        public string LastAccessIp { get; set; }

        /// <summary>
        /// ��󲥷�ʱ��
        /// eg: ��������� 2019-12-12 22:21 ʱ�����˴���Ƶ
        /// </summary>
        public DateTime? LastPlayTime { get; set; }

        /// <summary>
        /// ��ѧϰ���ڴ˿μ�-ѧϰ����
        /// <para>ѧϰ���ȣ���Ƶ��������λ�ã���ǰ���������Ƶ����ѧϰ������ȻΪ����״̬�����䣬�����²���λ����ͬ</para>
        /// ����
        /// ������Ƶ����λ��
        /// </summary>
        public long? ProgressAt { get; set; }

        /// <summary>
        /// ��ѧϰ���ڴ���Ƶ�μ�-���²���λ��
        /// ����
        /// </summary>
        public long? LastPlayAt { get; set; }

        #region Relationships

        /// <summary>
        /// ѧϰ��
        /// </summary>
        [ForeignKey("Learner")]
        public int? LearnerId { get; set; }
        [ForeignKey("LearnerId")]
        public virtual UserInfo Learner { get; set; }

        /// <summary>
        /// ��Ƶ�μ�
        /// </summary>
        [ForeignKey("VideoInfo")]
        public int? VideoInfoId { get; set; } 
        [ForeignKey("VideoInfoId")]
        public virtual VideoInfo VideoInfo { get; set; }

        #endregion

        #region Helpers

        /// <summary>
        /// ���Ȱٷֱ�
        /// Ϊ1�������
        /// </summary>
        public float Percent
        {
            get
            {
                float percent = 0;
                if (this.VideoInfo != null && this.VideoInfo.Duration != 0)
                {
                    percent = (float)ProgressAt / (float)this.VideoInfo.Duration;
                }

                return percent;
            }
        }

        #endregion
    }
}
