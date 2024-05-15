using Bussiness.Abstracts;
using Bussiness.Exceptions;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mamba.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        IOurTeamService _teamService;

        public TeamController(IOurTeamService teamService)
        {
            _teamService = teamService;
        }

        public IActionResult Index()
        {
            var teams = _teamService.GetAll();
            return View(teams);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(OurTeam ourTeam)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _teamService.Create(ourTeam);
            }
            catch (NullException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return RedirectToAction("Index");

        }
        public IActionResult Delete(int id)
        {
            _teamService.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var updateteam = _teamService.Get(x => x.Id == id);
            return View(updateteam);
        }
        [HttpPost]
        public IActionResult Update(OurTeam team)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _teamService.Update(team.Id, team);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(team);
        }
    }
}
