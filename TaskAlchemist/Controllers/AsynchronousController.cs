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
    public class AsynchronousController : AsyncController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "These methods are awaited and the controller is marked with 'AsyncController'. ActionResults & Tasks must be marked with 'async', Tasks must return as Task & callers must 'await' a response.";

            return View();
        }

        public async Task<ActionResult> _Response_PartialView(string type)
        {
            #region Initialization

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ProcessingResult result = new ProcessingResult();

            Debug.WriteLine("About to run process....");
            Debug.WriteLine("Controller is on Thread: " + Thread.CurrentThread.ManagedThreadId);

            #endregion

            result.Description = "Task will still be running in the background. View trace statements in your 'Output' window. You may also fire additional tasks once control returns to the view.";

            switch (type)
            {

                case "basic-parallel":

                    //Simple method:
                    await Methods.ParallelMethods.AsyncVoidWithParallelProcessingTask();  // <--- Task is preferred Option when not handling fire/forget scenarios: http://blogs.msdn.com/b/pfxteam/archive/2009/10/06/9903475.aspx

                    result.Description = "You'll notice this call is EXACTLY the same as the 'Parallel Tasks (Standard)' button in the 'Standard' section, with the exception that the Controller ActionResult is async and this method is awaited.";
                    result.Message = "Basic Async Task Complete!";
                    result.AlertType = "alert-success";
                    result.MethodType = "AsyncVoidWithParallelProcessingTask";

                    

                    break;


                case "void":

                    await Methods.BasicMethods.AsyncVoidProcessingTask();

                    result.Description = "";
                    result.Message = "Async Void Task Complete!";
                    result.AlertType = "alert-success";
                    result.MethodType = "AsyncVoidProcessingTask";

                    break;

                case "string-response":

                    result.Result = await (new Methods.BasicMethods()).AsyncStringResultProcessingTask();

                    result.Description = "Result = '" + result.Result + "'";
                    result.Message = "Await Results Task Complete!";
                    result.AlertType = "alert-success";
                    result.MethodType = "AsyncStringResultProcessingTask";


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


        [AsyncTimeout(20)] //<-- Timeout Length in Milliseconds
        //[NoAsyncTimeout] //<-- No Timeouts for Async Operation
        [HandleError(ExceptionType=typeof(TimeoutException), View="_TimeoutError")] //<-- Handle the Timeout Error
        public async Task<ActionResult> _Response_Timeout(string type, CancellationToken cancellationToken) //<-- Cancellation Token
        {
            #region Initialization

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ProcessingResult result = new ProcessingResult();

            Debug.WriteLine("About to run process....");
            Debug.WriteLine("Controller is on Thread: " + Thread.CurrentThread.ManagedThreadId);

            #endregion

            switch(type)
            {
                case "timeout":

                    // call is awaited, so Timeout Exception will not stop this thread, it will only disconnect the callback:
                    result.Result = await (new Methods.BasicMethods()).AsyncStringResultProcessingTask();

                    result.Description = "Result = '" + result.Result + "'";
                    result.Message = "Timeout Results Task Complete!";
                    result.AlertType = "alert-error";
                    result.MethodType = "Timeout: ";

                    break;

                case "timeout-handle-exception":

                    //Tell cancellation token to throw if exception occurs:
                    cancellationToken.ThrowIfCancellationRequested();

                    try
                    {
                        result.Result = await (new Methods.BasicMethods()).AsyncStringResultProcessingTask();

                        result.Description = "Result = '" + result.Result + "'";
                        result.Message = "Timeout Results Task Complete!";
                        result.AlertType = "alert-error";
                        result.MethodType = "Timeout: ";
                    }
                    catch(Exception e)
                    {

                    }

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