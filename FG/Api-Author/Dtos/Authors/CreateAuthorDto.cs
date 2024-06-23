using System.ComponentModel.DataAnnotations;
using Api_Author.Validation;

namespace Api_Author.Dtos.Authors;

public class CreateAuthorDto
{

    [Required(ErrorMessage = "El campo name es requerido")]
    [StringLength(maximumLength: 120, ErrorMessage = "El campo name debe de tener un maximo de 5 caracteres")]
    [FirstLetterMayus]
    public required string Name { get; set; }
}