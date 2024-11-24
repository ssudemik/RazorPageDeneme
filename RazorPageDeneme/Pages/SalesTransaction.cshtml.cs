using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPageDeneme.Models;

namespace RazorPageDeneme.Pages
{
    public class SalesTransactionModel : PageModel
    {
        private readonly Context _context;

        public List<Product> Product { get; private set; }

        public void OnGet(int id)
        {

            Product = _context.Products.ToList();

        }
        public IActionResult OnPost(SalesTransaction salesTransaction)
        {
            if (salesTransaction == null)
            {
                // Hata mesaj� ver, bir �eyler yanl�� gitmi� olabilir
                return Page();
            }

            // Sat�� i�lemi tarihini ayarla
            salesTransaction.Date = DateTime.Now;
            _context.SalesTransactions.Add(salesTransaction);
            _context.SaveChanges();

            return RedirectToPage("/Sale/Index");
        }
    }
}