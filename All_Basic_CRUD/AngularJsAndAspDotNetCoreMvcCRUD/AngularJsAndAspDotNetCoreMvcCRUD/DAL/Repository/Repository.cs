using AngularJsAndAspDotNetCoreMvcCRUD.DAL.IRepository;
using AngularJsAndAspDotNetCoreMvcCRUD.Models;
using AngularJsAndAspDotNetCoreMvcCRUD.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularJsAndAspDotNetCoreMvcCRUD.DAL.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;
        public EmployeeRepository(EmployeeContext contex)
        {
            _context = contex;
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetAll()
        {
            IEnumerable<EmployeeViewModel> listOfEmployee = await _context.Employees.Select(e => new EmployeeViewModel
            {
                EmployeeId = e.EmployeeId,
                Name = e.Name,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.Department.DepartmentName,
                Email = e.Email,
                DateOfBirth = e.DateOfBirth,
                Age = e.Age,
                ImageName = e.ImageName
            }).ToListAsync();
            return listOfEmployee;
        }

        public async Task<EmployeeViewModel> GetById(int id)
        {
            Employee e = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (e != null)
            {
                EmployeeViewModel employee = new EmployeeViewModel
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    DepartmentId = e.DepartmentId,
                    Email = e.Email,
                    DateOfBirth = e.DateOfBirth,
                    Age = e.Age,
                    ImageName = e.ImageName
                };
                return employee;
            }
            return null;
        }

        public async Task<EmployeeViewModel> Create(EmployeeViewModel e)
        {
            EmployeeViewModel returnObj = new EmployeeViewModel();
            if (e != null)
            {
                Employee obj = new Employee()
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    DepartmentId = e.DepartmentId,
                    Email = e.Email,
                    DateOfBirth = e.DateOfBirth,
                    Age = e.Age,
                    ImageName = e.ImageName
                };
                await _context.Employees.AddAsync(obj);
                await Save();
                returnObj = await GetById(obj.EmployeeId);
            }
            return returnObj;
        }
        public async Task<EmployeeViewModel> Update(EmployeeViewModel e)
        {
            var result = await _context.Employees.FirstOrDefaultAsync(h => h.EmployeeId == e.EmployeeId);
            EmployeeViewModel returnObj = new EmployeeViewModel();
            if (result != null)
            {
                result.EmployeeId = e.EmployeeId;
                result.Name = e.Name;
                result.DepartmentId = e.DepartmentId;
                result.Email = e.Email; ;
                result.DateOfBirth = e.DateOfBirth;
                result.Age = e.Age;
                result.ImageName = e.ImageName;
            }
            await Save();
            returnObj = await GetById(result.EmployeeId);
            return returnObj;
        }
        public async Task Delete(int id)
        {
            var result = await _context.Employees.FirstOrDefaultAsync(p => p.EmployeeId == id);
            if (result != null)
            {
                _context.Employees.Remove(result);
                await Save();
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeContext _context;
        public DepartmentRepository(EmployeeContext contex)
        {
            _context = contex;
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetAll()
        {
            IEnumerable<DepartmentViewModel> listOfDepartment = await _context.Departments.Select(e => new DepartmentViewModel
            {
                DepartmentId = e.DepartmentId,
                DepartmentName = e.DepartmentName,
            }).ToListAsync();
            return listOfDepartment;
        }

        public async Task<DepartmentViewModel> GetById(int id)
        {
            Department e = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(e => e.DepartmentId == id);
            if (e != null)
            {
                DepartmentViewModel department = new DepartmentViewModel
                {
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.DepartmentName,
                };
                return department;
            }
            return null;
        }

        public async Task<DepartmentViewModel> Create(DepartmentViewModel e)
        {
            DepartmentViewModel returnObj = new DepartmentViewModel();
            if (e != null)
            {
                Department obj = new Department()
                {
                    DepartmentId = e.DepartmentId,
                    DepartmentName=e.DepartmentName,
                };
                await _context.Departments.AddAsync(obj);
                await Save();
                returnObj = await GetById(obj.DepartmentId);
            }
            return returnObj;
        }
        public async Task<DepartmentViewModel> Update(DepartmentViewModel e)
        {
            var result = await _context.Departments.FirstOrDefaultAsync(h => h.DepartmentId == e.DepartmentId);
            DepartmentViewModel returnObj = new DepartmentViewModel();
            if (result != null)
            {
                result.DepartmentId = e.DepartmentId;
                result.DepartmentName = e.DepartmentName; ;
            }
            await Save();
            returnObj = await GetById(result.DepartmentId);
            return returnObj;
        }
        public async Task Delete(int id)
        {
            var result = await _context.Departments.FirstOrDefaultAsync(p => p.DepartmentId == id);
            if (result != null)
            {
                _context.Departments.Remove(result);
                await Save();
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
