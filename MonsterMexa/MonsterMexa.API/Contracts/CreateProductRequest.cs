using System.ComponentModel.DataAnnotations;
using MonsterMexa.Domain;

namespace MonsterMexa.API.Contracts
{
    public class CreateProductRequest
    {
        public CreateProductRequest(string name, int size)
        {
            Name = name;
            Size = size;
        }

        [Required]
        [StringLength(Product.MaxProductName)]
        public string Name { get; set; }

        [Range(Product.MinProductSize, Product.MaxProductSize)]
        public int Size { get; set; }
    }
}