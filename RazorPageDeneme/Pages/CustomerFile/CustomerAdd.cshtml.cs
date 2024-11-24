using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageDeneme.Models;

namespace RazorPageDeneme.Pages.CustomerFile;

public class CustomerAddModel : PageModel
{
    private readonly Context _context;

    [BindProperty]
    public CustomerM Customer { get; set; }
    public CustomerAddModel(Context context)
    {
        _context = context;
    }
    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPost()
    {

        Customer.Status = true;
        await _context.Customers.AddAsync(Customer);
        await _context.SaveChangesAsync();
        return RedirectToPage("Customer");
    }
}
