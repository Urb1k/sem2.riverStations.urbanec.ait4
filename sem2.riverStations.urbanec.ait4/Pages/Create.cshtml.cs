using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sem2.riverStations.urbanec.ait4;
using sem2.riverStations.urbanec.ait4.StationInfomrations;

namespace sem2.riverStations.urbanec.ait4.Pages.Shared
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Station Stations { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string userName = User.Identity.Name;

            Stations.CreatedByUser = userName;
            Stations.CreatedOn = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Retrieve the username of the currently logged-in user
            

            _context.Stations.Add(Stations);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
