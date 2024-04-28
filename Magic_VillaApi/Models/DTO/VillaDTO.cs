using System.ComponentModel.DataAnnotations;

namespace Magic_VillaApi.Models.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Occpancy { get; set; }
        public string Sqrft { get; set; }
    }
}
