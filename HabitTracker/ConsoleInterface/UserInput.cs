using HabitTracker.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.ConsoleInterface
{
    internal class UserInput
    {
        public static string GetDate()
        {
            Console.Clear();
            Console.WriteLine("Please enter the date (dd-mm-yyyy)");
            string todaysDate = Console.ReadLine();
            if (todaysDate == "0") GetDate();

            while (!DateTime.TryParseExact(todaysDate, "dd-MM-yyyy", new CultureInfo("en-UK"), DateTimeStyles.None, out _))
            {
                Console.Clear();
                Console.WriteLine("Invalid date. (Format: dd-mm-yyyy). Please try again");
                todaysDate = Console.ReadLine();
            }
            Console.Clear();
            return todaysDate;
        }
        public static int GetDataInput(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            string dataInput = Console.ReadLine();

            if (dataInput == "0")
            {
                GetDataInput(message);
            }

            while (!int.TryParse(dataInput, out _) || Convert.ToInt32(dataInput) < 0)
            {
                Console.Clear();
                Console.WriteLine("Invalid number. Please try again.");
                dataInput = Console.ReadLine();
            }
            Console.Clear();

            int integerSteps = Convert.ToInt32(dataInput);
            return integerSteps;
        }
        public static string GetStepsRecordId(string message)
        {
            Console.Clear();
            StepTrackerRepository.RetrieveRecords();
            Console.WriteLine(message);
            var recordId = Console.ReadLine();
            return recordId;
        }
        public static string GetCalorieRecordId(string message)
        {
            Console.Clear();
            CalorieTrackerRepository.RetrieveRecords();
            Console.WriteLine(message);
            var recordId = Console.ReadLine();
            return recordId;
        }
    }
}
