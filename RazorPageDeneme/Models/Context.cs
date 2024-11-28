//using Microsoft.EntityFrameworkCore;
//using RazorPageDeneme.Pages.ProductFile;

//namespace RazorPageDeneme.Models
//{
//    public class Context : DbContext //database set gibi komutları kullanabilmek için miras alma işlemi
//    {


//        public Context(DbContextOptions<Context> options) : base(options) {}
//        //sınıf isimleri ile tablo isimlerinin karışmaması için
//        public DbSet<ProductM> Products { get; set; }
//        public DbSet<CustomerM> Customers { get; set; }
//        public DbSet<SalesTransactionM> SalesTransactions { get; set; }


//    }
//}

using Microsoft.EntityFrameworkCore;
using RazorPageDeneme.Models;
using RazorPageDeneme.Pages.ProductFile;
using System.IO.Pipelines;

namespace RazorPageDeneme.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<ProductM> Products { get; set; }
        public DbSet<CustomerM> Customers { get; set; }
        public DbSet<SalesTransactionM> SalesTransactions { get; set; }

    }
}
