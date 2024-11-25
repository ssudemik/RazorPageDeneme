using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageDeneme.Models;

namespace RazorPageDeneme.Pages.CustomerFile;

public class CustomerUpdateModel : PageModel
{
    private readonly Context _context;

    [BindProperty]
    public CustomerM Customer { get; set; }
    public CustomerUpdateModel(Context context)
    {
        _context = context;
    }
    public void OnGet(int id)
    {
        Customer = _context.Customers.Find(id);
    }

    public async Task<IActionResult> OnPost()
    {

        Customer.Status = true;
         _context.Customers.Update(Customer);
        await _context.SaveChangesAsync();
        return RedirectToPage("Customer");



    }
}
