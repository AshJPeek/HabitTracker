using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.ConsoleInterface
{
    internal class MenuHelpers
    {
        public static void ReturnToMenu(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
        }
        public static void MainMenuReturn()
        {
            Console.WriteLine("Returning to main menu. Press any button to continue.");
            Console.ReadLine();
            Menu.MenuOptions();
        }
        public static void IncorrectMenuInput()
        {
            Console.Clear();
            ReturnToMenu("Please enter a number between 0 - 4. Press any button to return to menu");
            Console.Clear(); Menu.StepsTrackerMenu();
        }
    }
}
