using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace Githubusernamechecker
{
    class MainClass
    {
        //No API used lol, 403 errors :(
        public static void Main(string[] args)
        {
            Console.Title = "Github username checker V1";
            List<string> available = new List<string>();
            if (args.Length != 0)
            {
                if (File.Exists(args[0]))
                {
                    var lines = File.ReadLines(args[0]);
                    foreach (var line in lines)
                    {
                        using (WebClient client = new WebClient())
                        {
                            try
                            {
                                client.DownloadData("https://github.com/" + line);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(line + " - unavailable");
                            }
                            catch (WebException e)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(line + " - available");
                                available.Add(line);
                            }
                        }
                        Thread.Sleep(100);
                    }
                }
                else
                {
                    Console.WriteLine("File does not exist.");
                }
            }
            else
            {
                Console.WriteLine("No filename specified.");
            }
            Console.ForegroundColor = ConsoleColor.White;
            File.WriteAllLines(Path.GetFileNameWithoutExtension(args[0]) + "-scanned.txt", available);
            Console.WriteLine("All names scanned!");
            Console.ReadLine();
        }
    }
}