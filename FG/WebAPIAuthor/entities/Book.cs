namespace OtroProyect;

public class Book
{
    public int Id { get; set; }
    public required string Titulo { get; set; }

    public int AuthorId { get; set; }

    public required Author Author { get; set; }
}
