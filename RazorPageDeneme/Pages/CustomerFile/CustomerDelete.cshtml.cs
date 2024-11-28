using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageDeneme.Models;

namespace RazorPageDeneme.Pages.CustomerFile
{
    public class CustomerDeleteModel : PageModel
    {
        private readonly Context _context;

        [BindProperty]
        public CustomerM Customer { get; set; }
        public CustomerDeleteModel(Context context)
        {
            _context = context;
        }
        public void OnGet(int id)
        {
            Customer = _context.Customers.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            var customerFromDb = _context.Customers.Find(Customer.CustomerID);
            if (customerFromDb != null)
            {
                _context.Customers.Remove(customerFromDb);
                await _context.SaveChangesAsync();
                return RedirectToPage("Customer");

            }

            return Page();

        }
    }
}
