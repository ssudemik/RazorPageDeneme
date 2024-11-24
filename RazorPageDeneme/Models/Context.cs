using Microsoft.EntityFrameworkCore;
using RazorPageDeneme.Pages.ProductFile;

namespace RazorPageDeneme.Models
{
    public class Context : DbContext //database set gibi komutları kullanabilmek için miras alma işlemi
    {
        
       
        public Context(DbContextOptions<Context> options) : base(options) {}
        //sınıf isimleri ile tablo isimlerinin karışmaması için
        public DbSet<ProductM> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SalesTransaction> SalesTransactions { get; set; }

       
    }
}
