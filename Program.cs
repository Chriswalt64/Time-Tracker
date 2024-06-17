using System;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;

namespace Time_Tracker;
/*Time tracking
Chris Walters

I wanted a fairly quick way to track the time I'm taking on tasks throughout my workday. As someone who struggles with maintaing their attention, I find myself not happy with my productivity. 
I also wanted a way to track how long some tasks were taking as I have been given extra job duties to cover empty positions on my team and wanted something to show my boss when they weren't happy with my progress. 

Goals
- add a category selection, that reads a config file to give options, allowing for customizability
- write to a file and save it. The intention of this program is to run in a console window that can be minimized or just moved behind what you're working on, 
so in the event of a crash/unexpected end of some kind the file is safe and an entire days worth of time tracking doesnt just disappear. 
 
 Git test 
*/
class Program
{
    static void Main(string[] args)
    {
        string?[] Settings = ReadAllSettings();
        Console.WriteLine("--Time Tracker--");
        Console.WriteLine("Enter the name of the task.");
        string? taskName = Console.ReadLine();
        Console.WriteLine("Can you give a brief description of the task?");
        string? taskDesc = Console.ReadLine();
        Console.WriteLine(taskName);
        Console.WriteLine($"Press any key to start time tracking for {taskName}");

        Console.ReadKey();

        string? taskStart = DateTime.Now.ToString("HH:mm:ss");
        Console.WriteLine($"Press any key to end time tracking for {taskName}");

        Console.ReadKey();

        string? taskEnd = DateTime.Now.ToString("HH:mm:ss");

        Console.WriteLine($"Start: {taskStart} //// End: {taskEnd}");

        string? timeEntry = $"{taskName},{taskDesc},{taskStart},{taskEnd}";


         
        AddEntry(timeEntry, Settings);
    }



    static string?[] ReadAllSettings()  
    {  
        var appSettings = System.Configuration.ConfigurationManager.AppSettings;  

        var  Settings = appSettings.AllKeys;
        return Settings;
    }

    static void AddEntry(string entry, string[] settings)
    {
        string? path = settings[0];

        try
        {
            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter(path);
            //Write a line of text
            sw.WriteLine(entry);

            //Close the file
            sw.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }
    }
}
