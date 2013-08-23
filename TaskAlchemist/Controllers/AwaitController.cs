using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TaskAlchemist.Models;

namespace TaskAlchemist.Controllers
{
    public class AwaitController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "These methods are awaited and will run on a seperate thread. View will not be returned unting all tasks are complete. ActionResults & Tasks must be marked with 'async', Tasks must return as Task & callers must 'await' a response";

            return View();
        }

        public async Task<ActionResult> _Response_PartialView(string type)
        {
            #region Initialization

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ProcessingResult result = new ProcessingResult();

            Debug.WriteLine("About to run process....");

            #endregion

            result.description = "Task will still be running in the background. View trace statements in your 'Output' window. You may also fire additional tasks once control returns to the view.";

            switch (type)
            {

                case "basic":

                    //Simple method:
                    await Methods.ParallelMethods.AsyncVoidWithParallelProcessingTask();  // <--- Task is preferred Option when not handling fire/forget scenarios: http://blogs.msdn.com/b/pfxteam/archive/2009/10/06/9903475.aspx


                    break;


            }



            #region Response

            Debug.WriteLine("Control is back origin thread.");

            stopWatch.Stop();
            result.timeElapsed = stopWatch.ElapsedMilliseconds;
            result.methodType = "Thread.Sleep()";

            return PartialView(result);

            #endregion 


        }
    }
}