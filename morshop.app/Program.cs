using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using morshop.app.EmailServices;
using morshop.app.Identity;
using morshop.business.Abstract;
using morshop.business.Concrete;
using morshop.repository.Abstract;
using morshop.repository.Concrete;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();


//Identity İşlemleri//
    //Üyelik tablolarının oluşturulması

// builder.Services.AddDbContext<ApplicationDbContext>(
//     options => options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));//Identity tablolarının nereye ekleneceğini belittik veritabanı
// builder.Services.AddDbContext<ShopContext>(options=>options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

builder.Services.AddDbContext<ApplicationDbContext>(
    options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection"));
        options.EnableSensitiveDataLogging((true));//Identity tablolarının nereye ekleneceğini belittik veritabanı
    });
        
    
builder.Services.AddDbContext<ShopContext>(options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection"));
        options.EnableSensitiveDataLogging((true));//Identity tablolarının nereye ekleneceğini belittik veritabanı
    });

builder.Services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();//Identity leri tanımladık
    //Identity Ayarları
builder.Services.Configure<IdentityOptions>(options=>{
        //Password
    options.Password.RequireDigit=true;//Parolanun içerisinde sayı olsun mu?
    options.Password.RequireLowercase=true;//Parolanun içerisinde küçük harf olsun mu?
    options.Password.RequireUppercase=true;//Parolanun içerisinde büyük harf olsun mu?
    options.Password.RequiredLength=6;//Parola Minimum kaç karakter olsun?
    options.Password.RequireNonAlphanumeric=true;//Parolanın içerisinde özel karakter olsun mu?

        //Lockout
    options.Lockout.AllowedForNewUsers=true;//Şifreyi yanlış girme sayısını doldurunca kilitlensin mi?
    options.Lockout.MaxFailedAccessAttempts=5;//Şifreyi en fazla kaçkere yanlış girebilsin?
    options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(5);//Kilitlenen hesap ne kadar süre sonra açılsın?

        //User
    //options.User.AllowedUserNameCharacters="";//User adında İstediğini karakterleri yazın?
    options.User.RequireUniqueEmail=true;//user ların e-mail adersleri benzersiz mi olsun?
    options.SignIn.RequireConfirmedEmail=true;//user mailini onaylatması zorunlu olsun mu?
    options.SignIn.RequireConfirmedPhoneNumber=false;//user telefon no onaylatması zorunlu olsun mu?
});
    //Cookie Ayarları
builder.Services.ConfigureExternalCookie(options=>{
    options.LoginPath="/account/login";//Kullanıcı login değilse nereye yonlendirilsin?
    options.LogoutPath="/account/logout";//Kullanıcı çıkış yaptığında nereye yönlendirilsin?
    options.AccessDeniedPath="/account/accessdenied";//Yekilendirme Admin,seller,costumer vb... Yani yetkisi hangi sayfaya yönlendirilsin?
    options.SlidingExpiration=false;//İstek yaptığında cookie süresi tazelensin mi?
    options.ExpireTimeSpan=TimeSpan.FromMinutes(60);//Cookie nekadar süre sonra silinsin? 
    //Güvenlik
    options.Cookie=new CookieBuilder
    {
        HttpOnly=true,//Sadece Http isteklerini kabul et
        Name=".MorShop.Security.Cookie",//Cookie nin Adı
        SameSite=SameSiteMode.Strict //bu özellik Cookie çalındığında başka bir cihazdan kullanılmasını önler
    };
});

builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IProductService,ProductManager>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<ICategoryService,CategoryManager>();
builder.Services.AddScoped<ICardRepository,CardRepository>();
builder.Services.AddScoped<ICardService,CardManager>();
builder.Services.AddScoped<IOrderRepository,OrderRepository>();
builder.Services.AddScoped<IOrderService,OrderManager>();
//Mail Service
builder.Services.AddTransient<IEmailSender,SmtpEmailSender>();

var app = builder.Build();











app.UseStaticFiles();

app.UseAuthentication();//Identity işlemleri devreye alır

app.UseRouting();

app.UseAuthorization();//Yetkilendirmeyi devreye alır

app.UseEndpoints(endpoint=>{
    endpoint.MapControllerRoute(
        name:"List",
        pattern:"product/list/{categoryUrl?}",
        defaults:new {Controller="Product",Action="List"}
    );
    endpoint.MapControllerRoute(
        name:"Details",
        pattern:"product/details/{url?}",
        defaults:new {Controller="Product",Action="Details"}
    );
    endpoint.MapControllerRoute(
        name:"default",
        pattern:"{controller=Home}/{action=Index}/{id?}"
    );
});
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var userManager = services.GetRequiredService<UserManager<AppUser>>();
var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
var configuration = services.GetRequiredService<IConfiguration>();
SeedIdentity.Seed(userManager, roleManager, configuration).Wait();


app.Run();
