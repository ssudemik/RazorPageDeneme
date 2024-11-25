using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPageDeneme.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace RazorPageDeneme.Pages.ProductFile
{
    public class ProductSaleModel : PageModel
    {
        private readonly Context _context;

        public ProductSaleModel(Context context)
        {
            _context = context;
        }

        [BindProperty]
        public SalesTransactionM SalesTransaction { get; set; }

        [BindProperty]
        public ProductM Product { get; set; }

        public SelectList Customers { get; set; }

        public void OnGet(int id)
        {
            Product = _context.Products.FirstOrDefault(p => p.ProductID == id);
            Customers = new SelectList(_context.Customers.Select(x => new
            {
                Text = x.CustomerName + " " + x.CustomerSurname,
                Value = x.CustomerID
            }).ToList(), "Value", "Text");
        }

        public async Task<IActionResult> OnPost()
        {
            if (SalesTransaction == null || Product == null)
            {
                return Page();
            }

            SalesTransaction.Date = DateTime.Now;
            _context.SalesTransactions.Add(SalesTransaction);

            var productInDb = _context.Products.FirstOrDefault(p => p.ProductID == SalesTransaction.ProductID);
            if (productInDb != null)
            {
                productInDb.Stock -= (short)SalesTransaction.Piece;
            }

            _context.SaveChangesAsync();

            return RedirectToPage("Product");
        }
    }
}
