using CoreApiWithCoreMVCCView.DAL.IRepository;
using CoreApiWithCoreMVCCView.ViewModels;
using Microsoft.AspNetCore.Hosting;
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
    public class EmployeeController : ControllerBase
    {
        private readonly IWebHostEnvironment _iWebHostEnvironment;
        private readonly IEmployeeRepository _iEmployeeRepository;
        public EmployeeController(IEmployeeRepository iEmployeeRepository, IWebHostEnvironment iWebHostEnvironment)
        {
            _iEmployeeRepository = iEmployeeRepository;
            _iWebHostEnvironment = iWebHostEnvironment;
        }
        [HttpGet]
        public async Task<IEnumerable<EmployeeViewModel>> GetEmployees()
        {
            return await _iEmployeeRepository.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployee(int id)
        {
            return await _iEmployeeRepository.GetById(id);
        }
        [HttpPost]
        public async Task<ActionResult<EmployeeViewModel>> PostEmployee([FromBody] EmployeeViewModel employee)
        {
            var newEmployee= await _iEmployeeRepository.Create(employee);
            return CreatedAtAction(nameof(GetEmployees), new { id = newEmployee.EmployeeId }, newEmployee);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutEmployee(int id, [FromBody] EmployeeViewModel employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }
            await _iEmployeeRepository.Update(employee);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var employeeToDelete = await _iEmployeeRepository.GetById(id);
            if (employeeToDelete == null)
            {
                return NotFound();
            }
            await _iEmployeeRepository.Delete(employeeToDelete.EmployeeId);
            return NoContent();
        }
    }
}
