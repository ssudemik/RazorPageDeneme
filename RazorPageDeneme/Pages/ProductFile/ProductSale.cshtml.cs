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
            // Ürün bilgilerini yükle
            Product = _context.Products.Find(id);

            // Müþteri listesini doldur
            Customers = _context.Customers
                .Select(c => new SelectListItem
                {
                    Text = c.CustomerName,
                    Value = c.CustomerID.ToString()
                }).ToList();

            // Baþlangýç deðerleri
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

            // Satýþ iþlemini kaydet
            SalesTransaction.Date = DateTime.Now;
            _context.SalesTransactions.Add(SalesTransaction);
            _context.SaveChanges();

            return RedirectToPage("/Products"); // Ürünler sayfasýna dön
        }
    }
}
