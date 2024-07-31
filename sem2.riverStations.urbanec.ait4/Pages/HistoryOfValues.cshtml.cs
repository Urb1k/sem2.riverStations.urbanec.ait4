using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sem2.riverStations.urbanec.ait4.StationInfomrations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public int StartIndex { get; set; }
        public int BatchSize { get; set; }
        public int TotalRecords { get; set; } // Add this property

        public async Task<IActionResult> OnGetAsync(int startIndex = 0, int batchSize = 20)
        {
            StartIndex = startIndex;
            BatchSize = batchSize;

            HistoryValuesList = await _context.HistoryValues
                                     .Include(hv => hv.Station) // Include related Stations entity
                                     .OrderByDescending(hv => hv.Timestamp)
                                     .Skip(startIndex)
                                     .Take(batchSize)
                                     .ToListAsync();

            TotalRecords = await _context.HistoryValues.CountAsync(); // Count total records

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