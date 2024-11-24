using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageDeneme.Models;

namespace RazorPageDeneme.Pages.CustomerFile
{
    public class CustomerModel : PageModel
    {
        private readonly Context _context;
        public IEnumerable<CustomerM> Customers { get; set; }
        public CustomerModel(Context context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Customers = _context.Customers.ToList();
        }
    }
}
