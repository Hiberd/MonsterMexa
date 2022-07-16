using System.ComponentModel.DataAnnotations;
using MonsterMexa.Domain;

namespace MonsterMexa.API.Contracts
{
    public class UpdateProductRequest
    {
        public UpdateProductRequest(int id, string name, int size)
        {
            Id = id;
            Name = name;
            Size = size;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(Product.MaxProductName)]
        public string Name { get; set; }

        [Range(Product.MinProductSize, Product.MaxProductSize)]
        public int Size { get; set; }
    }
}
