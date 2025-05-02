using Microsoft.AspNetCore.Mvc;
using ProjectLearn.Models;

namespace ProjectLearn.Controllers
{
    public class checkingController : Controller
    {
        private readonly MyDbContext context;

        public checkingController(MyDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

       

      

       
    }

   
}
