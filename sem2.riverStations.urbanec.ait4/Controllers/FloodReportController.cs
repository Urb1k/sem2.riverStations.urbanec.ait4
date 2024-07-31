using Hangfire;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sem2.riverStations.urbanec.ait4.Filters;
using sem2.riverStations.urbanec.ait4.Services;
using sem2.riverStations.urbanec.ait4.StationInfomrations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace sem2.riverStations.urbanec.ait4.Controllers
{
    [ApiController]
    [Route("api")]
    public class FloodReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailSender _emailSender;

        public FloodReportController(ApplicationDbContext context, EmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        [TokenAuthorizationFilter]
        [HttpPost]
        [Route("get-floodlevel")]
        public async Task<IActionResult> GetFloodReports([FromBody] HistoryValues values)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if StationID exists
            var stationExists = await _context.Stations.AnyAsync(s => s.ID == values.StationID);

            if (!stationExists)
            {
                // Set a custom header and return a detailed response
                return BadRequest(new { message = $"StationID {values.StationID} does not exist. Please add the station first and then resend the API request." });
            }

            try
            {
                values.Timestamp = DateTime.Now;
                _context.HistoryValues.Add(values);
                await _context.SaveChangesAsync();

                // Enqueue a background job to check the latest readings and send alerts
                BackgroundJob.Enqueue(() => CheckAndSendAlert(values.StationID));

                // Send a success response
                return Ok("Data saved successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // Return an appropriate error response
                return StatusCode(500, new { message = "An error occurred while saving data", details = ex.Message });
            }
        }

        // This method will be executed as a Hangfire background job
        public async Task CheckAndSendAlert(int stationId)
        {
            var latestReadings = await _context.HistoryValues
                .Where(hv => hv.StationID == stationId)
                .OrderByDescending(hv => hv.Timestamp)
                .Take(6)
                .ToListAsync();

            var station = await _context.Stations.FindAsync(stationId);

            if (station != null)
            {
                if (latestReadings.Count > 5) { 
                    double meanValue = latestReadings.Any() ? latestReadings.Average(hv => hv.Value) : 0;

                    string recipient = "vojtisek.urbanec@gmail.com";
                    if (meanValue > station.FloodWarningValue)
                    {
                        await _emailSender.SendCriticalAlertEmail(recipient,
                            "WARNING FLOOD LEVEL",
                            "On river "+ station.River +" the flood level is too high. Go check immediately.");
                    }
                    else if (meanValue < station.DroughtWarningValue)
                    {
                        await _emailSender.SendCriticalAlertEmail(recipient,
                            "WARNING DROUGHT LEVEL",
                            "On river "+ station.River +" the drought level is too low. Go check immediately.");
                    }
                }
            }
        }
    }
}
