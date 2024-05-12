﻿using EmployeeManagementSystem.Controllers;
using EmployeeManagementSystem.Dto;
using EmployeeManagementSystem.Helpers;
using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Repository;
using EmployeeManagementSystem.Services;
using LazyCache;
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
        //IAppCache
        public EmployeeServiceTest()
        {
            _crudOperation = new Mock<ICrudOperationDL>();
            _scnEncoder = new Mock<ScnEncoder>();
            _dialCodeHelper = new Mock<DialCodeHelper>();
        }
        [Fact]
        public async Task GetAll_Employee_Success()
        {
            //Arrange
            List<InsertRecordRequest> employeeRecords= new List<InsertRecordRequest> { 
            new InsertRecordRequest
            {
                 FirstName= "Ranbir",
                 LastName= "Kapoor",
                  Scn= "raha",
                  Age= 42,
                  PhoneNumber= "1332611111",
                  Role= "Marketing",
                  Salary= 2933,
                  CountryName= "Egypt"
            },
            new InsertRecordRequest
            {
                 FirstName= "Kareena",
                 LastName= "Kapoor",
                  Scn= "taimur",
                  Age= 45,
                  PhoneNumber= "1332669111",
                  Role= "PR",
                  Salary= 2943,
                  CountryName= "United States"
            }
            }; 

            _crudOperation.Setup(x => x.GetAllRecord()).ReturnsAsync(employeeRecords);
             var _service = new EmployeeService(_crudOperation.Object, _scnEncoder.Object, _dialCodeHelper.Object);
            //Act
            var result = await _service.GetAllRecord();

            //Assert
            Assert.NotNull(result);
            var apiResponse = Assert.IsType<ApiResponse>(result);
            var data = Assert.IsAssignableFrom<List<EmployeeDto>>(apiResponse.data);

            Assert.True(apiResponse.Success);
            Assert.Equal("Data Extraction successful", apiResponse.Message);
            Assert.Equal(2, data.Count);
        }

        [Fact]
        public async Task Get_EmployeeById_Success()
        {
            //Arrange
            InsertRecordRequest employeeRecord = new InsertRecordRequest {
            
                 FirstName= "Ranbir",
                 LastName= "Kapoor",
                  Scn= "raha",
                  Age= 42,
                  PhoneNumber= "1332611111",
                  Role= "Marketing",
                  Salary= 2933,
                  CountryName= "Egypt"
            
            };

            _crudOperation.Setup(x => x.GetRecordById(It.IsAny<string>())).ReturnsAsync(employeeRecord);
            var _service = new EmployeeService(_crudOperation.Object, _scnEncoder.Object, _dialCodeHelper.Object);
            //Act
            var result = await _service.GetRecordById("valid_employee_id");

            //Assert
            Assert.NotNull(result);
            var apiResponse = Assert.IsType<ApiResponse>(result);
            var data = Assert.IsAssignableFrom<List<EmployeeDto>>(apiResponse.data);

            Assert.True(apiResponse.Success);
            Assert.Equal("Data Extraction successful", apiResponse.Message);
            Assert.Single(data);
        }

        [Fact]
        public async Task Insert_Employee_Success()
        {
            //Arrange
            InsertRecordRequest employeeRecord = new InsertRecordRequest
            {

                FirstName = "Ranbir",
                LastName = "Kapoor",
                Scn = "raha",
                Age = 42,
                PhoneNumber = "1332611111",
                Role = "Marketing",
                Salary = 2933,
                CountryName = "Egypt"

            };
            _crudOperation.Setup(x => x.DoesPhoneNumberExists(It.IsAny<string>())).ReturnsAsync(false);
            _crudOperation.Setup(x => x.InsertRecord(It.IsAny<InsertRecordRequest>())).Returns(Task.CompletedTask);
            _dialCodeHelper.Setup(x => x.GetDialCodeAsync(It.IsAny<string>())).ReturnsAsync("+20");
            // _mockedLazyCache.Setup();
            var _service = new EmployeeService(_crudOperation.Object, _scnEncoder.Object, _dialCodeHelper.Object);
            //Act
            var result = await _service.InsertRecord(employeeRecord);

            //Assert
            Assert.NotNull(result);
            var apiResponse = Assert.IsType<ApiResponse>(result);
            //var data = Assert.IsAssignableFrom<List<EmployeeDto>>(apiResponse.data);

            Assert.True(apiResponse.Success);
            Assert.Equal("Onboarding successful", apiResponse.Message);
            Assert.Null(apiResponse.data);

        }

        [Fact]
        public async Task Insert_Employee_PhoneNumber_Faliure()
        {
            //Arrange
            InsertRecordRequest employeeRecord = new InsertRecordRequest
            {

                FirstName = "Ranbir",
                LastName = "Kapoor",
                Scn = "raha",
                Age = 42,
                PhoneNumber = "13326111",
                Role = "Marketing",
                Salary = 2933,
                CountryName = "Egypt"

            };
            _crudOperation.Setup(x => x.DoesPhoneNumberExists(It.IsAny<string>())).ReturnsAsync(false);
            //_crudOperation.Setup(x => x.InsertRecord(It.IsAny<InsertRecordRequest>())).Returns(Task.CompletedTask);
            //_dialCodeHelper.Setup(x => x.GetDialCodeAsync(It.IsAny<string>())).ReturnsAsync("+20");
            // _mockedLazyCache.Setup();
            var _service = new EmployeeService(_crudOperation.Object, _scnEncoder.Object, _dialCodeHelper.Object);
            //Act
            var result = await _service.InsertRecord(employeeRecord);

            //Assert
            Assert.NotNull(result);
            var apiResponse = Assert.IsType<ApiResponse>(result);
            //var data = Assert.IsAssignableFrom<List<EmployeeDto>>(apiResponse.data);

            Assert.False(apiResponse.Success);
            Assert.Equal("Exception occured:Invalid phone number. Please try again with a valid phone number.", apiResponse.Message);
            Assert.Null(apiResponse.data);
        }

        [Fact]
        public async Task Insert_Employee_PhoneNumber_AlreadyExists()
        {
            //Arrange
            InsertRecordRequest employeeRecord = new InsertRecordRequest
            {

                FirstName = "Ranbir",
                LastName = "Kapoor",
                Scn = "raha",
                Age = 42,
                PhoneNumber = "1332611111",
                Role = "Marketing",
                Salary = 2933,
                CountryName = "Egypt"

            };
            _crudOperation.Setup(x => x.DoesPhoneNumberExists(It.IsAny<string>())).ReturnsAsync(true);
            //_crudOperation.Setup(x => x.InsertRecord(It.IsAny<InsertRecordRequest>())).Returns(Task.CompletedTask);
            _dialCodeHelper.Setup(x => x.GetDialCodeAsync(It.IsAny<string>())).ReturnsAsync("+20");
            // _mockedLazyCache.Setup();
            var _service = new EmployeeService(_crudOperation.Object, _scnEncoder.Object, _dialCodeHelper.Object);
            //Act
            var result = await _service.InsertRecord(employeeRecord);

            //Assert
            Assert.NotNull(result);
            var apiResponse = Assert.IsType<ApiResponse>(result);
            //var data = Assert.IsAssignableFrom<List<EmployeeDto>>(apiResponse.data);

            Assert.False(apiResponse.Success);
            Assert.Equal("Exception occured:Pre-existing phone Number. Please try again with a valid phone number.", apiResponse.Message);
            Assert.Null(apiResponse.data);
        }
        [Fact]
        public async Task Delete_Employee_Success()
        {
            _crudOperation.Setup(x => x.DeleteRecordById(It.IsAny<string>())).ReturnsAsync(true);
            var _service = new EmployeeService(_crudOperation.Object, _scnEncoder.Object, _dialCodeHelper.Object);
            //Act
            var result = await _service.DeleteRecordById("valid_employee_id");

            //Assert
            Assert.NotNull(result);
            var apiResponse = Assert.IsType<ApiResponse>(result);
            //var data = Assert.IsAssignableFrom<List<EmployeeDto>>(apiResponse.data);

            Assert.True(apiResponse.Success);
            Assert.Equal("Employee Deleted Successfully", apiResponse.Message);
            Assert.Null(apiResponse.data);
        }

        [Fact]
        public async Task Delete_Employee_Failure()
        {
            _crudOperation.Setup(x => x.DeleteRecordById(It.IsAny<string>())).ReturnsAsync(false);
            var _service = new EmployeeService(_crudOperation.Object, _scnEncoder.Object, _dialCodeHelper.Object);
            //Act
            var result = await _service.DeleteRecordById("valid_employee_id");

            //Assert
            Assert.NotNull(result);
            var apiResponse = Assert.IsType<ApiResponse>(result);
            //var data = Assert.IsAssignableFrom<List<EmployeeDto>>(apiResponse.data);

            Assert.False(apiResponse.Success);
            Assert.Equal("Employee Deletion Failed", apiResponse.Message);
            Assert.Null(apiResponse.data);
        }
    }
}
