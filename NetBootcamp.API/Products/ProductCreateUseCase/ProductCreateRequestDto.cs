using System.ComponentModel.DataAnnotations;

namespace NetBootcamp.API.Products.DTOs
{

    public record ProductCreateRequestDto(string Name, decimal Price);


    // yeni hal
    // Model validation - Attiribute - Data Anotation
    //public record ProductCreateRequestDto(
    //    [Required(ErrorMessage = "ürün ismi gereklidir")]
    //    [StringLength(10,ErrorMessage ="isim alanı en fazla 10 karakter olabilir")]
    //    string Name,
    //    [Range(1,Int32.MaxValue)] decimal Price,
    //    [Url(ErrorMessage ="url formatı yanlış")]
    //    string url);
}

//eski hal
//public class ProductCreateRequestDtoLegacy
//{
//    public string Name { get; set; }
//    public decimal Price { get; set; }

//    public ProductCreateRequestDtoLegacy(string name, decimal price)
//    {
//        Name = name;
//        Price = price;
//    }
//}