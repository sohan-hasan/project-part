using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularJsAndAspDotNetCoreMvcCRUD.ViewModels
{
    public class EmployeeViewModel
    {
        [Key]
        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Employee name is required")]
        [StringLength(50, ErrorMessage = "Name can not be exceed 50 characters")]
        public string Name { get; set; }
        [Display(Name = "Department Id")]
        [Required(ErrorMessage = "Department Id is required")]
        public int DepartmentId { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        [StringLength(50, ErrorMessage = "Email Address can not be exceed 50 characters")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Display(Name = "Date Of Birth"), DataType(DataType.Date), Required(ErrorMessage = "Date Of Birth is required"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Age")]
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }
        [Display(Name = "Employee Image")]
        public string ImageName { get; set; }
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
    }
    public class DepartmentViewModel
    {
        [Key]
        [Display(Name = "Department Id")]
        public int DepartmentId { get; set;}

        [Required(ErrorMessage = "Department Name is required")]
        [StringLength(50, ErrorMessage = "Department Name can not be exceed 50 characters")]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

    }
}
