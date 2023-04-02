using GradeBook.GradeBooks;
using System;

namespace GradeBook.UserInterfaces
{
    public static class StartingUserInterface
    {
        public static bool Quit = false;
        public static void CommandLoop()
        {
            while (!Quit)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine(">> What would you like to do?");
                var command = Console.ReadLine().ToLower();
                CommandRoute(command);
            }
        }

        public static void CommandRoute(string command)
        {
            if (command.StartsWith("create"))
                CreateCommand(command);
            else if (command.StartsWith("load"))
                LoadCommand(command);
            else if (command == "help")
                HelpCommand();
            else if (command == "quit")
                Quit = true;
            else
                Console.WriteLine("{0} was not recognized, please try again.", command);
        }

        public static void CreateCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 4)
            {
                Console.WriteLine("Command not valid, Create requires a name, type of gradebook, if it's weighted (true / false).");
                return;
            }
            var name = parts[1];
            var type = parts[2];
            var isWeighted = bool.Parse(parts[3]);
            BaseGradeBook gradeBook;
            if (type.ToLower() == "standard")
            {
                gradeBook = new StandardGradeBook(name, isWeighted);
            }
            else if (type.ToLower() == "ranked")
            {
                gradeBook = new RankedGradeBook(name, isWeighted);
            }
            else
            {
                Console.WriteLine("{0} is not a supported type of gradebook, please try again.", type);
                return;
            }
            Console.WriteLine("Created gradebook {0}.", name);
            GradeBookUserInterface.CommandLoop(gradeBook);
        }

        public static void LoadCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 2)
            {
                Console.WriteLine("Command not valid, Load requires a name.");
                return;
            }
            var name = parts[1];
            var gradeBook = BaseGradeBook.Load(name);

            if (gradeBook == null)
                return;

            GradeBookUserInterface.CommandLoop(gradeBook);
        }

        public static void HelpCommand()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("\tcreate 'Name' 'Type' 'Weighted' - Creates a new gradebook where 'Name' is the name of the gradebook, 'Type' is what type of grading it should use, and 'Weighted' is whether or not grades should be weighted (true or false).");
            Console.WriteLine("\tadd 'Name' 'Grade' - Adds a grade between 0.0 and 100.0 to the gradebook with the name 'Name'.");
            Console.WriteLine("\tremove 'Name' 'Grade' - Removes a grade between 0.0 and 100.0 from the gradebook with the name 'Name'.");
            Console.WriteLine("\tstats 'Name' - Computes statistics for the gradebook with the name 'Name'.");
            Console.WriteLine("\thelp - Displays available commands.");
            Console.WriteLine("\tquit - Exits the program.");
        }
    }
}
