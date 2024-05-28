using AutoMapper;
using EmployeeManagementSystem.Dto;
using EmployeeManagementSystem.Exceptions;
using EmployeeManagementSystem.Helpers;
using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ICrudOperationDL _crudOperationDL;

        private readonly ScnEncoder _scnEncoder;

        private readonly DialCodeHelper _dialCodeHelper;

        private readonly IMapper _mapper;
        public EmployeeService(ICrudOperationDL crudOperationDL, ScnEncoder scnEncoder, DialCodeHelper dialCodeHelper, IMapper mapper)
        {
            _crudOperationDL = crudOperationDL;
            _scnEncoder = scnEncoder;
            _dialCodeHelper = dialCodeHelper;
            _mapper= mapper;
        }

        /**
         * Method that deletes the employee record by id [Authenticated]
         **/
        public async Task<ApiResponse> DeleteRecordById(string ID)
        {
            ApiResponse response = new ApiResponse();
            response.Success = true;
            response.Message = "Employee Deleted Successfully";
            try
            {
                bool Result= await _crudOperationDL.DeleteRecordById(ID);
                if (!Result)
                {
                    response.Success=false;
                    response.Message = "Employee Deletion Failed";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Exception occured: "+ ex.Message;
            }
            return response;

            }

        /**
         * Method to retrieve all the employee records
         **/
        public async Task<ApiResponse> GetAllRecord()
        {
            ApiResponse response = new ApiResponse();
            response.Success = true;
            response.Message = "Data Extraction successful";
            try
            { 
                response.data = new List<EmployeeDto>();
                List<InsertRecordRequest> recordList = await _crudOperationDL.GetAllRecord();
                response.data= _mapper.Map<List<EmployeeDto>>(recordList);

                if (response.data.Count == 0)
                {
                    response.Message = "No data to display";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Exception occured:" + ex.Message;
            }
            return response;
          }

        /**
         *  Method to retrieve a particular employee record by id
         **/
        public async Task<ApiResponse> GetRecordById(string ID)
        {
            ApiResponse response = new ApiResponse();
            response.Success = true;
            response.Message = "Data Extraction successful";
            try
            {
                InsertRecordRequest record = await _crudOperationDL.GetRecordById(ID);   
                response.data = _mapper.Map<List<EmployeeDto>>(record);
                if (response.data.Count == 0)
                {
                    response.Message = "No data to display";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Exception occured:" + ex.Message;
            }
            return response;
        }

        /**
         *  Method to insert an employee record [Authenticated]
         **/
        public async Task<ApiResponse> InsertRecord(InsertRecordRequest request)
        {
            ApiResponse response = new ApiResponse();
            response.Success = true;
            response.Message = "Onboarding successful";
            try
            {
                request.CreatedAt = DateTime.Now.ToString();
                request.UpdatedAt = string.Empty;
                if (!Regex.IsMatch(request.PhoneNumber, @"^\d{10}$"))
                {
                    throw new InvalidPhoneNumberException("Invalid phone number. Please try again with a valid phone number.");
                }
                // here call the api: https://country-code-au6g.vercel.app/Country.json and store the data in the cache, use the data to map the country code to the dial code and append it accordingly

                string dialCode = await _dialCodeHelper.GetDialCodeAsync(request.CountryName);

                request.PhoneNumber = dialCode +" "+ request.PhoneNumber;
                bool isPhoneNumberPreviouslyExists = await _crudOperationDL.DoesPhoneNumberExists(request.PhoneNumber);
                if(isPhoneNumberPreviouslyExists)
                {
                    throw new InvalidPhoneNumberException("Pre-existing phone Number. Please try again with a valid phone number.");
                }
                request.Scn = _scnEncoder.EncodeScn(request.Scn);
                await _crudOperationDL.InsertRecord(request);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Exception occured:" + ex.Message;
            }
            return response;
        }

        /**
         *  Method the salary of an employee record by id [Authenticated]
         **/
        public async Task<ApiResponse> UpdateRecordById(string ID, int salary)
        {
            ApiResponse response = new ApiResponse();
            response.Success = true;
            response.Message = "Salary updated successfully";

            try
            {

                bool Result = await _crudOperationDL.UpdateRecordById(ID, salary);
                if (!Result)
                {
                    response.Message = "Salary Updation failure";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Exception occured: " + ex.Message;
            }
            return response;
        }
    }
}
