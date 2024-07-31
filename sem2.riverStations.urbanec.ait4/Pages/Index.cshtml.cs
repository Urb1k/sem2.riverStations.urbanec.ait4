using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sem2.riverStations.urbanec.ait4;
using sem2.riverStations.urbanec.ait4.Services;
using sem2.riverStations.urbanec.ait4.StationInfomrations;

namespace sem2.riverStations.urbanec.ait4.Pages.Shared
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private EmailSender _sender;

        public IndexModel(ApplicationDbContext context, EmailSender sender)
        {
            _context = context;
            _sender = sender;
        }

        public IList<StationViewModel> Stations { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var stations = await _context.Stations.ToListAsync();

            Stations = new List<StationViewModel>();

            foreach (var station in stations)
            {
                var latestReadings = await _context.HistoryValues
                    .Where(h => h.StationID == station.ID)
                    .OrderByDescending(h => h.Timestamp)
                    .Take(5)
                    .ToListAsync();

                double meanValue = latestReadings.Any() ? latestReadings.Average(h => h.Value) : 0;
               
                    Stations.Add(new StationViewModel
                {
                    StationName = station.River,
                    MeanValue = meanValue,
                    FloodWarningValue = station.FloodWarningValue,
                    DroughtWarningValue = station.DroughtWarningValue
                });
            }

            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the login page if the user is not authenticated
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            return Page();
        }

        public class StationViewModel
        {
            public string StationName { get; set; }
            public double MeanValue { get; set; }
            public int FloodWarningValue { get; set; }
            public int DroughtWarningValue { get; set; }
        }
    }
}
