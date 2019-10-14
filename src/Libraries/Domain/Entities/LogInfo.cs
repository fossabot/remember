namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ʵ���ࣺ��־��Ϣ
    /// </summary>
    public partial class LogInfo : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// �����ߵ��û�ID
        /// ���δ��¼����Ϊ 0
        /// </summary>
        public int? AccessUserId { get; set; }

        /// <summary>
        /// �����ߵ�IP
        /// </summary>
        [StringLength(30)]
        public string AccessIp { get; set; }

        /// <summary>
        /// ���������ڳ���
        /// </summary>
        [StringLength(500)]
        [Column(TypeName = "text")]
        public string AccessCity { get; set; }

        /// <summary>
        /// ���� UserAgent json�ַ���
        /// {
        ///      ua: "",
        ///      browser: {
        ///          name: "",
        ///          version: ""
        ///      },
        ///      engine: {
        ///          name: "",
        ///          version: ""
        ///      },
        ///      os: {
        ///          name: "",
        ///          version: ""
        ///      },
        ///      device: {
        ///          model: "",
        ///          type: "",
        ///          vendor: ""
        ///      },
        ///      cpu: {
        ///          architecture: ""
        ///      }
        //} }
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(500)]
        public string UserAgent { get; set; }

        [StringLength(30)]
        public string Browser { get; set; }

        [StringLength(30)]
        public string BrowserEngine { get; set; }

        [StringLength(30)]
        public string OS { get; set; }

        [StringLength(30)]
        public string Device { get; set; }

        [StringLength(30)]
        public string Cpu { get; set; }

        /// <summary>
        /// ����ʱ�䣺������ҳ���������ʱ��
        /// </summary>
        public DateTime? AccessTime { get; set; }

        /// <summary>
        /// ������ҳʱ��
        /// </summary>
        public DateTime? JumpTime { get; set; }

        /// <summary>
        /// ��ҳ��ĳ���ʱ�� = JumpTime - AccessTime
        /// ������
        /// </summary>
        public long? Duration { get; set; }

        /// <summary>
        /// ���ʵ�ַ
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(1000)]
        public string AccessUrl { get; set; }

        /// <summary>
        /// ��ԴURL
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(1000)]
        public string RefererUrl { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
