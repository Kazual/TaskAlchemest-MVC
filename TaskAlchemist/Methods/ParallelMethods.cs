using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TaskAlchemist.Methods
{
    public static class ParallelMethods
    {
        
        public static void VoidWithParallelProcessing()
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<int> delayTimes = new List<int>();
            delayTimes.Add(1000);
            delayTimes.Add(2000);
            delayTimes.Add(3000);
            delayTimes.Add(4000);

            Parallel.ForEach(delayTimes, delay =>
            {
                //Can't await this herre, so using Thread.Sleep instead:
                //Task.Delay(delay);
                Debug.WriteLine("AsyncVoidWithParallelProcessing (delay: {0}), started on thread: {1}", delay, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(delay);
                Debug.WriteLine("AsyncVoidWithParallelProcessing (delay: {0}), completed on thread: {1}", delay, Thread.CurrentThread.ManagedThreadId);
            });

            sw.Stop();

            Debug.WriteLine("AsyncVoidWithParallelProcessing completed in: {0} milliseconds", sw.ElapsedMilliseconds);
        }

        public static async Task AsyncVoidWithParallelProcessingTask()
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<int> delayTimes = new List<int>();
            delayTimes.Add(1000);
            delayTimes.Add(2000);
            delayTimes.Add(3000);
            delayTimes.Add(4000);


            Parallel.ForEach(delayTimes, delay =>
            {

                //Can't await this herre, so using Thread.Sleep instead:
                //Task.Delay(delay);
                Debug.WriteLine("AsyncVoidWithParallelProcessing (delay: {0}), started on thread: {1}", delay, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(delay);
                Debug.WriteLine("AsyncVoidWithParallelProcessing (delay: {0}), completed on thread: {1}", delay, Thread.CurrentThread.ManagedThreadId);
            });

            sw.Stop();

            Debug.WriteLine("AsyncVoidWithParallelProcessing completed in: {0} milliseconds", sw.ElapsedMilliseconds);
        }
    }
}