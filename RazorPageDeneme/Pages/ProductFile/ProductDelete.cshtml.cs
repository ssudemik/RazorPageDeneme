using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageDeneme.Models;

namespace RazorPageDeneme.Pages.ProductFile
{
    public class ProductDeleteModel : PageModel
    {
        private readonly Context _context;

        [BindProperty]
        public ProductM Product { get; set; }
        public ProductDeleteModel(Context context)
        {
            _context = context;
        }
        public void OnGet(int id)
        {
            Product = _context.Products.Find(id);


        }

        public async Task<IActionResult> OnPost()
        {

            var productFromDb = _context.Products.Find(Product.ProductID);
            if (productFromDb != null)
            {
                _context.Products.Remove(productFromDb);
                await _context.SaveChangesAsync();
                return RedirectToPage("Product");

            }

            return Page();
        }
    }
}
