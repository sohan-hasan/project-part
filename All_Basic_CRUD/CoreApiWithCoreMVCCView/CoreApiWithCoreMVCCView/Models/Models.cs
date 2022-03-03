using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWithCoreMVCCView.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required, MaxLength(50)]
        public string Email { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int Age { get; set; }
        public string ImageName { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
    }
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required, MaxLength(50)]
        public string DepartmentName { get; set; }
        public virtual ICollection<Department> Departments { get; set; }

    }
}
