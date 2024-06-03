using System.ComponentModel.DataAnnotations;

namespace ShoelessJoe.DataAccess.DataModels
{
    public class ShoeImage
    {
        [Key]
        public int ShoeImageId { get; set; }

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; }

        [Required]
        public byte[] ShoeArray { get; set; }

        [Required]
        public int ShoeId { get; set; }
        public Shoe Shoe { get; set; }

    }
}
