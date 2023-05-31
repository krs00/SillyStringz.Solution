using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SillyStringz.Controllers 
{
    public class HomeController : Controller
    {
        private readonly FactoryContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager, FactoryContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        [HttpGet("/")]
        public ActionResult Index()
        {
            Engineer[] engineers = _db.Engineers.ToArray();
            Machine[] machines = _db.Machines.ToArray();
            
            Dictionary<string,object[]> model = new Dictionary<string, object[]>();
            model.Add("engineers", engineers);
            model.Add("machines", machines);

            return View(model);
        }

    }
}