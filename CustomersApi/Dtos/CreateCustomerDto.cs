using System.ComponentModel.DataAnnotations;

namespace CustomersApi.Dtos;

public class CreateCustomerDto
{
    [Required(ErrorMessage = "El nombre propio tiene q estar especificado")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "El apellido propio tiene q estar especificado")]
    public string LastName { get; set; }
    [RegularExpression("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "El email no es valido")]
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}