﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("Master ?");
            string PassWord = Console.ReadLine();
            if (PassWord == "Darkness") 
            {
                Console.Clear();
                Console.WriteLine("LogedIn ... Welcome");
                Console.WriteLine("Version 1.5");
                
            }
            else
            {
                Environment.Exit(404);
            }
            bool exit = true;
            while (exit)
            {               
                string Command = Console.ReadLine();
                Fonction.CommandExe(Command);
                System.Threading.Thread.Sleep(500);               
            }
           

        }
 
    }

}
