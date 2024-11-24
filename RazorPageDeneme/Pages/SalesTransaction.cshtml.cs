using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPageDeneme.Models;
using System.Linq;

namespace RazorPageDeneme.Pages
{
    public class SalesTransactionModel : PageModel
    {
        private readonly Context _context;
        public IEnumerable<SalesTransaction> SalesTransactions { get; set; }

        public SalesTransactionModel(Context context)
        {
            _context = context;
        }

        [BindProperty]
        public SalesTransaction SalesTransaction { get; set; }

        public List<SelectListItem> Customers { get; set; }
        public ProductM Product { get; set; }

        public void OnGet(int id)
        {
            // Ürün bilgilerini getir
            Product = _context.Products.Find(id);

            if (Product != null)
            {
                SalesTransaction = new SalesTransaction
                {
                    ProductID = Product.ProductID,
                    Price = Product.SalePrice
                };
            }

            // Müþteri listesini doldur
            Customers = _context.Customers.Select(c => new SelectListItem
            {
                Text = c.CustomerName,
                Value = c.CustomerID.ToString()
            }).ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Yeniden müþteri listesini doldur
                Customers = _context.Customers
                    .Select(c => new SelectListItem
                    {
                        Value = c.CustomerID.ToString(),
                        Text = c.CustomerName
                    }).ToList();

                return Page();
            }

            // Toplam fiyatý sunucu tarafýnda doðrula
            SalesTransaction.SumPrice = SalesTransaction.Piece * SalesTransaction.Price;
            SalesTransaction.Date = DateTime.Now;

            _context.SalesTransactions.Add(SalesTransaction);
            _context.SaveChanges();

            return RedirectToPage("/Sale/Index");
        }
    }
}
