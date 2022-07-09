using System.ComponentModel.DataAnnotations;

namespace MonsterMexa.API.Contracts
{
    public class GetProductRequest
    {
        [Required]
        public string Name { get; set; }

        public int Id { get; set; }

        public int Size { get; set; }
    }
}
