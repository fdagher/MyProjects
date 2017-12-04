using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            DoWork();

            WriteToConsole($"Main Completed Successfully");

            Console.ReadLine();
        }

        static async Task<string> DoWork()
        {
            try
            {
                WriteToConsole("DoWork Enter");

                // Parallel Run
                //Task.Run(() => Method1());

                Thread.Sleep(100);
                WriteToConsole("DoWork Continue");

                // Log Exit
                //Task tsk = Task.Run(() => Monitor("Function - SendMail - Exit", "{}"));
                //tsk.Wait();


                await Task.Run(() => Method1());

                WriteToConsole("DoWork After Method1");

                await Method2();

                WriteToConsole("DoWork After Method2");
                //await Monitor("Function - SendMail - Exit", "{}");

                //await Task.Run(() => Thread.Sleep(10000));
                //result = await Monitor("Function - SendMail - Exit", "{}");

                WriteToConsole($"DoWork Completed Successfully");

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                WriteToConsole($"ERROR: {ex.ToString()}");
            }

            return "";
        }
        private static string Method1()
        {
            try
            {
                WriteToConsole($"Method1 Start");

                Thread.Sleep(2000);
                //await Task.Delay(2000);
                WriteToConsole($"Method1 End");

                return "";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "";
            }
        }

        private static async Task<string> Method2()
        {
            try
            {
                WriteToConsole($"Method2 Start");
                await Task.Delay(2000);
                WriteToConsole($"Method2 End");

                return "";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "";
            }
        }
        static void WriteToConsole(string message)
        {
            Console.WriteLine($"{message} : Thread {Thread.CurrentThread.ManagedThreadId} : Time {DateTime.Now.ToString("hh:mm:ss.fff tt")}");
        }
    }
}
