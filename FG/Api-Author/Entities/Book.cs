using System.ComponentModel.DataAnnotations;

namespace Api_Author.Entities;
public class Book
{
    public int Id { get; set; }

    [Required]
    [StringLength(maximumLength: 250)]
    public required string Title { get; set; }

    public DateTime PublishDate { get; set; }

    public required List<Comment> Comments { get; set; }

    public required List<AuthorBook> AuthorBooks { get; set; }

}