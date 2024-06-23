using System.ComponentModel.DataAnnotations;

namespace Api_Author;

public class UpdateBookDto
{
    public string Title { get; set; }   

    [Required]
    public List<int> AuthorsIds { get; set; }
}
