using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sem2.riverStations.urbanec.ait4.StationInfomrations;

namespace sem2.riverStations.urbanec.ait4.Pages
{
    [Authorize]
    public class HistoryOfValuesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public HistoryOfValuesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<HistoryValues> HistoryValuesList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            HistoryValuesList = await _context.HistoryValues
                                     .Include(hv => hv.Stations) // Include related Stations entity
                                     .ToListAsync();
            // Check if the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the login page if the user is not authenticated
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            return Page();
        }
    }
}
