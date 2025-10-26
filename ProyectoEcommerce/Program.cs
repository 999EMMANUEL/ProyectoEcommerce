using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoEcommerce.Data;

//VERSION CON OPCIONES DE ADMINISTRADOR
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProyectoEcommerceContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddIdentity<IdentityUser, IdentityRole>(o =>
    {
        o.SignIn.RequireConfirmedAccount = false;
        o.Password.RequireDigit = false;
        o.Password.RequireUppercase = false;
        o.Password.RequireNonAlphanumeric = false;
        o.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<ProyectoEcommerceContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();              // usa el paquete Identity.UI

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var sp = scope.ServiceProvider;
    var db = sp.GetRequiredService<ProyectoEcommerceContext>();
    db.Database.Migrate();   // aplica migraciones pendientes

    var roles = sp.GetRequiredService<RoleManager<IdentityRole>>();
    var users = sp.GetRequiredService<UserManager<IdentityUser>>();

    const string ADMIN = "Admin";
    if (!await roles.RoleExistsAsync(ADMIN))
        await roles.CreateAsync(new IdentityRole(ADMIN));

    var email = "admin@demo.com"; var pass = "Admin123!"; //para login administrador -  seccion de preguntas frecuente
    var admin = await users.FindByEmailAsync(email);
    if (admin == null)
    {
        admin = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
        if ((await users.CreateAsync(admin, pass)).Succeeded)
            await users.AddToRoleAsync(admin, ADMIN);
    }
}

if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/Home/Error"); app.UseHsts(); }

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();   //  habilita login
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");



app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();       // expone /Identity/Account/Login
app.Run();
