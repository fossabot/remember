﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.CourseBoxVM
{
    public class CourseBoxViewModel
    {
        /// <summary>
        /// 课程ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 课程名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public UserInfoVM.UserInfoViewModel Creator { get; set; }

        /// <summary>
        /// 封面图
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 最近更新时间
        /// </summary>
        public string LastUpdateTime { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 有效日期 - 开始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 有效日期 - 结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 有效学习天数
        /// </summary>
        public int LearnDay { get; set; }

        /// <summary>
        /// 课件数
        /// </summary>
        public int Count
        {
            get
            {
                return this.Pages.Count;
            }
        }

        /// <summary>
        /// 课件列表
        /// </summary>
        public IList<CourseInfoViewModel> Pages { get; set; }

        /// <summary>
        /// 课件
        /// </summary>
        public sealed class CourseInfoViewModel
        {
            /// <summary>
            /// 课件ID
            /// </summary>
            public int ID { get; set; }

            /// <summary>
            /// 此课件在课程内的序号
            /// </summary>
            public int Page { get; set; }

            /// <summary>
            /// 课件标题
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 持续时间
            /// 视频：此课件总需播放时间
            /// </summary>
            public long Duration { get; set; }
        }
    }
}