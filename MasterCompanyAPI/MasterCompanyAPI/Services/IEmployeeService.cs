using MasterCompanyAPI.Models;

namespace MasterCompanyAPI.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<IEnumerable<Employee>> GetEmployeesBySalaryRange(decimal minSalary, decimal maxSalary);
        Task<IEnumerable<Employee>> GetNonDuplicateEmployees();
        Task<IEnumerable<Employee>> GetSalaryAdjustments();
        Task<(decimal malePercentage, decimal femalePercentage)> GetGenderPercentages();
        Task AddEmployee(Employee employee);
        Task RemoveEmployee(string document);
        Task<Employee> GetEmployeeByDocument(string document);
    }
}
