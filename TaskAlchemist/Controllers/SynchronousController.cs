using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TaskAlchemist.Models;

namespace TaskAlchemist.Controllers
{
    public class SynchronousController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Message = "These methods are 'Standard' and Non-Awaited. You'll notice that... ";

            return View();
        }

        public ActionResult _Response_PartialView(string type)
        {
            #region Initialization

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ProcessingResult result = new ProcessingResult();

            Debug.WriteLine("About to run process....");
            Debug.WriteLine("Controller is on Thread: " + Thread.CurrentThread.ManagedThreadId);

            #endregion



            switch (type)
            {

                case "basic-parallel":

                    //Simple method:
                    Methods.ParallelMethods.VoidWithParallelProcessing();  // <--- Task is preferred Option when not handling fire/forget scenarios: http://blogs.msdn.com/b/pfxteam/archive/2009/10/06/9903475.aspx

                    result.Description = "You'll notice this call is EXACTLY the same as the 'Parallel Tasks (Await)' button in the 'Await' section, with the exception that the Controller ActionResult is not async and the method is not awaited. ";
                    result.Message = "Basic Task Complete!";
                    result.AlertType = "alert-success";
                    result.MethodType = "VoidWithParallelProcessing";

                    break;


                case "void":

                    Methods.BasicMethods.VoidProcessing();

                    result.Description = "";
                    result.Message = "Standard Void Task Complete!";
                    result.AlertType = "alert-success";
                    result.MethodType = "VoidProcessing";


                    break;

                case "string-response":

                    result.Result = (new Methods.BasicMethods()).StringResultProcessing();

                    result.Description = "Result ='" + result.Result + "'";
                    result.Message = "Standard Results Task Complete!";
                    result.AlertType = "alert-success";
                    result.MethodType = "StringResultProcessing";


                    break;



            }



            #region Response

            Debug.WriteLine("Control is back origin thread.");
            Debug.WriteLine("Controller is on Thread: " + Thread.CurrentThread.ManagedThreadId);

            stopWatch.Stop();
            result.TimeElapsed = stopWatch.ElapsedMilliseconds;


            return PartialView(result);

            #endregion


        }
    }
}