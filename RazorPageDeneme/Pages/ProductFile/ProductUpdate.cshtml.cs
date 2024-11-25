using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageDeneme.Models;

namespace RazorPageDeneme.Pages.ProductFile;

public class ProductUpdateModel : PageModel
{
    private readonly Context _context;

    [BindProperty]
    public ProductM Product { get; set; }
    public ProductUpdateModel(Context context)
    {
        _context = context;
    }
    public void OnGet(int id)
    {
        Product = _context.Products.Find(id);
        

    }

    public async Task<IActionResult> OnPost()
    {

        Product.Status = true;
        _context.Products.Update(Product);
        await _context.SaveChangesAsync();
        return RedirectToPage("Product");

    }
}
