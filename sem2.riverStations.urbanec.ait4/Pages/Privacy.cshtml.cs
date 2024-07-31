using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sem2.riverStations.urbanec.ait4.Services;

namespace sem2.riverStations.urbanec.ait4.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
            Hangfire.BackgroundJob.Enqueue<EmailSender>(x => x.SendEmailAsync("vojtisek.urbanec@gmail.com", "body", "body"));
        }
    }

}
