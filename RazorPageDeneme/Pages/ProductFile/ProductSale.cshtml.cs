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
        public ProductM deneme { get; set; }

        public List<SelectListItem> CustomerList { get; set; }

        public JsonResult OnGetProductPrice(int productId)
        {
            // ProductID'ye göre ürün bilgilerini getir
            var product = _context.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                return new JsonResult(new { price = product.SalePrice });
            }
            return new JsonResult(new { price = 0 }); // Ürün bulunamazsa 0 döner
        }
       
        public void OnGet(int id)
        {

            deneme = _context.Products.FirstOrDefault(p => p.ProductID == id);

            CustomerList = _context.Customers
                .Select(x => new SelectListItem
                {
                    Text = x.CustomerName + " " + x.CustomerSurname,
                    Value = x.CustomerID.ToString()
                }).ToList();
        }

        public async Task<IActionResult> OnPost()
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductID == SalesTransaction.ProductID);

                if (product.Stock >= SalesTransaction.Piece)
                {
                    // Stok düþ
                    product.Stock -= (short)SalesTransaction.Piece;
                    _context.Products.Update(product);
                }
                else
                {
                    // Stok yetersizse hata mesajý
                    ModelState.AddModelError("", "Not enough stock for the selected product.");
                    return Page(); // Ayný sayfaya dön
                }
          

            await _context.SalesTransactions.AddAsync(SalesTransaction);
            await _context.SaveChangesAsync();
            return Redirect("Product");
        }

    }

}
