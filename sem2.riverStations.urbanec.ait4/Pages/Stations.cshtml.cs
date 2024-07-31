using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sem2.riverStations.urbanec.ait4.StationInfomrations;

namespace sem2.riverStations.urbanec.ait4.Pages
{
    public class StationsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public StationsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Station> Stations { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            Stations = await _context.Stations.ToListAsync();

            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the login page if the user is not authenticated
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            return Page();
        }
    }
}
