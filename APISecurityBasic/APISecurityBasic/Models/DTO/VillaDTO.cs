using System.ComponentModel.DataAnnotations;

namespace APISecurityBasic.Models.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]    
        public string Name { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public int Sqft { get; set; }
        [Required]
        public int Occupany { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Amentity { get; set; }
    }
}
