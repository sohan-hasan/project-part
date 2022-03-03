using CoreMvcViewWithCoreApi.Helper;
using CoreMvcViewWithCoreApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CoreMvcViewWithCoreApi.Controllers
{
    public class DepartmentController : Controller
    {
        EmployeeApi _api = new EmployeeApi();

        public async Task<IActionResult> Index()
        {
            List<DepartmentViewModel> department = new List<DepartmentViewModel>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Department");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                department = JsonConvert.DeserializeObject<List<DepartmentViewModel>>(result);
            }
            return View(department);
        }
        public async Task<PartialViewResult> Details(int id)
        {
            var department = new DepartmentViewModel();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/department/{id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                department = JsonConvert.DeserializeObject<DepartmentViewModel>(results);
            }

            return PartialView("~/Views/Department/_Details.cshtml", department);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentViewModel department)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _api.Initial();
                var postTask = client.PostAsJsonAsync<DepartmentViewModel>("api/department", department);
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
            var department = new DepartmentViewModel();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/department/{id}");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<PartialViewResult> Edit(int id)
        {
            var department = new DepartmentViewModel();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/department/{id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                department = JsonConvert.DeserializeObject<DepartmentViewModel>(results);
            }
            return PartialView("~/Views/Department/_Edit.cshtml", department);
        }
        [HttpPost]
        [ActionName("Edit")]
        public IActionResult PostEdit(DepartmentViewModel department)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _api.Initial();
                var postTask = client.PutAsJsonAsync<DepartmentViewModel>($"api/department/{department.DepartmentId}", department);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            return View();
        }
    }
}
