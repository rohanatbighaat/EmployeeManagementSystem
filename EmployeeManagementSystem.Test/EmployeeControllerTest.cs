using EmployeeManagementSystem.Controllers;
using EmployeeManagementSystem.Dto;
using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeManagementSystem.Test
{
    public class EmployeeControllerTest// : IClassFixture<EmployeeController>
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
        /*
        [Fact]
        public async Task Add_Employee_PhoneNumber_Faliure()
        {
            InsertRecordRequest employee = new InsertRecordRequest()
            {
                FirstName = "Katrina",
                LastName = "Kaif",
                Scn = "djdpd",
                Age = 40,
                PhoneNumber = "12623111",
                Role = "Product Manager",
                Salary = 2133,
                CountryName = "Egypt"
            };

            // Act
            var response = await _controller.OnboardEmployee(employee) as ApiResponse;
            // Assert
            Assert.False(response.Success); // Check if the operation was successful
            Assert.Equal("Exception occured:Invalid phone number. Please try again with a valid phone number.", response.Message);
            Assert.Null(response.data);
        }

        [Fact]
        public async Task Add_Employee_PhoneNumber_AlreadyExists()
        {
            InsertRecordRequest employee = new InsertRecordRequest()
            {
                FirstName = "Katrina",
                LastName = "Kaif",
                Scn = "djdpd",
                Age = 40,
                PhoneNumber = "9380601863",
                Role = "Product Manager",
                Salary = 2133,
                CountryName = "Egypt"
            };

            // Act
            var response = await _controller.OnboardEmployee(employee) as ApiResponse;
            // Assert
            Assert.False(response.Success); // Check if the operation was successful
            Assert.Equal("Pre-existing phone Number. Please try again with a valid phone number.", response.Message);
            Assert.Null(response.data);
        }
        */
        [Fact]
        public async Task Delete_Employee_Success()
        {
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