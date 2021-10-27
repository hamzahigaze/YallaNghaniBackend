using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YallaNghani.Controllers
{
    public class HomeController : Controller
    {
        [Route("support")]
        [HttpGet()]
        public IActionResult Support()
        {
            return View();
        }

        [Route("privacy")]
        [HttpGet()]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
