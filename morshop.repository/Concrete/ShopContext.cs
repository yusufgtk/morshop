using Microsoft.EntityFrameworkCore;
using morshop.entity;

namespace morshop.repository.Concrete
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardItem> CardItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        // public DbSet<ProductCategory> Productcategory { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     base.OnConfiguring(optionsBuilder);
        //     optionsBuilder.UseSqlite("Data Source=C:\\Users\\yusuf\\OneDrive\\Masaüstü\\morshop\\morshop.db");
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<ProductCategory>().HasKey(i=>new {i.ProductId,i.CategoryId});
            modelBuilder.Entity<Product>().HasData(
                new Product {Id=1,CategoryId=1,Name="Iphone 14 Pro Max",Description="Iphone 14 Pro Max 1 tb Hafıza",CurrentPrice=68_899,PreviousPrice=70_999,ImageUrl="iphone-14-pro-max.jpeg",Url="iphone-14-pro-max",IsApproved=true,IsHome=false,NumberOfSales=1230},
                new Product {Id=2,CategoryId=1,Name="Iphone 14",Description="Iphone 14 1 tb Hafıza",CurrentPrice=59_899,PreviousPrice=61_999,ImageUrl="iphone-14.jpeg",Url="iphone-14",IsApproved=true,IsHome=false,NumberOfSales=1453},
                new Product {Id=3,CategoryId=1,Name="Iphone 13 Pro Max",Description="Iphone 13 Pro Max 512 gb Hafıza",CurrentPrice=45_999,PreviousPrice=48_999,ImageUrl="iphone-13-pro-max.jpeg",Url="iphone-13-pro-max",IsApproved=true,IsHome=false,NumberOfSales=1100},
                new Product {Id=4,CategoryId=1,Name="Iphone 12",Description="Iphone 12 512 gb Hafıza",CurrentPrice=35_899,PreviousPrice=37_999,ImageUrl="iphone-12.jpeg",Url="iphone-12",IsApproved=true,NumberOfSales=920},
                new Product {Id=5,CategoryId=1,Name="Samsung S23 Ultra",Description="Samsun S20 Ultra 1 tb Hafıza",CurrentPrice=35_999,PreviousPrice=37_999,ImageUrl="samsung-s23-ultra.jpeg",Url="samsung-s23-ultra",IsApproved=true,IsHome=false,NumberOfSales=963},
                new Product {Id=6,CategoryId=2,Name="Ipad Air 5. Nesil",Description="Ipad Air 5. Nesil 1 tb Hafıza",CurrentPrice=30_899,PreviousPrice=32_999,ImageUrl="ipad-air-5-nesil.jpeg",Url="ipad-air-5-nesil",IsApproved=true,IsHome=false,NumberOfSales=571},
                new Product {Id=7,CategoryId=2,Name="Samsung Tab S8 Ultra",Description="Samsung Tab S8 Ultra 1 tb Hafıza",CurrentPrice=30_999,PreviousPrice=28_999,ImageUrl="samsung-tab-s8-ultra.jpeg",Url="samsung-tab-s8-ultra",IsApproved=true,IsHome=false,NumberOfSales=236},
                new Product {Id=8,CategoryId=3,Name="Macbook Pro",Description="Macbook Pro 1 tb Hafıza",CurrentPrice=70_899,PreviousPrice=65_999,ImageUrl="macbook-pro.jpeg",Url="macbook-pro",IsApproved=true,IsHome=false,NumberOfSales=1230},
                new Product {Id=9,CategoryId=3,Name="Monster Abra A5",Description="Monster Abra A5 1 tb Hafıza",CurrentPrice=29_899,PreviousPrice=25_999,ImageUrl="monster-abra-a5.jpeg",Url="monster-abra-a5",IsApproved=true,IsHome=false,NumberOfSales=851},
                new Product {Id=10,CategoryId=3,Name="Asus Rog 17",Description="Asus Rog 17.1 512 gb Hafıza Rtx4080 i7 13750h",CurrentPrice=90_899,PreviousPrice=81_999,ImageUrl="asus-rog-17.jpeg",Url="asus-rog-17",IsApproved=true,IsHome=false,NumberOfSales=1230}
            );

            modelBuilder.Entity<Category>().HasData(
                new Category {Id=1,Name="Telefon", Url="telefon"},
                new Category {Id=2,Name="Tablet", Url="tablet"},
                new Category {Id=3,Name="Bilgisayar", Url="bilgisayar"},
                new Category {Id=4,Name="Televizyon", Url="televizyon"},
                new Category {Id=5,Name="Konsol", Url="konsol"}
            );
            // modelBuilder.Entity<ProductCategory>().HasData(
            //     new ProductCategory {ProductId=1,CategoryId=1},
            //     new ProductCategory {ProductId=1,CategoryId=5},
            //     new ProductCategory {ProductId=2,CategoryId=1},
            //     new ProductCategory {ProductId=2,CategoryId=5},
            //     new ProductCategory {ProductId=3,CategoryId=1},
            //     new ProductCategory {ProductId=3,CategoryId=5},
            //     new ProductCategory {ProductId=4,CategoryId=1},
            //     new ProductCategory {ProductId=4,CategoryId=5},
            //     new ProductCategory {ProductId=5,CategoryId=1},
            //     new ProductCategory {ProductId=5,CategoryId=5},
            //     new ProductCategory {ProductId=6,CategoryId=2},
            //     new ProductCategory {ProductId=6,CategoryId=5},
            //     new ProductCategory {ProductId=7,CategoryId=2},
            //     new ProductCategory {ProductId=7,CategoryId=5},
            //     new ProductCategory {ProductId=8,CategoryId=3},
            //     new ProductCategory {ProductId=8,CategoryId=5},
            //     new ProductCategory {ProductId=9,CategoryId=3},
            //     new ProductCategory {ProductId=9,CategoryId=5},
            //     new ProductCategory {ProductId=10,CategoryId=3},
            //     new ProductCategory {ProductId=10,CategoryId=5}
            // );
        }
    }
}