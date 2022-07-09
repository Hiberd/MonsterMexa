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

        [Required]
        [StringLength(Product.MaxProductName)]
        public string Name { get; set; }

        [Range(25, 50)]
        public int Size { get; set; }

        public int Id { get; set; }
    }
}
