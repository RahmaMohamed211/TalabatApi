﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Sepecifitction
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 10;

        private int pageSize = 5; //5
        public int PageSize { get
            {
                return pageSize;
            }
            set
            {
                pageSize = value > MaxPageSize ? MaxPageSize:value;
            }
        }
        public int PageIndex { get; set; } = 1; 
        public string? Sort {  get; set; }
        public int? brandId { get; set; }

        public int? TypeId { get; set;}

        private string search {  get; set; }

        public string? Search { 
            get { return search; }
            set { search = value.ToLower(); }
        }

        

    }
}
