using EmployeeManagementSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Model;
using Amazon.Runtime.Internal;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ICrudOperationDL _crudOperationDL;

        public EmployeeController(ICrudOperationDL crudOperationDL)
        {
            _crudOperationDL = crudOperationDL;
        }

        [HttpPost]
        public async Task<IActionResult> OnboardEmployee(InsertRecordRequest request)
        {
            ApiResponse response = new ApiResponse(); 
            try
            {
                response = await _crudOperationDL.InsertRecord(request);
            }
            catch(Exception e)
            {
                response.Success = false;
                response.Message= "Exception occured: "+ e.Message;
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> ViewAllEmployee()
        {
            ApiResponse response = new ApiResponse();
            try
            {
                response = await _crudOperationDL.GetAllRecord();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Exception occured: " + e.Message;
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> ViewEmployeeById([FromQuery]string ID)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                response = await _crudOperationDL.GetRecordById(ID);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Exception occured: " + e.Message;
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSalaryById([FromQuery] string ID, [FromQuery] int salary)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                response = await _crudOperationDL.UpdateRecordById(ID, salary);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Exception occured: " + e.Message;
            }
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployeeById([FromQuery] string ID)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                response = await _crudOperationDL.DeleteRecordById(ID);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Exception occured: " + e.Message;
            }
            return Ok(response);
        }


    }
}
