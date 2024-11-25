using System.ComponentModel.DataAnnotations;

namespace RazorPageDeneme.Models
{
    public class SalesTransactionM
    {
        [Key]
        public int SalesID { get; set; }
        public DateTime Date { get; set; }
        public int Piece { get; set; } //adet,tane
        public decimal Price { get; set; }
        public decimal SumPrice { get; set; } //toplam tutar

        public int ProductID { get; set; }
        public ProductM Product { get; set; }
        public int CustomerID { get; set; }
        public CustomerM Customer { get; set; }

        
        

    }
}
