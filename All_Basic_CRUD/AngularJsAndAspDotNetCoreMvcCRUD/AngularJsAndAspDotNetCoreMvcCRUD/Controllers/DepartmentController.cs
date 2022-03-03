using AngularJsAndAspDotNetCoreMvcCRUD.DAL.IRepository;
using AngularJsAndAspDotNetCoreMvcCRUD.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AngularJsAndAspDotNetCoreMvcCRUD.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _iDepartmentRepository;
        public DepartmentController(IDepartmentRepository iDepartmentRepository)
        {
            _iDepartmentRepository = iDepartmentRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var departments = await _iDepartmentRepository.GetAll();
            return Json(departments, new JsonSerializerOptions());

        }

        [HttpPost]
        public async Task<JsonResult> Create([FromBody] DepartmentViewModel obj)
        {
            DepartmentViewModel department = new DepartmentViewModel();
            if (ModelState.IsValid)
            {
                department = await _iDepartmentRepository.Create(obj);
            }
            return Json(department.DepartmentId, new JsonSerializerOptions());
        }
        [HttpPost]
        public async Task<JsonResult> Edit([FromBody] DepartmentViewModel obj)
        {
            DepartmentViewModel department = new DepartmentViewModel();
            if (ModelState.IsValid)
            {
                if (obj.DepartmentId>0)
                {
                    department =  await _iDepartmentRepository.Update(obj);

                }
            }
            return Json(department.DepartmentId, new JsonSerializerOptions());
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            DepartmentViewModel department = await _iDepartmentRepository.GetById(id);
            if (department!=null)
            {
               await _iDepartmentRepository.Delete(id);
            }
            return Json(department.DepartmentId, new JsonSerializerOptions());
        }
    }
}
