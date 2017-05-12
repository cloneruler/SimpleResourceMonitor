using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Speech.Synthesis;
using System.Windows.Forms;
    
namespace ResourceMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            //Greet the user in default voice.
            SpeechSynthesizer synth = new SpeechSynthesizer();
            //synth.Speak("Welcome to Cloneruler's Resource Monitor.");
            
            //This will pull the CPU usage in percentage
            PerformanceCounter cpuMonitor = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
            //This will pull the current available RAM in MBs
            PerformanceCounter ramMonitor = new PerformanceCounter("Memory", "Available MBytes");
            //This will give us system uptime in seconds.
            PerformanceCounter uptimeMonitor = new PerformanceCounter("System", "System Up Time");

            bool userAlerts = true;

            Console.WriteLine("Do you want alerts activated? (y/n)");
            var answer = Console.ReadKey();

                switch (answer.Key)
                {
                    case ConsoleKey.Y:
                        break;
                    case ConsoleKey.N:
                        userAlerts = false;
                        break;
                    default:
                    Console.WriteLine("That is not y/n");
                        break;
                }
 

            while (true)
            {
                //Get actual measurements of CPU and RAM
                float cpuUsage = cpuMonitor.NextValue();
                float ramLeft = ramMonitor.NextValue();
                

                //Display CPU load and available RAM left in console.
                Console.WriteLine("CPU Load     : {0}\n", cpuUsage);
                Console.WriteLine("Available RAM: {0}MB\n", ramLeft);
                Thread.Sleep(1500);

                //If CPU usage is equal to or above 80%, a warning will be displayed to the user if they pressed "y" to enable alerts.
                if(cpuUsage >= 80 && userAlerts == true)
                {
                    MessageBox.Show("WARNING: High CPU Usage. Consider closing some intensive programs");
                }
                //If there is less than 2GB of RAM left, a warning will be displayed to the user if they pressed "y" to enable alerts.
                else if (ramLeft < 4000 && userAlerts == true)
                {
                    MessageBox.Show("Amount of RAM left has dropped below 4GB!");
                    
                }
                Thread.Sleep(500);
                Console.Clear();
            }


        }
    }
}
