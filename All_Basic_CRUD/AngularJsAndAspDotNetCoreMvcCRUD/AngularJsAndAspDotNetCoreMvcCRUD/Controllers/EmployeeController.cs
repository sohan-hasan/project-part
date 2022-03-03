using AngularJsAndAspDotNetCoreMvcCRUD.DAL.IRepository;
using AngularJsAndAspDotNetCoreMvcCRUD.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AngularJsAndAspDotNetCoreMvcCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IWebHostEnvironment _iWebHostEnvironment;
        private readonly IEmployeeRepository _iEmployeeRepository;
        private readonly IDepartmentRepository _iDepartmentRepository;
        public EmployeeController(IEmployeeRepository iEmployeeRepository, IWebHostEnvironment iWebHostEnvironment, IDepartmentRepository iDepartmentRepository)
        {
            _iEmployeeRepository = iEmployeeRepository;
            _iWebHostEnvironment = iWebHostEnvironment;
            _iDepartmentRepository = iDepartmentRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            ViewBag.Departments = await _iDepartmentRepository.GetAll();
            var employees = await _iEmployeeRepository.GetAll();
            return Json(employees, new JsonSerializerOptions());
        }

        [HttpPost]
        public async Task<JsonResult> Create([FromBody]EmployeeViewModel obj)
        {
            EmployeeViewModel returnObj = new EmployeeViewModel();
            if (ModelState.IsValid)
            {
                returnObj = await _iEmployeeRepository.Create(obj);
            }
            return Json(returnObj.EmployeeId, new JsonSerializerOptions());
        }
        
        [HttpPost]
        public async Task<JsonResult> Edit([FromBody] EmployeeViewModel obj)
        {
            EmployeeViewModel returnObj = new EmployeeViewModel();
            if (ModelState.IsValid)
            {
                if (obj.EmployeeId > 0)
                {
                    returnObj = await _iEmployeeRepository.Update(obj);
                }
            }
            return Json(returnObj.EmployeeId, new JsonSerializerOptions());
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var employee = await _iEmployeeRepository.GetById(id);
            if (employee!=null)
            {
                string uploadFolder = Path.Combine(_iWebHostEnvironment.WebRootPath, "images");
                if (employee.ImageName != null)
                {
                    DeleteExistingImage(Path.Combine(uploadFolder, employee.ImageName));
                }
                await _iEmployeeRepository.Delete(id);
            }
            return Json(employee.EmployeeId, new JsonSerializerOptions());
        }
        private void DeleteExistingImage(string imagePath)
        {
            FileInfo fileObj = new FileInfo(imagePath);
            if (fileObj.Exists)
            {
                fileObj.Delete();
            }
        }

    }
}
