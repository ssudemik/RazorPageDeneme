using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageDeneme.Models;

namespace RazorPageDeneme.Pages.ProductFile;

public class ProductAddModel : PageModel
{
    private readonly Context _context;

    [BindProperty]
    public ProductM Product { get; set; }
    public ProductAddModel(Context context)
    {
        _context = context;
    }
    public void OnGet()
    {
        
    }

    //public async Task<IActionResult> OnPost()
    //{
    //    Product.Status = true;
    //    _context.Products.Add(Product);
    //    await _context.SaveChangesAsync();

    //    return RedirectToPage("Product");
    //}

    public async Task<IActionResult> OnPost()
    {

        Product.Status = true;
        await _context.Products.AddAsync(Product);
        await _context.SaveChangesAsync();
        return RedirectToPage("Product");
    }
}
