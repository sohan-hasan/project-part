using CoreMvcViewWithCoreApi.Helper;
using CoreMvcViewWithCoreApi.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace CoreMvcViewWithCoreApi.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeApi _api = new EmployeeApi();
        public readonly IWebHostEnvironment _iWebHostEnvironment;
        public EmployeeController(IWebHostEnvironment iWebHostEnvironment)
        {
            _iWebHostEnvironment = iWebHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res1 = await client.GetAsync("api/Department");
            if (res1.IsSuccessStatusCode)
            {
                var results = res1.Content.ReadAsStringAsync().Result;
                ViewBag.Departments = JsonConvert.DeserializeObject<List<DepartmentViewModel>>(results);

            }
            HttpResponseMessage res = await client.GetAsync("api/employee");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                employees = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(result);
            }
            return View(employees);
        }
        [HttpGet]
        public async Task<PartialViewResult> Details(int id)
        {
            ViewBag.Details = "Show";
            var employee = new EmployeeViewModel();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/employee/{id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<EmployeeViewModel>(results);
            }
            return PartialView("~/Views/Employee/_Details.cshtml", employee);
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                string uniqueImageName = "";

                if (employee.Photo != null)
                {
                    string uploadFolder = Path.Combine(_iWebHostEnvironment.WebRootPath, "images");
                    uniqueImageName = Guid.NewGuid().ToString() + "_" + employee.Photo.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueImageName);
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    employee.Photo.CopyTo(fileStream);
                    fileStream.Close();
                    employee.ImageName = uniqueImageName;
                    employee.Photo = null;
                }
                HttpClient client = _api.Initial();
                var postTask = client.PostAsJsonAsync<EmployeeViewModel>("api/employee", employee);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            return View();
        }
        [HttpGet]
        public async Task<PartialViewResult> Edit(int id)
        {
            ViewBag.Details = "Show";
            var employee = new EmployeeViewModel();
            HttpClient client = _api.Initial();
            HttpResponseMessage res2 = await client.GetAsync("api/Department");
            if (res2.IsSuccessStatusCode)
            {
                var results = res2.Content.ReadAsStringAsync().Result;
                ViewBag.Departments = JsonConvert.DeserializeObject<List<DepartmentViewModel>>(results);

            }
            HttpResponseMessage res = await client.GetAsync($"api/employee/{id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<EmployeeViewModel>(results);
            }
            return PartialView("~/Views/Employee/_Edit.cshtml", employee);
        }
        [HttpPost]
        [ActionName("Edit")]
        public IActionResult PostEdit(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                string uniqueImageName = "";

                if (employee.Photo != null)
                {
                    string uploadFolder = Path.Combine(_iWebHostEnvironment.WebRootPath, "images"); 
                    if (employee.ImageName != null)
                    {
                        DeleteExistingImage(Path.Combine(uploadFolder, employee.ImageName));
                    }
                    uniqueImageName = Guid.NewGuid().ToString() + "_" + employee.Photo.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueImageName);
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    employee.Photo.CopyTo(fileStream);
                    fileStream.Close();
                    employee.ImageName = uniqueImageName;
                    employee.Photo = null;
                }
                HttpClient client = _api.Initial();
                var postTask = client.PutAsJsonAsync<EmployeeViewModel>($"api/employee/{employee.EmployeeId}", employee);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = new EmployeeViewModel();
            HttpClient client = _api.Initial();
            HttpResponseMessage res1 = await client.GetAsync($"api/employee/{id}");
            if (res1.IsSuccessStatusCode)
            {
                var results = res1.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<EmployeeViewModel>(results);
            }
            if (employee.ImageName != null)
            {
                string uploadFolder = Path.Combine(_iWebHostEnvironment.WebRootPath, "images");
                DeleteExistingImage(Path.Combine(uploadFolder, employee.ImageName));
            }
            HttpResponseMessage res = await client.DeleteAsync($"api/employee/{id}");
            return RedirectToAction("Index");
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
