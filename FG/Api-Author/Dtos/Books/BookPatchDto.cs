using System.ComponentModel.DataAnnotations;
using Api_Author.Validation;

namespace Api_Author.Dtos.Books;

public class BookPatchDto {


    [FirstLetterMayus]
    [StringLength(maximumLength: 250)]
    [Required]
    public string Title { get; set; }

    public DateTime PublishDate { get; set; }
}