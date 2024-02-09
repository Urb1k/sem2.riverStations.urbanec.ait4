using Microsoft.AspNetCore.Mvc;
using sem2.riverStations.urbanec.ait4.StationInfomrations;

namespace sem2.riverStations.urbanec.ait4.Controllers
{
    [ApiController]
    [Route("api")]
    public class StationController : Controller
    {
        private ApplicationDbContext _context { get; set; }

        public StationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-stations")]
        public IActionResult GetListOfStations()
        {
            var list = _context.Stations.ToList();
            return StatusCode(200,new JsonResult(list));
        }

        

        [HttpPost] // Change to HttpPost
        [Route("add-stations")]
        public IActionResult AddStation([FromBody] Stations stations) // Add FromBody attribute
        {
            if (!ModelState.IsValid)
            {
                // Log ModelState errors (if needed)
                return BadRequest(ModelState);
            }
            
            stations.CreatedOn = DateTime.Now;
            _context.Stations.Add(stations);
            _context.SaveChanges();
            
             
            return StatusCode(200, "JsonData added");
        }

    }
}
