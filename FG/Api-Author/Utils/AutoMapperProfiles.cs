using Api_Author.Dtos.Authors;
using Api_Author.Dtos.Books;
using Api_Author.Dtos.Comments;
using Api_Author.Entities;
using AutoMapper;

namespace Api_Author.Utils;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<CreateAuthorDto, Author>();
        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorDtoWithBook>()
                .ForMember(authorDto => authorDto.Books, options => options.MapFrom(MapAuthorDtoBooks));
        CreateMap<UpdateAuthorDto, Author>();

        CreateMap<CreateBookDto, Book>()
                .ForMember(book => book.AuthorBooks, options => options.MapFrom(MapAuthorsBooks));
        CreateMap<Book, BookDto>();
        CreateMap<Book, BookDtoWithAuthor>()
                .ForMember(bookDto => bookDto.Authors, options => options.MapFrom(MapBookDtoAuthors));
        CreateMap< BookPatchDto, Book>().ReverseMap();


        CreateMap<CreateCommentDto, Comment>();
        CreateMap<Comment, CommentDto>();

    }

    private List<BookDto> MapAuthorDtoBooks(Author author, AuthorDto authorDto)
    {
        var result = new List<BookDto>();

        if (author.AuthorBooks == null) return result;

        author.AuthorBooks.ForEach(authorBook =>
        {
            result.Add(new BookDto()
            {
                Id = authorBook.BookId,
                Title = authorBook.Book.Title
            });
        });

        return result;
    }

    private List<AuthorDto> MapBookDtoAuthors(Book book, BookDto bookDto)
    {
        var result = new List<AuthorDto>();

        if (book.AuthorBooks == null) return result;

        book.AuthorBooks.ForEach(authorBook =>
        {
            result.Add(new AuthorDto()
            {
                Id = authorBook.AuthorId,
                Name = authorBook.Author.Name
            });
        });

        return result;
    }

    private List<AuthorBook> MapAuthorsBooks(CreateBookDto createBookDto, Book book)
    {

        var result = new List<AuthorBook>();

        if (createBookDto == null) return result;

        createBookDto.AuthorsIds!.ForEach(authorId =>
        {
            result.Add(new AuthorBook() { AuthorId = authorId });
        });

        return result;
    }


}