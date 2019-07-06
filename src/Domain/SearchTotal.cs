﻿using Domain.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class SearchTotal : BaseEntity<SearchTotal>
    {
        [Key]
        public new Guid ID { get; set; }

        [StringLength(50)]
        public string KeyWord { get; set; }

        public int SearchCount { get; set; }
    }

}