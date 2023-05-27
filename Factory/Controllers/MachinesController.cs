using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers
{
    public class MachinesController : Controller
    {
        private readonly FactoryContext _db;

        public MachinesController(FactoryContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Machine> model = _db.Machines
                                            // .Include(machine => machine) 
                                            .ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            // ViewBag = new SelectList(_db, "", "");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Machine machine)
        {
            if (!ModelState.IsValid)
            {
                // ViewBag = new SelectList(_db, "", "");
                return View();
            }
            else
            {
                _db.Machines.Add(machine);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        public ActionResult Details(int id)
        {
            Machine thisMachine = _db.Machines
                                .Include(machine => machine.EngineerMachines)
                                .FirstOrDefault(machine => machine.MachineId == id);
            return View(thisMachine);
        }






    }
}