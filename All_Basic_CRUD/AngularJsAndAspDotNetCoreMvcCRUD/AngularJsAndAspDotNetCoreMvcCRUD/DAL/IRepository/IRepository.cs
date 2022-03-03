using AngularJsAndAspDotNetCoreMvcCRUD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularJsAndAspDotNetCoreMvcCRUD.DAL.IRepository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeViewModel>> GetAll();
        Task<EmployeeViewModel> GetById(int id);
        Task<EmployeeViewModel> Create(EmployeeViewModel e);
        Task<EmployeeViewModel> Update(EmployeeViewModel e);
        Task Delete(int id);
        Task Save();
    }
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DepartmentViewModel>> GetAll();
        Task<DepartmentViewModel> GetById(int id);
        Task<DepartmentViewModel> Create(DepartmentViewModel e);
        Task<DepartmentViewModel> Update(DepartmentViewModel e);
        Task Delete(int id);
        Task Save();
    }
}
