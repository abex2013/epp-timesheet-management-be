using Excellerent.ResourceManagement.Domain.DTOs;
using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Services;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Services
{
    public class EmployeeService : CRUD<EmployeeEntity, Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;


        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> AddNewEmployeeEntry(Employee employee)
        {
            return await _employeeRepository.AddAsync(employee);

        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetEmployeesAsync();
        }

        public async Task<Employee> GetEmployeesByEmailAsync(string email)
        {
            return await _employeeRepository.GetEmployeesByEmailAsync(email);
        }

        public async Task<bool> CheckIfEmailExists(string email)
        {
            bool returnresult = false;
            var result = await GetEmployeesByEmailAsync(email);
            if (result == null)
            {
                returnresult = true;
            }
            else
            {
                returnresult = false;
            }
            return  returnresult;
        }

        public async Task<PredicatedResponseDTO> GetAllEmployeesDashboardAsync(string searchKey, int pageIndex, int pageSize)
        {
            var predicate = PredicateBuilder.True<Employee>();

            if (searchKey == null)
                predicate = null;
            else
            {
                predicate = predicate.And(p => p.FirstName.ToLower().Contains(searchKey.ToLower()))
                                      .Or(p => p.FatherName.ToLower().Contains(searchKey.ToLower()))
                                      .Or(p => p.GrandFatherName.ToLower().Contains(searchKey.ToLower()));
            }

            var result = await _employeeRepository.GetAllEmployeesDashboardAsync(predicate, pageIndex-1, pageSize);
            
            if (result != null)
            {
                List<EmployeeViewModel> employeeList = (List<EmployeeViewModel>)result;
                int totalRowCount = await _employeeRepository.AllEmployeesDashboardCountAsync(predicate);
                return new PredicatedResponseDTO
                {
                    Data = employeeList,
                    TotalRecord = totalRowCount,//total row count
                    PageIndex = pageIndex,
                    PageSize = pageSize,  // itemPerPage,
                    TotalPage = employeeList.Count
                };
            }
            else
            {
                return new PredicatedResponseDTO
                {
                    Data = null,
                    TotalRecord = 0,//total row count
                    PageIndex = 0,
                    PageSize = 0,  // itemPerPage,
                    TotalPage = 0
                };
            }
        }


        public  Employee GetEmployeesById(Guid empId)
        {
            return  _employeeRepository.GetEmployeesById(empId);
        }

        public async Task UpdateEmployee(EmployeeEntity employeeEntity)
        {
            var data = await _employeeRepository.FindOneAsync(x => x.Guid == employeeEntity.Guid);
            var model = employeeEntity.MapToModel(data);
            await _employeeRepository.UpdateAsync(model);
        }

        public  Employee UpdateEmployee(Employee employee)

        {
           return  _employeeRepository.UpdateEmployee(employee);
            
            // _employeeRepository.UpdatePersonalInfoEmployee(employee);
            //_employeeRepository.UpdatePersonalAddressEmployee(employee);
            //_employeeRepository.UpdateOrgDetailEmployee(employee);
            //_employeeRepository.UpdateFamilyDetailEmployee(employee);
            //_employeeRepository.UpdateContactEmployee(employee);
           
        }

        public async Task<IEnumerable<EmployeeDTO>> GetSelections()
        {
            return (await _employeeRepository.GetEmployeesAsync()).Select(x => new EmployeeDTO(x));
        }

        public async Task<EmployeeDTO> GetSelection(Guid employeeGuid)
        {
            return (await _employeeRepository.GetEmployeesAsync())
                .Where(x=> x.Guid == employeeGuid)
                .Select(x => new EmployeeDTO(x))
                .FirstOrDefault();
        }
    }
}
