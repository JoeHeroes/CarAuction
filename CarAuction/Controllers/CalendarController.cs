using CarAuction.Models;
using CarAuction.Models.DTO;
using CarAuction.Models.View;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace CarAuction.Controllers
{
    public class CalendarController : Controller
    {
        private readonly AuctionDbContext dbContext;

        public CalendarController(AuctionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [Route("Calendar")]
        public IActionResult Calendar()
        {

            int id = int.Parse(HttpContext.Session.GetString("id"));

            var events = this.dbContext.Events.Where(x => x.Owner == 0 || x.Owner == id).ToList();

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
        public IActionResult EventList()
        {
            var events = this.dbContext.Events.ToList();
            return View(events);
        }

        [Route("AddEvent")]
        public IActionResult AddEvent()
        {
            return View();
        }

        [HttpPost]
        [Route("AddEvent")]
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
                    AllDay = true,
                    Owner = 0,
                };


                this.dbContext.Events.Add(newEvent);
                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                return RedirectToAction("Calendar");
            }
            return View("AddEvent");
        }


        [Route("EditEvent")]
        public IActionResult EditEvent(int id)
        {
            var account = this.dbContext.Events.FirstOrDefault(x => x.Id == id);

            return View(new EventDto()
            {
                Id = account.Id,
                Title = account.Title,
                StartDate = DateTime.Parse(account.Start),
                BackgroundColor = account.Color
            });
        }

        [HttpPost]
        [Route("EditEvent")]
        public IActionResult EditEvent(EventDto dto)
        {
            if (ModelState.IsValid)
            {
                if (dto.StartDate.Date < DateTime.Now.Date)
                {
                    ViewBag.msg = "Time has passed";
                    return View("EditEvent");
                }

                var events = this.dbContext.Events.FirstOrDefault(x => x.Id == dto.Id);
                events.Title = dto.Title;
                events.Start = dto.StartDate.ToString("yyyy-MM-dd");
                events.End = dto.StartDate.ToString("yyyy-MM-dd");
                events.Color = dto.BackgroundColor;
                events.AllDay = true;
                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }
            }
            return View("EditEvent");
        }

        public IActionResult DeleteEvent(int id)
        {
            var events = this.dbContext.Events.FirstOrDefault(x => x.Id == id);
            if (events != null)
            {
                this.dbContext.Events.Remove(events);
            }

            try
            {
                this.dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            return RedirectToAction("EventList");
        }
    }
}
