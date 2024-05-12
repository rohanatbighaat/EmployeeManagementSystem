using EmployeeManagementSystem.Controllers;
using EmployeeManagementSystem.Helpers;
using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Repository;
using EmployeeManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Test
{
    public class EmployeeServiceTest
    {
        private readonly Mock<ICrudOperationDL> _crudOperation;
        private readonly Mock<ScnEncoder> _scnEncoder;
        private readonly Mock<DialCodeHelper> _dialCodeHelper;
        public EmployeeServiceTest()
        {
            _crudOperation = new Mock<ICrudOperationDL>();
            _scnEncoder = new Mock<ScnEncoder>();
            _dialCodeHelper = new Mock<DialCodeHelper>();

        }
        /*
        public async Task GetAll_Employee_Success()
        {
            //Arrange
            _crudOperation.Setup(x => x.GetAllRecord()).ReturnsAsync(new List<InsertRecordRequest>());
            var _controller = new EmployeeService(_crudOperation.Object, _scnEncoder.Object, _dialCodeHelper.Object);
            //Act
            var result = await _controller.ViewAllEmployee();

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsAssignableFrom<ApiResponse>(objectResult.Value);

            Assert.True(apiResponse.Success);
            Assert.Equal("Data Extraction successful", apiResponse.Message);
        }
        */
    }
}
