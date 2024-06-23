using Api_Author.Dtos.Authors;
using Api_Author.Entities;
using AutoMapper;

// using Api_Author.Filters;
// using Api_Author.Services;
// using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Author.Controller;

[ApiController]
[Route("/api/authors")]
public class AuthorController : ControllerBase
{

    private readonly ApplicationDbContext context;

    private readonly IMapper mapper;

    // private readonly ServiceA serviceA;

    // private readonly ServiceTransient serviceTransient;

    // private readonly ServiceScope serviceScope;

    // private readonly ServiceSingleton serviceSingleton;

    // private readonly ILogger<AuthorController> logger;

    public AuthorController(
        ApplicationDbContext context,
        IMapper mapper
        // ServiceA serviceA,
        // ServiceTransient serviceTransient,
        // ServiceScope serviceScope,
        // ServiceSingleton serviceSingleton,
        // ILogger<AuthorController> logger
        )
    {
        this.context = context;
        this.mapper = mapper;
        // this.serviceA =serviceA;
        // this.serviceTransient = serviceTransient;
        // this.serviceScope = serviceScope;
        // this.serviceSingleton = serviceSingleton;
        // this.logger = logger;
    }

    [HttpGet]
    // [Authorize]
    public async Task<ActionResult<List<AuthorDto>>> FindAll()
    {
        // logger.LogInformation("Obteniendo todos los datos del autor");
        // logger.LogWarning("Esto es un mensaje de warning de prueba");

        // var authors = await context.Authors.Include(x => x.BookList).ToListAsync();

        var authors = await context.Authors.ToListAsync();
        var authorsMapper = mapper.Map<List<AuthorDto>>(authors);

        return Ok(authorsMapper);

    }

    [HttpGet("{id:int}", Name = "getAuthor")]
    // [ServiceFilter(typeof(MyFilterAction))]
    public async Task<ActionResult<AuthorDtoWithBook>> FindById([FromRoute] int id)
    {
        Author authorDb = await context.Authors
                .Include(authorDb => authorDb.AuthorBooks)
                .ThenInclude(authorBooksDb => authorBooksDb.Book)
                .FirstAsync(author => author.Id == id);

        if (authorDb == null)
        {
            return NotFound();
        }

        var author = mapper.Map<AuthorDtoWithBook>(authorDb);

        return Ok(author);
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<List<AuthorDto>>> FindByName([FromRoute] string name)
    {

        var authors = await context.Authors
            .Where(author => author.Name.Contains(name))
            .ToListAsync();

        if (authors == null)
        {
            return NotFound($"Author with name {name} not found");
        }

        var authorsMapper = mapper.Map<List<AuthorDto>>(authors);

        return Ok(authorsMapper);
    }


    // [HttpGet("Guid")]
    // [ResponseCache(Duration = 10)]
    // [ServiceFilter(typeof(MyFilterAction))]
    // public ActionResult GetGuids()
    // {
    //     return Ok(new
    //     {
    //         AuthorControllerTrasient = serviceTransient.Guid,
    //         ServiceATrasient = serviceA.GetTransient(),
    //         AuthorControllerScope = serviceScope.Guid,
    //         ServiceAScope = serviceA.GetScope(),
    //         AuthorControllerSingleton = serviceSingleton.Guid,
    //         ServiceASingleton = serviceA.GetSingleton(),

    //     });
    // }

    // [HttpGet("exception/{ok}")]
    // public ActionResult GetException([FromRoute] string ok){

    //     if(ok != null){
    //         throw new NotImplementedException();
    //     }

    //     return Ok();
    // }

    [HttpPost]
    public async Task<ActionResult<Author>> Create([FromBody] CreateAuthorDto createAuthorDto)
    {

        var existName = await context.Authors.AnyAsync(x => x.Name == createAuthorDto.Name);

        if (existName)
        {
            return BadRequest($"El nombre del author {createAuthorDto.Name} ya existe");
        }

        var author = mapper.Map<Author>(createAuthorDto);

        context.Add(author);

        await context.SaveChangesAsync();

        var authorDto =  mapper.Map<AuthorDto>(author);

        return CreatedAtRoute("getAuthor", new {id = author.Id}, authorDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<AuthorDto>> Update([FromRoute] int id, [FromBody] UpdateAuthorDto updateAuthorDto)
    {

        var existAuthor = await context.Authors.FirstOrDefaultAsync(author => author.Id == id);

        if (existAuthor == null)
        {
            return NotFound($"Author with id {id} not found");
        }

        existAuthor.Name = updateAuthorDto.Name;

        var author =  mapper.Map<AuthorDto>(existAuthor);

        context.Update(existAuthor);
        await context.SaveChangesAsync();

        return Ok(author);

    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var author = await context.Authors.FirstAsync(author => author.Id == id);

        if (author == null)
        {
            return NotFound($"Author with id {id} not found");
        }

        context.Authors.Remove(author);
        await context.SaveChangesAsync();

        return Ok();

    }

}