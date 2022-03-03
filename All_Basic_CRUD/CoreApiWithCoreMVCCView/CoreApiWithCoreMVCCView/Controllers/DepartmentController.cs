using CoreApiWithCoreMVCCView.DAL.IRepository;
using CoreApiWithCoreMVCCView.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWithCoreMVCCView.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _iDepartmentRepository;
        public DepartmentController(IDepartmentRepository iDepartmentRepository)
        {
            _iDepartmentRepository = iDepartmentRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<DepartmentViewModel>> GetDepartments()
        {
            return await _iDepartmentRepository.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentViewModel>> GetDepartment(int id)
        {
            return await _iDepartmentRepository.GetById(id);
        }
        [HttpPost]
        public async Task<ActionResult<DepartmentViewModel>> PostDepartment([FromBody] DepartmentViewModel department)
        {
            var newDepartment = await _iDepartmentRepository.Create(department);
            return CreatedAtAction(nameof(GetDepartments), new { id = newDepartment.DepartmentId }, newDepartment);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutDepartment(int id, [FromBody] DepartmentViewModel department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }
            await _iDepartmentRepository.Update(department);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var departmentToDelete = await _iDepartmentRepository.GetById(id);
            if (departmentToDelete == null)
            {
                return NotFound();
            }
            await _iDepartmentRepository.Delete(departmentToDelete.DepartmentId);
            return NoContent();
        }
    }
}
