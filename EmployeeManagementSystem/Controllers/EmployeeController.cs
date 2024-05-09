using EmployeeManagementSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Model;
using Amazon.Runtime.Internal;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Helpers;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> OnboardEmployee(InsertRecordRequest request)
        {
            ApiResponse response = new ApiResponse(); 
            
                response = await _employeeService.InsertRecord(request);
      
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> ViewAllEmployee()
        {
            ApiResponse response = new ApiResponse();
           
                response = await _employeeService.GetAllRecord();
            
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> ViewEmployeeById([FromQuery]string ID)
        {
            ApiResponse response = new ApiResponse();
           
                response = await _employeeService.GetRecordById(ID);
           
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateSalaryById([FromQuery] string ID, [FromQuery] int salary)
        {
            ApiResponse response = new ApiResponse();


                response = await _employeeService.UpdateRecordById(ID, salary);
           
            return Ok(response);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> OffBoardEmployee([FromQuery] string ID)
        {
            ApiResponse response = new ApiResponse();
           
                response = await _employeeService.DeleteRecordById(ID);
            
            return Ok(response);
        }


    }
}
