using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPageDeneme.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public SalesTransaction SalesTransaction { get; set; }
        public ProductM Product { get; set; }
        public List<SelectListItem> Customers { get; set; }

        public void OnGet(int id)
        {
            // �r�n bilgilerini y�kle
            Product = _context.Products.Find(id);

            // M��teri listesini doldur
            Customers = _context.Customers
                .Select(c => new SelectListItem
                {
                    Text = c.CustomerName,
                    Value = c.CustomerID.ToString()
                }).ToList();

            // Ba�lang�� de�erleri
            SalesTransaction = new SalesTransaction
            {
                ProductID = Product.ProductID,
                Price = Product.SalePrice
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Sat�� i�lemini kaydet
            SalesTransaction.Date = DateTime.Now;
            _context.SalesTransactions.Add(SalesTransaction);
            _context.SaveChanges();

            return RedirectToPage("/Products"); // �r�nler sayfas�na d�n
        }
    }
}
