
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPageDeneme.Models;

namespace RazorPageDeneme.Pages.ProductFile;

public class ProductModel : PageModel
{
    private readonly Context _context;
    public IEnumerable<ProductM> Products { get; set; }
    public ProductModel(Context context)
    {
        _context = context;
    }

    public void OnGet()
    {
        Products = _context.Products.ToList();
    }

    
}
