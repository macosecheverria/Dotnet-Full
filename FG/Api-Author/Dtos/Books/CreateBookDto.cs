using System.ComponentModel.DataAnnotations;
using Api_Author.Validation;

namespace Api_Author.Dtos.Books;

public class CreateBookDto
{

    [FirstLetterMayus]
    [StringLength(maximumLength: 250)]
    [Required]
    public required string Title { get; set; }

    public DateTime PublishDate { get; set; }

    [Required]
    public required List<int> AuthorsIds { get; set; } 
}