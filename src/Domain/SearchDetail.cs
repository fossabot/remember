﻿using Domain.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class SearchDetail : BaseEntity<SearchDetail>
    {
        [StringLength(50)]
        public string KeyWord { get; set; }

        public DateTime? SearchTime { get; set; }
    }

}