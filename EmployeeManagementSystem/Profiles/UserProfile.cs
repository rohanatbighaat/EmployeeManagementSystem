using AutoMapper;
using EmployeeManagementSystem.Dto;
using EmployeeManagementSystem.Model;

namespace EmployeeManagementSystem.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile() {
            CreateMap<InsertRecordRequest, EmployeeDto>();
        }
    }
}
