using Api_Author.Dtos.Comments;
using Api_Author.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Author.Controller;

[ApiController]
[Route("api/books/{bookId:int}/comments")]
public class CommentController : ControllerBase
{

    private readonly ApplicationDbContext context;

    private readonly IMapper mapper;

    public CommentController(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet(Name = "getComment")]
    public async Task<ActionResult<List<Comment>>> GetAllCommetById([FromRoute] int bookId)
    {

        var existBook = await ExistBook(bookId);

        if (!existBook)
        {
            return NotFound($"El libro con el id {bookId} no existe");
        }

        var bookCommentsDb = await context.Comments.Where(commentDb => commentDb.BookId == bookId).ToListAsync();

        if (bookCommentsDb == null)
        {
            return BadRequest($"No se ha podido encontrar commentarios del id del libro {bookId}");
        }

        var bookComments = mapper.Map<List<CommentDto>>(bookCommentsDb);

        return Ok(bookComments);
    }

    [HttpPost]
    public async Task<ActionResult> CreateComment(
        [FromRoute] int bookId,
        [FromBody] CreateCommentDto createCommentDto)
    {
        var existBook = await ExistBook(bookId);

        if (!existBook)
        {
            return NotFound($"El libro con el id {bookId} no existe");
        }

        var comment = mapper.Map<Comment>(createCommentDto);
        comment.BookId = bookId;

        context.Add(comment);

        await context.SaveChangesAsync();

        var commentDto = mapper.Map<CommentDto>(comment);

        return CreatedAtRoute("getComment", new { id = comment.BookId, bookId = bookId }, commentDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CommentDto>> UpdateComment(
        [FromRoute] int bookId,
        [FromRoute] int id,
        [FromBody] UpdateCommentDto updateCommentDto)
    {

        var existBookId = await ExistBook(bookId);

        if (!existBookId) return NotFound($"El libro con el id {bookId} no existe");

        var existComment = await context.Comments.FirstOrDefaultAsync(commentDb => commentDb.Id == id);

        if (existComment == null) return NotFound($"El comentario del libro con el id {id} no existe");

        existComment.Content = updateCommentDto.Content;

        context.Update(existComment);
        await context.SaveChangesAsync();

        return Ok();
    }

    private async Task<bool> ExistBook(int bookId)
    {
        return await context.Books.AnyAsync(bookDb => bookDb.Id == bookId);
    }
}