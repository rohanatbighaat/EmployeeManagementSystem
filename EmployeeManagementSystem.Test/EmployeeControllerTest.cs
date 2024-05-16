using EmployeeManagementSystem.Controllers;
using EmployeeManagementSystem.Dto;
using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeManagementSystem.Test
{
    public class EmployeeControllerTest
    {
        private readonly Mock<IEmployeeService> _employeeService;

        public EmployeeControllerTest()
        {
            _employeeService= new Mock<IEmployeeService>();
        }
        [Fact]
        public async Task GetAll_Employee_Success()
        {
            //Arrange
            var _apiResponse = new ApiResponse()
            {
                Success = true,
                Message = "Data Extraction successful"
            };
            _employeeService.Setup(x => x.GetAllRecord()).ReturnsAsync(_apiResponse);
            var _controller = new EmployeeController(_employeeService.Object);
            //Act
            var result = await _controller.ViewAllEmployee();

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsAssignableFrom<ApiResponse>(objectResult.Value);

            Assert.True(apiResponse.Success);
            Assert.Equal("Data Extraction successful", apiResponse.Message);
        }
        
        [Fact]
        public async Task GetById_Employee_Success()
        {
            //Arrange
            var _apiResponse = new ApiResponse()
            {
                Success = true,
                Message = "Data Extraction successful"
            };
            _employeeService.Setup(x => x.GetRecordById(It.IsAny<string>())).ReturnsAsync(_apiResponse);
            var _controller = new EmployeeController(_employeeService.Object);

            //Act
            var result = await _controller.ViewEmployeeById("valid_employee_id");

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsAssignableFrom<ApiResponse>(objectResult.Value);

            Assert.True(apiResponse.Success);
            Assert.Equal("Data Extraction successful", apiResponse.Message);
        }
        
        [Fact]
        public async Task Add_Employee_Success()
        {
            //Arrange
            var _apiResponse = new ApiResponse()
            {
                Success = true,
                Message = "Onboarding successful"
            };

            _employeeService.Setup(x => x.InsertRecord(It.IsAny<InsertRecordRequest>())).ReturnsAsync(_apiResponse);
            var _controller = new EmployeeController(_employeeService.Object);


            // Act
            var result = await _controller.OnboardEmployee(new InsertRecordRequest());
            // Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsAssignableFrom<ApiResponse>(objectResult.Value);

            Assert.True(apiResponse.Success);
            Assert.Equal("Onboarding successful", apiResponse.Message);
            
        }
        [Fact]
        public async Task Delete_Employee_Success()
        {
            //Arrange
            var _apiResponse = new ApiResponse()
            {
                Success = true,
                Message = "Employee Deleted Successfully"
            };

            _employeeService.Setup(x => x.DeleteRecordById(It.IsAny<string>())).ReturnsAsync(_apiResponse);
            var _controller = new EmployeeController(_employeeService.Object);


            // Act
            var result = await _controller.OffBoardEmployee("valid_employee_id");
            // Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsAssignableFrom<ApiResponse>(objectResult.Value);

            Assert.True(apiResponse.Success);
            Assert.Equal("Employee Deleted Successfully", apiResponse.Message);
        } 

    }
}