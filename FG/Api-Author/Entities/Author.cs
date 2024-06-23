
namespace Api_Author.Entities;

public class Author
{
    public int Id { get; set; }

    public  required string Name { get; set; }

    public  required List<AuthorBook> AuthorBooks { get; set; }


    // [Range(18, 120)]
    // [NotMapped]
    // public int Age { get; set; }

    // [NotMapped]
    // [CreditCard]
    // public required string CreditCard { get; set; }

    // [Url]
    // [NotMapped]
    // public required string URL { get; set; }

    // public int Menor { get; set; }

    // public int Mayor { get; set; }

    // public required List<Book> BookList { get; set; }

    // public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    // {
    //     if(string.IsNullOrEmpty(Name)){
    //         var firstLetter = Name[0].ToString();

    //         if(firstLetter != firstLetter.ToUpper()){
    //             yield return new ValidationResult("La primera letra debe de ser mayuscula", new string[]{nameof(Name)});
    //         }
    //     }

    //     // if(Menor > Mayor){
    //     //     yield return new ValidationResult("Este valor no puede ser mas grande que el campo Mayor", new string[]{nameof(Menor)});
    //     // }
    // }
}