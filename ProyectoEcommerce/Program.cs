using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoEcommerce.Data;

var builder = WebApplication.CreateBuilder(args);

// DbContext - DEBE especificar <ProyectoEcommerceContext>
builder.Services.AddDbContext<ProyectoEcommerceContext>(opciones =>
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity - DEBE especificar <IdentityUser>
builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
    .AddRoles<IdentityRole>() // DEBE especificar <IdentityRole>
    .AddEntityFrameworkStores<ProyectoEcommerceContext>(); // DEBE especificar <ProyectoEcommerceContext>

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // Para las páginas de Identity

var app = builder.Build();

// Inicializar roles y usuario admin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // IMPORTANTE: antes de UseAuthorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Para las páginas de Identity

app.Run();