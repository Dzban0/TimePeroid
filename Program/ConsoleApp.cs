using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    public static partial class MyConsoleApp
    {
        private static bool shouldContinue = true;
        private static int nextID = 0;
        private static List<RaceMember> raceMembers = new List<RaceMember>();
        public static void Start()
        {
            StartupMessage();
            SampleDataFill();

            while (shouldContinue)
            {
                Analyze(Console.ReadLine());
            }

            ExitMessage();
        }

        private static void Analyze(string? instruction) {
            if (instruction is null || instruction == "") return;

            string command = instruction.Split(' ')[0].ToUpper();
            switch (command)
            {
                case "ADD":
                    ADD(instruction);
                    break;
                case "DEL":
                    DEL(instruction);
                    break;
                case "CMP":
                    CMP(instruction);
                    break;
                case "LST":
                    LST();
                    break;
                case "CLS":
                    CLS();
                    break;
                default:
                    NotRecognized(command);
                    break;
            }
        }

        private static void StartupMessage()
        {
            ConsoleColor consoleColor = Console.ForegroundColor; // previous color
            StringBuilder sb = new StringBuilder();
            
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            sb.AppendLine("Welcome!");
            sb.AppendLine("In this app you can add Marathon members and compare their times.");
            sb.AppendLine("To do so, you can use the following commands:");
            sb.AppendLine();
            Console.WriteLine(sb);

            Console.ForegroundColor = ConsoleColor.Cyan;
            sb.AppendLine("ADD <Name> <Surname> <Time hh:mm:ss> - adds new entry with a new member");
            sb.AppendLine("DEL <ID> - remove a race member");
            sb.AppendLine("CMP <ID> <ID> - compares two race members");
            sb.AppendLine("LST - list all race members");
            sb.AppendLine("CLS - close the program");
            sb.AppendLine();
            Console.WriteLine(sb);

            Console.ForegroundColor = consoleColor;
        }

        private static void ExitMessage()
        {
            ConsoleColor consoleColor = Console.ForegroundColor; // previous color
            StringBuilder sb = new StringBuilder();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            sb.AppendLine("Quitting...");
            sb.AppendLine();
            Console.WriteLine(sb);

            Console.ForegroundColor = consoleColor;
        }

        private static int NextID()
        {
            nextID++;
            return nextID;
        }

        private static void SampleDataFill()
        {
            ADD("ADD Daniel Baster 2:45:36:543");
            ADD("ADD Anna Zięba 1:22:45:111");
            ADD("ADD Robert Dowbor 0:35:28:34");
            ADD("ADD Kinga Adamczyk 0:35:28:34");
            ADD("ADD Kacper Kowal 2:01:15:546");
            ADD("ADD Paulina Domagała 0:35:43:153");
            ADD("ADD Joanna Sroka 1:01:11:064");

        }
    }
}
