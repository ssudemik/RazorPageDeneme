using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPageDeneme.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace RazorPageDeneme.Pages.SaleFile;

public class SalesTransactionModel : PageModel
{
    private readonly Context _context;

    public SalesTransactionModel(Context context)
    {
        _context = context;
    }

    [BindProperty]
    public SalesTransactionM SalesTransaction { get; set; }

    public ProductM Product { get; set; }  // Product nesnesini burada tan�ml�yoruz

    public SelectList Products { get; set; }
    public SelectList Customers { get; set; }

    // OnGet metodunda �r�n ve m��teri listesini y�kl�yoruz
    public void OnGet()
    {
        // �r�nlerin listesini dropdown i�in haz�rl�yoruz
        Products = new SelectList(_context.Products, "ProductID", "ProductName");

        // M��terilerin listesini dropdown i�in haz�rl�yoruz
        Customers = new SelectList(_context.Customers, "CustomerID", "CustomerName");
    }

    // Product fiyat�n� almak i�in bir metod yaz�yoruz
    public IActionResult OnGetGetProductPrice(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.ProductID == id);

        if (product != null)
        {
            return new JsonResult(new { salePrice = product.SalePrice });
        }

        return NotFound();
    }

    // Post metodu sat�� i�lemini kaydetmek i�in
    public async Task<IActionResult> OnPost()
    {
        
        // �r�n� buluyoruz ve toplam fiyat� hesapl�yoruz
        Product = _context.Products.FirstOrDefault(p => p.ProductID == SalesTransaction.ProductID);
        SalesTransaction.SumPrice = SalesTransaction.Piece * Product.SalePrice;
        SalesTransaction.Date = DateTime.Now;

        // Sat�� i�lemini veritaban�na ekliyoruz
        await _context.SalesTransactions.AddAsync(SalesTransaction);
        _context.SaveChanges();

        // Sat�lan �r�n�n sto�undan miktar kadar d���r�yoruz
        Product.Stock -= (short)SalesTransaction.Piece;
        _context.SaveChanges();

        return RedirectToPage("SalesTransaction");
    }
}
