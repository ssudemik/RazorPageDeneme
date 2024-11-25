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

    public ProductM Product { get; set; }  // Product nesnesini burada tanýmlýyoruz

    public SelectList Products { get; set; }
    public SelectList Customers { get; set; }

    // OnGet metodunda ürün ve müþteri listesini yüklüyoruz
    public void OnGet()
    {
        // Ürünlerin listesini dropdown için hazýrlýyoruz
        Products = new SelectList(_context.Products, "ProductID", "ProductName");

        // Müþterilerin listesini dropdown için hazýrlýyoruz
        Customers = new SelectList(_context.Customers, "CustomerID", "CustomerName");
    }

    // Product fiyatýný almak için bir metod yazýyoruz
    public IActionResult OnGetGetProductPrice(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.ProductID == id);

        if (product != null)
        {
            return new JsonResult(new { salePrice = product.SalePrice });
        }

        return NotFound();
    }

    // Post metodu satýþ iþlemini kaydetmek için
    public async Task<IActionResult> OnPost()
    {
        
        // Ürünü buluyoruz ve toplam fiyatý hesaplýyoruz
        Product = _context.Products.FirstOrDefault(p => p.ProductID == SalesTransaction.ProductID);
        SalesTransaction.SumPrice = SalesTransaction.Piece * Product.SalePrice;
        SalesTransaction.Date = DateTime.Now;

        // Satýþ iþlemini veritabanýna ekliyoruz
        await _context.SalesTransactions.AddAsync(SalesTransaction);
        _context.SaveChanges();

        // Satýlan ürünün stoðundan miktar kadar düþürüyoruz
        Product.Stock -= (short)SalesTransaction.Piece;
        _context.SaveChanges();

        return RedirectToPage("SalesTransaction");
    }
}
