using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/authors")]
public class AuthorsController : ControllerBase
{

    private readonly ApplicationDbContext _context;

    public AuthorsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Author>>> GET()
    {

        return await _context.Authors.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult> POST([FromBody] Author author)
    {
        _context.Add(author);

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("/{id:int}")]
    public async Task<ActionResult> Put(int id, Author author)
    {
        if (author.Id != id)
        {
            return BadRequest("El id del autor no coincide con el id de la Url");
        }

        var existAuthorId = await _context.Authors.AnyAsync(author => author.Id == id);

        if (!existAuthorId)
        {
            return NotFound();
        }

        _context.Update(author);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("/{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {

        var existAuthor = await _context.Authors.AnyAsync(author => author.Id == id);

        if (!existAuthor)
        {
            return NotFound();
        }

        _context.Remove(existAuthor);
        await _context.SaveChangesAsync();
        return Ok();

    }

}