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
    public class FireForgetController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "These methods are 'Fire & Forget'. View/UI control will be returned immediatly while tasks run on seperate thread(s) in the background. ActionResults do not have to be marked with 'async' and method calls do not have to be 'await'(ed).";

            return View();
        }

        public ActionResult _Response_PartialView(string threadType)
        {

            #region Initialization

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ProcessingResult result = new ProcessingResult();

            Debug.WriteLine("About to run process....");
            Debug.WriteLine("Controller is on Thread: " + Thread.CurrentThread.ManagedThreadId);

            #endregion

            result.Description = "Task will still be running in the background. View trace statements in your 'Output' window. You may also fire additional tasks once control returns to the view.";

            switch (threadType)
            {

                case "basic-thread":

                    //BEST method for ASP.NET:
                    //Fire & Forget on a background thread
                    System.Threading.ThreadStart threadStart = delegate
                    {
                        Methods.ParallelMethods.VoidWithParallelProcessing();

                    };
                    System.Threading.Thread thread = new System.Threading.Thread(threadStart);
                    thread.IsBackground = true;
                    thread.Start(); //<-- Preferred option fpr Fire/Forget when considering not disturbing the ThreadPool in ASP.NET.


                    //Simple method:
                    //Also, don't forget to use EXTENSIVE ERROR HANDLING in your routine because any unhandled exceptions outside of a debugger will abruptly crash your application:
                    //ThreadPool.QueueUserWorkItem(o => Methods.ParallelMethods.VoidWithParallelProcessing()); //<-- Preferred option fpr Fire/Forget when considering performance (but not for ASP.NET).

                    //Alternate option:
                    //Task.Run(() => Methods.ParallelMethods.AsyncVoidWithParallelProcessing());  // <--- Task is preferred Option when not handling fire/forget scenarios: http://blogs.msdn.com/b/pfxteam/archive/2009/10/06/9903475.aspx

                    result.Message = "Parallel Call Complete!";
                    result.AlertType = "alert-success";;

                    break;



                case "detailed-thread":

                    //w/ Thread Details
                    //By using this method over ThreadPool.QueueUserWorkItem you can name your new thread to make it easier for debugging.
                    //Also, don't forget to use EXTENSIVE ERROR HANDLING in your routine because any unhandled exceptions outside of a debugger will abruptly crash your application:
                    (new Thread(() =>
                    {
                        Methods.ParallelMethods.VoidWithParallelProcessing();
                    })

                    {
                        Name = "Long Running Work Thread (AsyncVoidWithParallelProcessing)",
                        Priority = ThreadPriority.AboveNormal
                    }).Start();

                    result.Message = "Parallel Call Complete!";
                    result.AlertType = "alert-success";

                    break;


                case "exception":

                    throw new System.Exception("Exception message");

                    break;

                case "exception-catch":

                    try
                    {
                        
                        throw new System.Exception("Exception message");
                    }
                    catch(Exception e)
                    {
                        result.Message = "Exception Caught!";
                        result.AlertType = "alert-error";
                    }

                    break;

            }
            



            #region Response

            Debug.WriteLine("Control is back origin thread.");
            Debug.WriteLine("Controller is on Thread: " + Thread.CurrentThread.ManagedThreadId);

            stopWatch.Stop();
            result.TimeElapsed = stopWatch.ElapsedMilliseconds;
            result.MethodType = "Thread.Sleep()";

            return PartialView(result);

            #endregion 
        }



    }
}