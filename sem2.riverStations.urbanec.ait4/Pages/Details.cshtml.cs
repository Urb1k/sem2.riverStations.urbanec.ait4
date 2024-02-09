﻿using System;
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

        public Stations Stations { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stations = await _context.Stations.FirstOrDefaultAsync(m => m.ID == id);
            if (stations == null)
            {
                return NotFound();
            }
            else
            {
                Stations = stations;
            }
            return Page();
        }
    }
}
