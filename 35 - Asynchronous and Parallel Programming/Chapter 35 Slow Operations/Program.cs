using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter_35_Slow_Operations
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadVersion();
            //ThreadPoolVersion();
            //TaskVersion();
            //TaskWithResult();
            //DoAsyncWithResult();
            //DoAsyncWithReturnValue();
            WebMethodDownload();

            WaitAndCount();

        }

        static void WaitAndCount()
        {
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }
        }

        static void ThreadVersion()
        {
            Thread thread = new Thread(PerformSlowOperation, 0);
            thread.Start();
        }

        static void ThreadPoolVersion()
        {
            ThreadPool.QueueUserWorkItem(PerformSlowOperationThreadPool);
        }

        static void PerformSlowOperation()
        {
            Console.WriteLine(SlowOperation.Process());
        }

        static void PerformSlowOperationThreadPool(object state)
        {
            Console.WriteLine(SlowOperation.Process());
        }

        static void TaskVersion()
        {
            var task = new Task(() => Console.WriteLine(SlowOperation.Process()));
            task.Start();
        }

        static void TaskVersionOneLine()
        {
            Task.Factory.StartNew(() => Console.WriteLine(SlowOperation.Process()));
        }

        static void TaskWithResult()
        {
var task = Task<int>.Factory.StartNew(() => SlowOperation.Process());
int i = task.Result;
Console.WriteLine(i);
        }

        static void DoAsyncWithResult()
        {
            AsyncTaskWithResult();
        }

static async void AsyncTaskWithResult()
{
    int result = await Task<int>.Factory.StartNew(() => SlowOperation.Process());
    Console.WriteLine(result);
}

static void DoAsyncWithReturnValue()
{
    CallAsyncWithReturnValue();
}

static async void CallAsyncWithReturnValue()
{
    int result = await AsyncTaskWithReturnValue();
    Console.WriteLine(result);
}

static async Task<int> AsyncTaskWithReturnValue()
{
    Console.WriteLine("Start");
    int result1 = await Task<int>.Factory.StartNew(() => SlowOperation.Process());
    Console.WriteLine("First value back");
    int result2 = await Task<int>.Factory.StartNew(() => SlowOperation.Process());
    Console.WriteLine("Second value back");
    return result1 + result2;
}

static async void WebMethodDownload(string url)
{
    string contents = await new WebClient().DownloadStringTaskAsync("http://www.bing.com");
    Console.WriteLine(contents);
}

    }
}
