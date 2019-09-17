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
        public long CreateTime { get; set; }

        /// <summary>
        /// 最近更新时间
        /// </summary>
        public long LastUpdateTime { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 有效日期 - 开始时间
        /// </summary>
        public long StartTime { get; set; }

        /// <summary>
        /// 有效日期 - 结束时间
        /// </summary>
        public long EndTime { get; set; }

        /// <summary>
        /// 有效学习天数
        /// </summary>
        public int LearnDay { get; set; }

        /// <summary>
        /// 最新访问的课件
        /// </summary>
        public CourseInfoViewModel LastAccessCourseInfo { get; set; }

        /// <summary>
        /// 加入学习的时间
        /// js时间戳
        /// </summary>
        public long JoinTime { get; set; }

        /// <summary>
        /// 此学习者在此课程总学习时间: 花费时间
        /// 毫秒
        /// <para>注意：不一定是学习者在此课程上的所有课件的进度时间，因为课程存在反复看，反复看时间也要计算在内</para>
        /// </summary>
        public long SpendTime { get; set; }

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
        }
    }
}