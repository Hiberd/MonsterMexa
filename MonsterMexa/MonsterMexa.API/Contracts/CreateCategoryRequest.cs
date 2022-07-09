using System.ComponentModel.DataAnnotations;
using MonsterMexa.Domain;

namespace MonsterMexa.API.Contracts
{
    public class CreateCategoryRequest
    {
        public CreateCategoryRequest(string name)
        {
            Name = name;
        }

        [Required]
        [StringLength(Category.MaxCategoryName)]
        public string Name { get; set; }
    }
}
