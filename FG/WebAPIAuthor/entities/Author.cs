

using OtroProyect;

public class Author {
    public int Id { get; set; }
    public required string Name { get; set; }

    public required List<Book> MyProperty { get; set; }
}