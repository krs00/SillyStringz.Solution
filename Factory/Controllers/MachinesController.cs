using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Factory.Controllers
{
  [Authorize]
  public class MachinesController : Controller
  {
    private readonly FactoryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public MachinesController(UserManager<ApplicationUser> userManager, FactoryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {

			string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
			List<Machine> userMachines = _db.Machines
																							.Where(entry => entry.User.Id == currentUser.Id)
																							.ToList();
			// old code without user association

      // List<Machine> model = _db.Machines 
      //                                 .ToList();
      return View(userMachines); 
    }

    public ActionResult Create()
    {
      // ViewBag = new SelectList(_db, "", "");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Machine machine)
    {
      if (!ModelState.IsValid)
      {
        // ViewBag = new SelectList(_db, "", "");
        return View();
      }
      else
      {

				string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

				ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);

				machine.User = currentUser;

        _db.Machines.Add(machine);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }


    public ActionResult Details(int id)
    {
      Machine thisMachine = _db.Machines
                          .Include(machine => machine.EngineerMachines)
                          .ThenInclude(join => join.Engineer)
                          .FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    public ActionResult Edit(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult Edit(Machine machine)
    {
      _db.Machines.Update(machine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      _db.Machines.Remove(thisMachine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult AddEngineer(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineId == id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult AddEngineer(Machine machine, int engineerId)
    {
#nullable enable
      EngineerMachine? joinEntity = _db.EngineerMachines.FirstOrDefault(join => (join.EngineerId == engineerId && join.MachineId == machine.MachineId));
#nullable disable
      if (joinEntity == null && engineerId != 0)
      {
        _db.EngineerMachines.Add(new EngineerMachine() { EngineerId = engineerId, MachineId = machine.MachineId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = machine.MachineId });
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      EngineerMachine joinEntry = _db.EngineerMachines.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
      _db.EngineerMachines.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}