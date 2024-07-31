using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sem2.riverStations.urbanec.ait4;
using sem2.riverStations.urbanec.ait4.StationInfomrations;

namespace sem2.riverStations.urbanec.ait4.Pages.Shared
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Station Stations { get; set; } = default!;
        public List<HistoryValues> HistoryValues { get; set; } = new List<HistoryValues>();

        public async Task<IActionResult> OnGetAsync(int? id, int startIndex = 0, int batchSize = 10)
        {
            if (id == null)
            {
                return NotFound();
            }

            Stations = await _context.Stations.FirstOrDefaultAsync(m => m.ID == id);

            if (Stations == null)
            {
                return NotFound();
            }

            // Fetch history values associated with this station based on startIndex and batchSize
            HistoryValues = await _context.HistoryValues
                .Where(h => h.StationID == id)
                .OrderByDescending(h => h.Timestamp)
                .Skip(startIndex)
                .Take(batchSize)
                .ToListAsync();

            return Page();
        }

    }
}
