
using Bussiness.Abstracts;
using Data.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Mamba.Controllers
{
    public class HomeController : Controller
    {

        IOurTeamService _teamService;

        public HomeController(IOurTeamService teamService)
        {
            _teamService = teamService;
        }

        public IActionResult Index()
        {
            var teams = _teamService.GetAll();
            return View(teams);
        }

   
    }
}
