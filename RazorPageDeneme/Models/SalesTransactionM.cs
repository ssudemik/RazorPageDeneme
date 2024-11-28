using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPageDeneme.Models
{
    public class SalesTransactionM
    {
        [Key]
        public int SalesID { get; set; }

        [Required] 
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Adet en az 1 olmalıdır.")]
        public int Piece { get; set; } //adet,tane
        public decimal Price { get; set; }
        public decimal SumPrice { get; set; } //toplam tutar

        [Required]
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public ProductM Product { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public CustomerM Customer { get; set; }

    }
}
