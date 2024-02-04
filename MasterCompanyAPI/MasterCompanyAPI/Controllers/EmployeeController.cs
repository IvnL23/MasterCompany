using MasterCompanyAPI.Models;
using MasterCompanyAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Reflection.Metadata;

namespace MasterCompanyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController(IEmployeeService employeeService) : Controller
    {
        private readonly IEmployeeService _employeeService = employeeService;

        /// <summary>
        /// Gets the list of all employees.
        /// </summary>
        /// <response code="200">Returns the list of all employees.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();
            return Json(new { employees });
        }

        /// <param name="minSalary">Minimum salary of the range.</param>
        /// <param name="maxSalary">Maximum salary of the range.</param>
        /// <summary>
        /// Get employees by salary range.
        /// </summary>
        /// <response code="200">Returns the list of employees within the salary range.</response>
        /// <response code="400">The maximum wage is less than the minimum salary.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns> 
        [HttpGet("salary")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesBySalaryRange(decimal minSalary, decimal maxSalary)
        {
            if(maxSalary < minSalary)
            {
                return BadRequest("The maximum wage can't be less than the minimum salary.");
            }

            var employees = await _employeeService.GetEmployeesBySalaryRange(minSalary, maxSalary);
            return Json(new { employees });
        }

        /// <summary>
        /// Gets the list of all employees without duplicates.
        /// </summary>
        /// <response code="200">Returns the list of all employees without duplicates.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns> 
        [HttpGet("noduplicates")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetNonDuplicateEmployees()
        {
            var employees = await _employeeService.GetNonDuplicateEmployees();
            return Json(new { employees });
        }

        /// <summary>
        /// Shows adjusted salaries.
        /// </summary>
        /// <response code="200">Returns the list of all employees with adjusted salaries.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns> 
        [HttpGet("salaryadjustments")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetSalaryAdjustments()
        {
            var employees = await _employeeService.GetSalaryAdjustments();
            return Json(new { employees });
        }

        /// <summary>
        /// Gets the gender percentages (male and female) of the employees.
        /// </summary>
        /// <response code="200">Returns the gender percentages (male and female) of the employees.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns> 
        [HttpGet("genderpercentages")]
        public async Task<IActionResult> GetGenderPercentages()
        {
            var (malePercentage, femalePercentage) = await _employeeService.GetGenderPercentages();
            return Json(new { femalePercentage, malePercentage });
        }

        /// <summary>
        /// Insert new Employee.
        /// </summary>
        /// <response code="200">Employee inserted.</response>
        /// <response code="400">Employee already exist.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns> 
        [HttpPost]
        public async Task<ActionResult> AddEmployee([FromBody] Employee employee)
        {
            try
            {
                var employeeToRemove = await _employeeService.GetEmployeeByDocument(employee.Document);

                if (employeeToRemove != null)
                {
                    return BadRequest("There is already an employee with this ID number (Cédula).");
                }

                await _employeeService.AddEmployee(employee);
                return Ok("Employee added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <param name="document">ID number(Cédula) of the employee.</param>
        /// <summary>
        /// Delete an employee by his ID number (Cédula).
        /// </summary>
        /// <response code="200">Employee deleted.</response>
        /// <response code="404">The ID number doesn't match any of the records.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns> 
        [HttpDelete("{document}")]
        public async Task<ActionResult> RemoveEmployee(string document)
        {
            try
            {
                var employeeToRemove = await _employeeService.GetEmployeeByDocument(document);

                if (employeeToRemove == null)
                {
                    return NotFound("Employee couldn't be found");
                }

                await _employeeService.RemoveEmployee(document);
                return Ok("Employee successfully removed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
