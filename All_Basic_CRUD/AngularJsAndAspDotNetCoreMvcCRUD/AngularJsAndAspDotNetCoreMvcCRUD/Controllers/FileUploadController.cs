using AngularJsAndAspDotNetCoreMvcCRUD.DAL.IRepository;
using AngularJsAndAspDotNetCoreMvcCRUD.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AngularJsAndAspDotNetCoreMvcCRUD.Controllers
{
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IEmployeeRepository _iEmployeeRepository;
        private readonly IWebHostEnvironment _iWebHostEnvironment;
        public FileUploadController(IEmployeeRepository iEmployeeRepository, IWebHostEnvironment iWebHostEnvironment)
        {
            _iWebHostEnvironment = iWebHostEnvironment;
            _iEmployeeRepository = iEmployeeRepository;
        }
        [Route("api/FileUpload/EmployeeImageUpload")]
        [HttpPost]
        public async Task<IActionResult> EmployeeImageUpload(int id)
        {
            if (id>0)
            {
                IFormFile photo = null;
                string uniqueImageName = null;
                var files = HttpContext.Request.Form.Files;
                foreach (IFormFile Photo in HttpContext.Request.Form.Files)
                {
                    photo = Photo;
                }
                EmployeeViewModel obj = await _iEmployeeRepository.GetById(id);
                if (photo != null)
                {
                    string uploadFolder = Path.Combine(_iWebHostEnvironment.WebRootPath, "images");
                    if (obj.ImageName != null)
                    {
                        DeleteExistingImage(Path.Combine(uploadFolder, obj.ImageName));
                    }
                    uniqueImageName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueImageName);
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    photo.CopyTo(fileStream);
                    fileStream.Close();
                    obj.ImageName = uniqueImageName;
                    await _iEmployeeRepository.Update(obj);
                }
                return Ok();
            }
            return NotFound();
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
