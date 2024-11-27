using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageDeneme.Models;

namespace RazorPageDeneme.Pages.SaleFile;
    public class SalesTransactionModel : PageModel
    {
    private readonly Context _context;
    public IEnumerable<SalesTransactionM> SalesTransactions { get; set; }
    public SalesTransactionModel(Context context)
    {
        _context = context;
    }

    public void OnGet()
    {
        SalesTransactions = _context.SalesTransactions.ToList();
    }
}


