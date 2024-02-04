using MasterCompanyAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MasterCompanyAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly string filePath = "Data/employeesdb.txt";

        private string GetFilePath()
        {
            var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(rootPath!, filePath);
        }

        public async Task AddEmployee(Employee employee)
        {
            var allEmployees = (await GetAllEmployees()).ToList();
            allEmployees.Add(employee);
            await SaveToFile(allEmployees);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            string jsonContent = await File.ReadAllTextAsync(GetFilePath());
            return JsonConvert.DeserializeObject<IEnumerable<Employee>>(jsonContent)!;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesBySalaryRange(decimal minSalary, decimal maxSalary)
        {
            var allEmployees = await GetAllEmployees();
            return allEmployees.Where(e => e.Salary >= minSalary && e.Salary <= maxSalary);
        }

        public async Task<(decimal malePercentage, decimal femalePercentage)> GetGenderPercentages()
        {
            var allEmployees = await GetAllEmployees();

            if (allEmployees == null || !allEmployees.Any())
            {
                return (0, 0);
            }

            var totalEmployees = allEmployees.Count();
            var maleCount = allEmployees.Count(e => e.Gender == "M");
            var femaleCount = allEmployees.Count(e => e.Gender == "F");

            decimal malePercentage = Math.Round(((decimal)maleCount / totalEmployees) * 100, 1);
            decimal femalePercentage = Math.Round(((decimal)femaleCount / totalEmployees) * 100, 1);

            return (malePercentage, femalePercentage);
        }

        public async Task<IEnumerable<Employee>> GetNonDuplicateEmployees()
        {
            var allEmployees = await GetAllEmployees();
            return allEmployees.GroupBy(e => e.Document)
                              .Select(group => group.First());
        }

        public async Task<IEnumerable<Employee>> GetSalaryAdjustments()
        {
            var allEmployees = await GetAllEmployees();
            return allEmployees.Select(e =>
            {
                if (e.Salary > 100000)
                    e.Salary *= 1.25m; // Aumento del 25% para salarios mayores a 100,000
                else
                    e.Salary *= 1.30m; // Aumento del 30% para salarios menores a 100,000
                return e;
            });
        }

        public async Task<Employee> GetEmployeeByDocument(string document)
        {
            var allEmployees = await GetAllEmployees();
            return allEmployees.FirstOrDefault(e => e.Document == document)!;
        }

        public async Task RemoveEmployee(string document)
        {
            var allEmployees = (await GetAllEmployees()).ToList();
            var employeeToRemove = allEmployees.FirstOrDefault(e => e.Document == document);

            if (employeeToRemove != null)
            {
                allEmployees.Remove(employeeToRemove);
                await SaveToFile(allEmployees);
            }
        }

        private async Task SaveToFile(IEnumerable<Employee> employees)
        {
            var json = JsonConvert.SerializeObject(employees, Formatting.Indented);
            await File.WriteAllTextAsync(filePath, json);
        }

    }
}
