using CarAuction.Models;
using CarAuction.Models.DTO;
using CarAuction.Models.View;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.Controllers
{
    public class CalendarController : Controller
    {
        private readonly AuctionDbContext dbContext;
        private readonly IPasswordHasher<User> passwordHasherUser;
        private readonly AuthenticationSettings authenticationSetting;

        public CalendarController(AuctionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public IActionResult AddCalendar()
        {
            return View();
        }

        public IActionResult AddEvent(EventDto dto)
        {
            if (ModelState.IsValid)
            {
                var newEvent = new Event()
                {
                    Title = dto.Title,
                    Start = dto.StartDate.ToString("yyyy-MM-dd"),
                    End = dto.StartDate.ToString("yyyy-MM-dd"),
                    Color = dto.BackgroundColor,
                    AllDay= true,
                };

                
                this.dbContext.Events.Add(newEvent);
                this.dbContext.SaveChanges();

                return RedirectToAction("Calendar");
            }
            return View("AddEvent");
        }


        [Route("Calendar")]
        public IActionResult Calendar()
        {
            var events = this.dbContext.Events.ToList();

            List<EventViewModel> listEvent = new();

            int i = 1;

            foreach (var eve in events)
            {
                listEvent.Add(new EventViewModel 
                { 
                    Id = i,
                    Title = eve.Title, 
                    StartDate = eve.Start,
                    EndDate = eve.End,
                    BackgroundColor = eve.Color, 
                    AllDay = eve.AllDay
                });
                i++;
            }

            ViewData["events"] = listEvent;

            return View();


        }
    }
}
