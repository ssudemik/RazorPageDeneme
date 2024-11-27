using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPageDeneme.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using RazorPageDeneme.Pages.SaleFile;

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
        public ProductM Product { get; set; }

        public List<SelectListItem> CustomerList { get; set; }

        public void OnGet(int id)
        {
            Product = _context.Products.FirstOrDefault(p => p.ProductID == id);

            CustomerList = _context.Customers
            .Select(x => new SelectListItem
            {
                Text = x.CustomerName + " " + x.CustomerSurname,
                Value = x.CustomerID.ToString()
            }).ToList();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // SalesTransaction nesnesinin de�erlerini kontrol et ve logla
            Console.WriteLine($"ProductID: {SalesTransaction.ProductID}, CustomerID: {SalesTransaction.CustomerID}, Piece: {SalesTransaction.Piece}, Price: {SalesTransaction.Price}");

            if (SalesTransaction.ProductID <= 0 || SalesTransaction.CustomerID <= 0 || SalesTransaction.Piece <= 0 || SalesTransaction.Price <= 0)
            {
                ModelState.AddModelError(string.Empty, "Gerekli alanlar doldurulmam��.");
                return Page();
            }

            try
            {
                _context.SalesTransactions.Add(SalesTransaction);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Sat�� i�lemi s�ras�nda bir hata olu�tu: " + ex.Message);
                return Page();
            }

            return RedirectToPage("/SaleFile/SalesTransaction");
        }

        //public async Task<IActionResult> OnPost()
        //{


        //    //if (!ModelState.IsValid)
        //    //{
        //    //    // Model ge�erli de�ilse hata mesajlar�n� d�nd�r
        //    //    return Page();
        //    //}

        //    //await _context.SalesTransactions.AddAsync(SalesTransaction);
        //    //await _context.SaveChangesAsync();
        //    //return Redirect("/SaleFile/SalesTransaction");


        //}
    }
}