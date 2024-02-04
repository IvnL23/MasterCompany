using System.ComponentModel.DataAnnotations;

namespace MasterCompanyAPI.Models
{
    public class Employee
    {
        [Required]
        public string Name { get; set; }  = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(11, ErrorMessage = "Document must have {1} characters.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Document can only contain numbers.")]
        public string Document { get; set; } = string.Empty;

        [Required]
        public decimal Salary { get; set; }

        [Required]
        [StringLength(1, ErrorMessage = "Gender only accepts one character.")]
        [RegularExpression("^[MF]$", ErrorMessage = "Gender only accepts 'M' or 'F'.")]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public string Position { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.MinValue;

    }
}
