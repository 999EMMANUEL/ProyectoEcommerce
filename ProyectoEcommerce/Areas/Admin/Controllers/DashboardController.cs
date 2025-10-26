using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoEcommerce.Areas.Admin.ViewModels;

namespace ProyectoEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            // Datos de ejemplo; luego puedes inyectar servicios y llenar el VM con datos reales
            var vm = new AdminDashboardVM
            {
                TotalUsuarios = 123,
                PedidosHoy = 7,
                VentasHoy = 254_500.75m,
                ProductosActivos = 86
            };
            return View(vm);
        }
    }
}
