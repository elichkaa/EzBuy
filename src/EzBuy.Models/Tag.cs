﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzBuy.Models
{
    public class Tag : EntityName
    {
        
        public ICollection<ProductTags> Products { get; set; }
    }
}
