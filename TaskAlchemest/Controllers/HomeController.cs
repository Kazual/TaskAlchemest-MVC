using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskAlchemest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Debug.WriteLine("Task Alchemest has started.....");

            return View();
        }

        public ActionResult Await()
        {

            ViewBag.Message = "These methods are awaited. View will not be returned unting all tasks are complete.";

            return View();
        }


        public ActionResult Parallel()
        {
            ViewBag.Message = "These methods are run in parallel. Processing will complete faster. View will not be returned unting all tasks are complete.";

            return View();
        }


    }
}