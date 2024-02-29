using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.core.Sepecifitction
{
    public class EmployeeSpecification :BaseSpecification<Employee>
    {
        public EmployeeSpecification()
        {
            includes.Add(E => E.Department);              
        }

        public EmployeeSpecification(int id) :base(E=>E.Id ==id)
        {
            includes.Add(E => E.Department);
          }
    }
}
