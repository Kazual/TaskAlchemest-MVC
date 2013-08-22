using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TaskAlchemest.Models;

namespace TaskAlchemest.Controllers
{
    public class FireForgetController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "These methods are 'Fire & Forget'. View will be returned immediatly while tasks run on seperate threads in the background.";

            return View();
        }

        public ActionResult _Parallel_PartialView(string threadType)
        {

            #region Initialization

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ProcessingResult result = new ProcessingResult();

            Debug.WriteLine("About to run process....");

            #endregion

            result.description = "Task will still be running in the background. View trace statements in your 'Output' window. You may also fire additional tasks once control returns to the view.";

            switch (threadType)
            {

                case "simple-thread":

                    //Simple method:
                    //Also, don't forget to use EXTENSIVE ERROR HANDLING in your routine because any unhandled exceptions outside of a debugger will abruptly crash your application:
                    Task.Run(() => Methods.ParallelMethods.AsyncVoidWithParallelProcessing());  // <--- Preferred Optiob

                    //Alternate option:
                    //ThreadPool.QueueUserWorkItem(o => Methods.ParallelMethods.AsyncVoidWithParallelProcessing());
                    

                    break;



                case "detailed-thread":

                    //w/ Thread Details
                    //By using this method over ThreadPool.QueueUserWorkItem you can name your new thread to make it easier for debugging.
                    //Also, don't forget to use EXTENSIVE ERROR HANDLING in your routine because any unhandled exceptions outside of a debugger will abruptly crash your application:
                    (new Thread(() =>
                    {
                        Methods.ParallelMethods.AsyncVoidWithParallelProcessing();
                    })

                    {
                        Name = "Long Running Work Thread (AsyncVoidWithParallelProcessing)",
                        Priority = ThreadPriority.AboveNormal
                    }).Start();

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