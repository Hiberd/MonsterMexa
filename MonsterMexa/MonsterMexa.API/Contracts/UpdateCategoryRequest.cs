using System.ComponentModel.DataAnnotations;
using MonsterMexa.Domain;

namespace MonsterMexa.API.Contracts
{
    public class UpdateCategoryRequest
    {
        public UpdateCategoryRequest(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [Required]
        [StringLength(Category.MaxCategoryName)]
        public string Name { get; set; }

        public int Id { get; set; }
    }
}
