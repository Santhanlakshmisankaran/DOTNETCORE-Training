using System;
using System.Collections.Generic;

namespace trainingmiddleware.Model
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? EmployeeDoj { get; set; }
        public string EmployeeEmail { get; set; }
    }
}
