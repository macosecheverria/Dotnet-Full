using Api_Author.Dtos.Books;
using Api_Author.Entities;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Author.Controller;

[ApiController]
[Route("/api/books")]
public class BookController : ControllerBase
{

    private readonly ApplicationDbContext context;

    private readonly IMapper mapper;

    public BookController(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<BookDto>>> FindAllBook()
    {
        var booksDb = await context.Books.ToListAsync();

        var books = mapper.Map<List<BookDto>>(booksDb);


        return Ok(books);
    }

    [HttpGet("{id:int}", Name = "getBook")]
    public async Task<ActionResult<BookDtoWithAuthor>> FindBookById(int id)
    {

        var bookDb = await context.Books
                .Include(bookDb => bookDb.AuthorBooks)
                .ThenInclude(authorBookDb => authorBookDb.Author)
                .FirstOrDefaultAsync(book => book.Id == id);

        if (bookDb == null) return NotFound($"Book with id {id} not found");

        bookDb.AuthorBooks = bookDb.AuthorBooks.OrderBy(key => key.Order).ToList();

        var book = mapper.Map<BookDtoWithAuthor>(bookDb);

        return Ok(book);
    }


    [HttpPost]
    public async Task<ActionResult<CreateBookDto>> CreateBook(CreateBookDto createBookDto)
    {

        // var existAuthor = await context.Authors.AnyAsync(author => author.Id == createBookDto.Id);

        // if (!existAuthor) return BadRequest($"Id no exist with author {createBookDto.Id}");

        if (createBookDto.AuthorsIds == null)
        {
            return BadRequest("No se puede crear un libro sin autores");
        }

        var authorsIds = await context.Authors
                .Where(authorDb => createBookDto.AuthorsIds.Contains(authorDb.Id))
                .Select(author => author.Id)
                .ToListAsync();

        if (createBookDto.AuthorsIds.Count != authorsIds.Count)
        {
            return BadRequest("No existe unos de los autores enviados");
        }

        var book = mapper.Map<Book>(createBookDto);

        AssingOrderAuthors(book);

        context.Add(book);
        await context.SaveChangesAsync();

        var bookDto = mapper.Map<BookDto>(book);
        return CreatedAtRoute("getBook", new { id = book.Id }, bookDto);
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult<BookDto>> UpdateBook([FromRoute] int id, [FromBody] UpdateBookDto updateBookDto)
    {
        var book = await context.Books
                .Include(bookDb => bookDb.AuthorBooks)
                .FirstOrDefaultAsync(bookDb => bookDb.Id == id);

        if (book == null) return NotFound(new { error = $"No se ha encontrado el libro con el id {id}" });

        book = mapper.Map(updateBookDto, book);
        AssingOrderAuthors(book);

        await context.SaveChangesAsync();

        var bookDto = mapper.Map<BookDto>(book);

        return Ok(bookDto);
    }


    [HttpPatch("{id:int}")]
    public async Task<ActionResult> UpdateBookPatch([FromRoute] int id, JsonPatchDocument<BookPatchDto> patchDocument){

        if(patchDocument == null) return BadRequest(new {error = "Se ha producido un error al envio del formato"});

        var book = await context.Books.FirstOrDefaultAsync(bookDb => bookDb.Id == id);

        if(book == null) return NotFound(new {error = $"El libro con el id {id} no existe" });


        var bookDto = mapper.Map<BookPatchDto>(book);

        patchDocument.ApplyTo(bookDto, ModelState);

        var isNotValid =TryValidateModel(bookDto);

        if(!isNotValid) return BadRequest(ModelState);

        var updatedBook = mapper.Map<Book>(bookDto);

        context.Update(updatedBook);

        await context.SaveChangesAsync();

        return Ok(new {messsage = "Libro actualizado correctamente", bookDto});
    }


    private void AssingOrderAuthors(Book book)
    {
        if (book.AuthorBooks != null)
        {
            for (int i = 0; i < book.AuthorBooks.Count; i++)
            {
                book.AuthorBooks[i].Order = i;
            }
        }
    }

}