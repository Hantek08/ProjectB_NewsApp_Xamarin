using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Json; //Requires nuget package System.Net.Http.Json
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;

using News.Models;
using News.Services;

namespace News.Consoles
{
    //Your can move your Console application Main here. Rename Main to myMain and make it NOT static and async
    class Program
    {
        static void Main(string[] args)
        {
            NewsService service = new NewsService();
            service.NewsAvailable += ReportNewsDataAvailable;
            Task<NewsGroup> t1 = null;
            Exception exception = null;

            try
            {
                //Create task and wait for completion
                for (NewsCategory i = NewsCategory.Business; i < NewsCategory.Technology + 1; i++)
                {
                    t1 = service.GetNewsAsync(i);
                }


                t1.Wait();

            }
            catch (Exception ex)
            {
                //if exception write the message later
                exception = ex;
            }
            Console.WriteLine("----------------------");

            for (NewsCategory i = NewsCategory.Business; i < NewsCategory.Technology + 1; i++)
            {
                if (t1?.Status == TaskStatus.RanToCompletion)
                {
                    NewsGroup news = t1.Result;
                    Console.WriteLine($"News in Category {i}");

                    news.Articles.ForEach(a => Console.WriteLine($"  - {a.DateTime.ToString("yyyy-MM-dd HH:mm")}: {a.Title} "));

                }
                else
                {
                    Console.WriteLine("Geolocation News service error.");
                }
            }

        }

        static void ReportNewsDataAvailable(object sender, string message)
        {
            Console.WriteLine($"Event message from news service: {message}");
        }
        #region used by the Console
        Views.ConsolePage theConsole;
        StringBuilder theConsoleString;
        public Program(Views.ConsolePage myConsole)
        {
            //used for the Console
            theConsole = myConsole;
            theConsoleString = new StringBuilder();
        }
        #endregion

        #region Console Demo program
        //This is the method you replace with your async method renamed and NON static Main
        public async Task myMain()
        {
            theConsole.WriteLine("Demo program output");

            //Write an output to the Console
            theConsole.Write("One ");
            theConsole.Write("Two ");
            theConsole.WriteLine("Three and end the line");

            //As theConsole.WriteLine return trips are quite slow in UWP, use instead of myConsoleString to build the the more complex output
            //string using several myConsoleString.AppendLine instead of several theConsole.WriteLine. 
            foreach (char c in "Hello World from my Console program")
            {
                theConsoleString.Append(c);
            }

            //Once the string is complete Write it to the Console
            theConsole.WriteLine(theConsoleString.ToString());

            theConsole.WriteLine("Wait for 2 seconds...");
            await Task.Delay(2000);

            //Finally, demonstrating getting some data async
            theConsole.WriteLine("Download from https://dotnet.microsoft.com/...");
            theConsoleString.Clear();
            using (var w = new WebClient())
            {
                string str = await w.DownloadStringTaskAsync("https://dotnet.microsoft.com/");
                theConsoleString.Append($"Nr of characters downloaded: {str.Length}");
            }
            theConsole.WriteLine(theConsoleString.ToString());
        }

        //If you have any event handlers, they could be placed here
        void myEventHandler(object sender, string message)
        {
            theConsole.WriteLine($"Event message: {message}"); //theConsole is a Captured Variable, don't use myConsoleString here
        }
        #endregion
    }
}
