﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Entities
{
    public class Employee :BaseEntity
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }

        public int Age { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; } //navigation prop

    }
}
