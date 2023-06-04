using System.ComponentModel.DataAnnotations;

namespace Elearning.Models.DTO;

public class PersonLoginModel
{
    [Required(ErrorMessage = "Duhet plotesuar email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required(ErrorMessage = "Duhet plotesuar password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}