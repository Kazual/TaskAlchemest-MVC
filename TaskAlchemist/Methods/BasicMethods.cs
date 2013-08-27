using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TaskAlchemist.Methods
{
    public class BasicMethods
    {
        
        public static void VoidProcessing()
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<int> delayTimes = new List<int>();
            delayTimes.Add(1000);
            delayTimes.Add(2000);
            delayTimes.Add(3000);
            delayTimes.Add(4000);


            foreach(int d in delayTimes)
            {
                Debug.WriteLine("Basic: VoidProcessing {0}, started on thread: {1}", d, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(d);
                Debug.WriteLine("Basic: VoidProcessing {0}, completed on thread: {1}", d, Thread.CurrentThread.ManagedThreadId);
            }



            sw.Stop();

            Debug.WriteLine("Basic: VoidProcessing completed in: {0} milliseconds", sw.ElapsedMilliseconds);
        }


        public static async Task AsyncVoidProcessingTask()
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<int> delayTimes = new List<int>();
            delayTimes.Add(1000);
            delayTimes.Add(2000);
            delayTimes.Add(3000);
            delayTimes.Add(4000);

            foreach (int d in delayTimes)
            {
                Debug.WriteLine("Basic: AsyncVoidProcessingTask {0}, started on thread: {1}", d, Thread.CurrentThread.ManagedThreadId);
                //Thread.Sleep(d);
                await Task.Delay(d);
                Debug.WriteLine("Basic: AsyncVoidProcessingTask {0}, completed on thread: {1}", d, Thread.CurrentThread.ManagedThreadId);
            }

            sw.Stop();

            Debug.WriteLine("Basic: AsyncVoidProcessingTask completed in: {0} milliseconds", sw.ElapsedMilliseconds);
        }




        public string StringResultProcessing()
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<int> delayTimes = new List<int>();
            delayTimes.Add(1000);
            delayTimes.Add(2000);
            delayTimes.Add(3000);
            delayTimes.Add(4000);

            foreach (int d in delayTimes)
            {
                Debug.WriteLine("Basic: StringResultProcessing {0}, started on thread: {1}", d, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(d);
                Debug.WriteLine("Basic: StringResultProcessing {0}, completed on thread: {1}", d, Thread.CurrentThread.ManagedThreadId);
            }

            sw.Stop();

            Debug.WriteLine("Basic: StringResultProcessing completed in: {0} milliseconds", sw.ElapsedMilliseconds);

            return "complete";
        }

        public async Task<string> AsyncStringResultProcessingTask()
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<int> delayTimes = new List<int>();
            delayTimes.Add(1000);
            delayTimes.Add(2000);
            delayTimes.Add(3000);
            delayTimes.Add(4000);

            foreach (int d in delayTimes)
            {
                Debug.WriteLine("Basic: AsyncStringResultProcessingTask {0}, started on thread: {1}", d, Thread.CurrentThread.ManagedThreadId);
                //Thread.Sleep(d);
                Task.Delay(d);
                Debug.WriteLine("Basic: AsyncStringResultProcessingTask {0}, completed on thread: {1}", d, Thread.CurrentThread.ManagedThreadId);
            }

            sw.Stop();

            Debug.WriteLine("Basic: AsyncStringResultProcessingTask completed in: {0} milliseconds", sw.ElapsedMilliseconds);

            return "complete";
        }


        


    }
}