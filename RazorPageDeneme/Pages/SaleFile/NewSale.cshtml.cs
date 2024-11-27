using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPageDeneme.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RazorPageDeneme.Pages.SaleFile;

public class NewSaleModel : PageModel
{
    private readonly Context _context;


    [BindProperty]
    public SalesTransactionM SalesTransaction { get; set; } 

    public NewSaleModel(Context context)
    {
        _context = context;
    }

    public List<SelectListItem> ProductList { get; set; } 
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

    public void OnGet(int productId)
    {

        // Ürün ve müþteri listelerini doldur
        ProductList = _context.Products
            .Select(x => new SelectListItem
            {
                Text = x.ProductName,
                Value = x.ProductID.ToString()
            }).ToList();

        CustomerList = _context.Customers
            .Select(x => new SelectListItem
            {
                Text = x.CustomerName + " " + x.CustomerSurname,
                Value = x.CustomerID.ToString()
            }).ToList();

       
    }

    public async Task<IActionResult> OnPost()
    {
        
        await _context.SalesTransactions.AddAsync(SalesTransaction);
        await _context.SaveChangesAsync();
        return RedirectToPage("SalesTransaction");
    }
}
