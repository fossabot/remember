﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.BookInfoVM
{
    public class CommentInputModel
    {
        public int CourseBoxId { get; set; }

        public string Content { get; set; }
    }
}