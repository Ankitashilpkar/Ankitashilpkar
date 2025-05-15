using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectLearn.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectLearn.Controllers
{

    public class HomeController : Controller
    {
        private readonly MyDbContext context;

        public HomeController(MyDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult SeatCheck(string FromPlace, string ToPlace)
        {
            var data = context.FlightRecords
                .Where(f => f.FromPlace == FromPlace && f.ToPlace == ToPlace && f.FlightSeat == "free")
                .ToList();

            return View(data);
        }

        [HttpGet]
        public IActionResult FlightBook(int flightId)
        {
            var flight = context.FlightRecords.FirstOrDefault(f => f.FlightId == flightId);
            if(flight == null && flight.FlightSeat !="Free")
            {
                ViewBag.Message = "Seat is not available.";
                return View("FlightBook", null);
            }
            ViewBag.FlightId = flightId;
            return View();
        }

        [HttpPost]
        public IActionResult FlightBook(int flightId , string name,string aadhaar)
        {
            var flight = context.FlightRecords.FirstOrDefault(f => f.FlightId == flightId);

            if(flight !=null && flight.FlightSeat == "Free")
            {
                var flightbook = new FlightBook
                {
                    PassengerName = name,
                    PassengerAadhaar = aadhaar
                };
                context.FlightBooks.Add(flightbook);

                flight.FlightSeat = "booked";
                context.SaveChanges();

                ViewBag.Message = "Ticket booked successfully";
                return View("flightbook",flight);
            }
            ViewBag.Message = "Seat is not available";
            return View("flightbook", null);
        }







        //[HttpGet]
        //public IActionResult FlightBook(int flightId)
        //{
        //    var flight = context.FlightRecords.FirstOrDefault(f => f.FlightId == flightId);
        //    if (flight == null)
        //    {
        //        return View(flight);
        //    }
        //    return NotFound();
        //}

        //[HttpPost]
        //public IActionResult ConfirmBooking(int flightId)
        //{
        //    var flight = context.FlightRecords.FirstOrDefault(f => f.FlightId == flightId);
        //    if(flight != null && flight.FlightSeat == "Free")
        //    {
        //        flight.FlightSeat = "booked";
        //        context.SaveChanges();
        //        ViewBag.Message = "Ticket book successfully";
        //        return View("FlightBook",flight);
        //    }
        //    ViewBag.Message = "Seat is not available";
        //    return View("FlightBook", null);

        //}

        //[HttpPost]
        //public async Task<IActionResult> Booking(FlightBook book)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        await context.FlightBooks.AddAsync(book);
        //        await context.SaveChangesAsync();

        //        TempData["Message"] = "Insert SuccessFull..";
        //        return RedirectToAction("ConfirmBooking");

        //        //var data = await context.FlightRecords.FindAsync(book.FlightId);
        //        //if(data != null && data.FlightSeat == "Free")
        //        //{
        //        //    data.FlightSeat = "booked";
        //        //    await context.SaveChangesAsync();
        //        //    return RedirectToAction("ConfirmBooking");
        //        ///}
        //        //return NotFound("Flight not found");
        //    }
        //    return View(book);
        //}



        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public async Task<IActionResult> FlightBook(int flightId)
        //{
        //    var flight = context.FlightRecords.FirstOrDefault(f => f.FlightId == flightId);
        //    if (flight != null && flight.FlightSeat == "Free")
        //    {
        //        ViewBag.FlightId = flight.FlightId;
        //        ViewBag.FlightSeat = flight.FlightSeat;
        //        ViewBag.FlightNo = flight.FlightNumber;
        //        ViewBag.FlightName = flight.FlightName;
        //        ViewBag.Price = flight.Price;
        //        ViewBag.FromPlace = flight.FromPlace;
        //        ViewBag.ToPlace = flight.ToPlace;
        //        ViewBag.DepartureTime = flight.DepartureTime;
        //        ViewBag.ArrivalTime = flight.ArrivalTime;
        //        ViewBag.Class = flight.Class;
        //        ViewBag.SeatType = flight.SeatType;
        //        ViewBag.FreeBaggage = flight.FreeBaggage;
        //        ViewBag.FreeMeal = flight.FreeMeal;

        //        return View();
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Seat is not Available.";
        //        return View();
        //    }

        //}


        [HttpPost]
        public IActionResult CancelTicket(int flightId)

        {
            var flight = context.FlightRecords.FirstOrDefault(f => f.FlightId == flightId);
            if (flight != null && flight.FlightSeat == "booked")
            {
                flight.FlightSeat = "Free";
                context.SaveChanges();

                ViewBag.Message = "Booking cancelled successfully";

            }
            else
            {
                ViewBag.Message = "No booking found to cancel";

            }
            return RedirectToAction("BookTickets");

        }

        public IActionResult BookTickets()
        {
            var bt = context.FlightRecords
                .Where(f => f.FlightSeat == "booked")
                .ToList();
            return View(bt);
        }
        public IActionResult Index()
        { 

            List<string> fromLocations = new List<string>();
            fromLocations = context.FlightRecords
                .Select(f => f.FromPlace)
                .Distinct()
                .ToList();

            ViewBag.FromLocations = fromLocations;



            List<string> toLocations = new List<string>();
            toLocations = context.FlightRecords
                .Select(f => f.ToPlace)
                .Distinct()
                .ToList();

            ViewBag.toLocations = toLocations;

            return View();
        }


        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("ClientSession") != null)
            {
               return RedirectToAction("Dashboard");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(ClientTbl client)
        {
            var myClient=context.ClientTbls.Where(x => x.Email== client.Email && x.Password == client.Password).FirstOrDefault();
            if (myClient != null)
            {
                HttpContext.Session.SetString("ClientSession", "Login SuccessFully..");// myClient.Email
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Message = "Login Faild...";
            }
                return View();
        }


        public async Task<IActionResult> Dashboard()
        {
            //FlightRecord abc= new FlightRecord();
            // abc = await context.FlightRecords.ToListAsync();
            var data = await context.FlightRecords.ToListAsync();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(FlightRecord fr)
        {
            if(ModelState.IsValid)
            {
               await context.FlightRecords.AddAsync(fr);
               await context.SaveChangesAsync();

                TempData["Message"] = "Insert SuccessFull..";
                //RedirectToAction("Home","Dashboard");
                return RedirectToAction(nameof(Dashboard));
            }
            return View(fr);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null || context.FlightRecords == null)
            {
                return NotFound();
            }
            var data = await context.FlightRecords.FirstOrDefaultAsync(x => x.FlightId==id);
            if(data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || context.FlightRecords == null)
            {
                return NotFound();
            }
            var data = await context.FlightRecords.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id,FlightRecord ft)
        {
            if(id !=ft.FlightId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                context.FlightRecords.Update(ft);
                await context.SaveChangesAsync();
                //RedirectToAction("Home","Dashboard");
                return RedirectToAction(nameof(Dashboard));
            }
            return View(ft);
         
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.FlightRecords == null)
            {
                return NotFound();
            }
            var data = await context.FlightRecords.FirstOrDefaultAsync(x => x.FlightId == id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var data = await context.FlightRecords.FindAsync(id);
            if(data != null)
            {
                context.FlightRecords.Remove(data);
            }
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Dashboard));
            
        }




        //if (HttpContext.Session.GetString("ClientSession")!=null)
        //{
        //    ViewBag.MySession = HttpContext.Session.GetString("ClientSession").ToString();
        //}
        //else
        //{
        //    return RedirectToAction("Login");
        //}

        public IActionResult LogOut()
        {
            if (HttpContext.Session.GetString("ClientSession") != null)
            {
                HttpContext.Session.Remove("ClientSession");
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult Register()
        { 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(ClientTbl user)
        {
            if(ModelState.IsValid)
            {
                await context.ClientTbls.AddAsync(user);
                await context.SaveChangesAsync();
                TempData["Success"] = "Registered Successfully..";
                return RedirectToAction("Login");
               
            }
            return View(user);
        }
     
       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
