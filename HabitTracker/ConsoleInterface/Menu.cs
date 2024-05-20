using HabitTracker.Persistence.Repositories;
using static HabitTracker.Persistence.Repositories.CalorieTrackerRepository;


namespace HabitTracker.ConsoleInterface
{
    internal class Menu
    {
        public static void MenuOptions()
        {
            Console.Clear();
            var exitApplication = false;

            while (!exitApplication)
            {
                Console.WriteLine("Main Menu\n");
                Console.WriteLine("Welcome to your habit tracker, what would you like to track?\n");
               Console.WriteLine("""
                                    Type S to track step count.
                                    Type C to track calories.
                                    Type E to exit the application
                                 """);

                var habitChosen = Console.ReadLine();
                switch (habitChosen?.ToLower())
                {
                    case "s":
                        StepsTrackerMenu();
                        break;
                    case "c":
                        CalorieTrackerMenu();
                        break;
                    case "e":
                        exitApplication = true; ;
                        break;
                }
            }
        }
        public static void StepsTrackerMenu()
        {

            Console.Clear();
            Console.WriteLine("""
                              _______________________________________

                              Type 0 to return to main menu.
                              Type 1 to view all records.
                              Type 2 to insert record.
                              Type 3 to delete record.
                              Type 4 to update record.
                              _______________________________________
                              """);

            var menuItemChosen = Console.ReadLine();

            switch (menuItemChosen)
            {
                case "0":
                    {
                        MenuHelpers.MainMenuReturn();
                    }
                    break;
                case "1":
                    {
                        Console.Clear();
                        var stepsTaken = StepTrackerRepository.RetrieveRecords();                      
                        Console.WriteLine("-----------------------------------------");

                        if (stepsTaken != null)
                        {
                            foreach (var stepTaken in stepsTaken)
                            {
                                Console.WriteLine($"{stepTaken.Id} - {stepTaken.Date:dd-MM-yyyy} - Steps: {stepTaken.Quantity}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No records found. Press any button to continue.");
                            Console.ReadLine();
                        }
                        Console.WriteLine("-----------------------------------------");
                        MenuHelpers.ReturnToMenu("Press any button to return to menu");
                        Console.Clear();
                        StepsTrackerMenu();
                    }
                    break;
                case "2":
                    {
                        string todaysDate = UserInput.GetDate(); int stepsTaken = UserInput.GetDataInput("Please enter the number of steps you have taken today");
                        StepTrackerRepository.Insert(todaysDate, stepsTaken);
                        Console.Clear(); 
                        StepsTrackerMenu();
                    }
                    break;
                case "3":
                    {
                        Console.Clear();
                        var stepsTaken = StepTrackerRepository.RetrieveRecords();
                        Console.WriteLine("-----------------------------------------");

                        if (stepsTaken != null)
                        {
                            foreach (var stepTaken in stepsTaken)
                            {
                                Console.WriteLine($"{stepTaken.Id} - {stepTaken.Date:dd-MM-yyyy} - Steps: {stepTaken.Quantity}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No records found. Press any button to continue.");
                            Console.ReadLine();
                            Console.Clear();
                            StepsTrackerMenu();
                        }
                        Console.WriteLine("-----------------------------------------");


                        string recordId = UserInput.GetStepsRecordId("Please enter the record Id of the record you would like to delete.");
                        var rowCount = StepTrackerRepository.DeleteRecords(recordId);

                        if (rowCount == 0)
                        {
                            Console.WriteLine($"\nRecord with Id {recordId} doesn't exist. Press any button to return to menu.");
                            Console.ReadLine();
                        }
                        else
                        {
                            MenuHelpers.ReturnToMenu("Record successfully deleted. Press any button to return to menu");
                        }
                        Console.Clear(); 
                        StepsTrackerMenu();
                    }
                    break;
                case "4":
                    {
                        Console.Clear();
                        var stepsTaken = StepTrackerRepository.RetrieveRecords();
                        Console.WriteLine("-----------------------------------------");

                        if (stepsTaken != null)
                        {
                            foreach (var stepTaken in stepsTaken)
                            {
                                Console.WriteLine($"{stepTaken.Id} - {stepTaken.Date:dd-MM-yyyy} - Steps: {stepTaken.Quantity}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No records found. Press any button to continue.");
                            Console.ReadLine();
                            Console.Clear();
                            StepsTrackerMenu();
                        }
                        Console.WriteLine("-----------------------------------------");

                        var recordId = UserInput.GetStepsRecordId("Please enter the Id of the record you would like to update");
                        Console.Clear();
                        if (StepTrackerRepository.IdMatch(recordId) == false)
                        {
                            Console.WriteLine($"Record with Id {recordId} doesn't exist.");
                            Console.ReadLine();
                            StepsTrackerMenu();
                        }
                        else
                        {
                            var date = UserInput.GetDate();
                            var quantity = UserInput.GetDataInput("Please enter the number of steps you have taken today");

                            var successfullyUpdated = StepTrackerRepository.UpdateRecords(recordId, date, quantity);

                            MenuHelpers.ReturnToMenu(successfullyUpdated ? "Record successfully updated. Press any button to return to menu" : "An error has occured. Please try again");

                            Console.Clear();
                            StepsTrackerMenu();
                        }
                    }
                    break;
                default:
                    {
                        MenuHelpers.IncorrectMenuInput();
                    }
                    break;
            }

        }
        public static void CalorieTrackerMenu()
        {
            Console.Clear();
            Console.WriteLine("""
                                _______________________________________

                                Type 0 to return to main menu.
                                Type 1 to view all records.
                                Type 2 to insert record.
                                Type 3 to delete record.
                                Type 4 to update record.
                                ________________________________________
                                """);

            var menuItemChosen = Console.ReadLine();

            switch (menuItemChosen)
            {
                case "0":
                    {
                        MenuHelpers.MainMenuReturn();
                    }
                    break;
                case "1":
                    {
                        Console.Clear();
                        var caloriesEaten = CalorieTrackerRepository.RetrieveRecords();
                        Console.WriteLine("-----------------------------------------");

                        if (caloriesEaten != null)
                        {
                            foreach (var calorieEaten in caloriesEaten)
                            {
                                Console.WriteLine($"{calorieEaten.Id} - {calorieEaten.Date:dd-MM-yyyy} - Calories: {calorieEaten.Quantity}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No records found. Press any button to continue.");
                            Console.ReadLine();
                        }
                        
                        Console.WriteLine("-----------------------------------------");
                      
                        MenuHelpers.ReturnToMenu("Press any button to return to menu");
                        Console.Clear();
                        CalorieTrackerMenu();
                    }
                    break;
                case "2":
                    {
                        var todaysDate = UserInput.GetDate(); int stepsTaken = UserInput.GetDataInput("Please enter the number of calories you have eaten today");
                        CalorieTrackerRepository.Insert(todaysDate, stepsTaken);
                        Console.Clear();
                        CalorieTrackerMenu();
                    }
                    break;
                case "3":
                    {
                        Console.Clear();

                        var caloriesEaten = CalorieTrackerRepository.RetrieveRecords();
                        Console.WriteLine("-----------------------------------------");

                        if (caloriesEaten != null)
                        {
                            foreach (var calorieEaten in caloriesEaten)
                            {
                                Console.WriteLine($"{calorieEaten.Id} - {calorieEaten.Date:dd-MM-yyyy} - Calories: {calorieEaten.Quantity}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No records found. Press any button to continue.");
                            Console.ReadLine();
                        }

                        Console.WriteLine("-----------------------------------------");

                        var recordId = UserInput.GetCalorieRecordId("Please enter the Id of the record you would like to delete");
                        var rowCount = CalorieTrackerRepository.DeleteRecords(recordId);
                        Console.Clear();
                        if (rowCount == 0)
                        {
                            Console.WriteLine($"Record with Id {recordId} doesn't exist. Press any button to return to menu.");
                            Console.ReadLine();
                        }
                        else
                        {
                            MenuHelpers.ReturnToMenu("Record successfully deleted. Press any button to return to menu");
                        }
                        Console.Clear(); 
                        CalorieTrackerMenu();
                    }
                    break;
                case "4":
                    {
                        Console.Clear();

                        var caloriesEaten = CalorieTrackerRepository.RetrieveRecords();
                        Console.WriteLine("-----------------------------------------");

                        if (caloriesEaten != null)
                        {
                            foreach (var calorieEaten in caloriesEaten)
                            {
                                Console.WriteLine($"{calorieEaten.Id} - {calorieEaten.Date:dd-MM-yyyy} - Calories: {calorieEaten.Quantity}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No records found. Press any button to continue.");
                            Console.ReadLine();
                        }

                        Console.WriteLine("-----------------------------------------");

                        var recordId = UserInput.GetCalorieRecordId("Please enter the Id of the record you would like to update");
                        Console.Clear();
                        if (CalorieTrackerRepository.IdMatch(recordId) == false)
                        {
                            Console.WriteLine($"Record with Id {recordId} doesn't exist.");
                            Console.ReadLine();
                            CalorieTrackerMenu();
                        }
                        else
                        {
                            var date = UserInput.GetDate();
                            var quantity = UserInput.GetDataInput("Please enter the number of steps you have taken today");

                            var successfullyUpdated = CalorieTrackerRepository.UpdateRecords(recordId, date, quantity);

                            MenuHelpers.ReturnToMenu(successfullyUpdated ? "Record successfully updated. Press any button to return to menu" : "An error has occured. Please try again");

                            Console.Clear();
                            CalorieTrackerMenu();
                        }                       
                        break;
                    }
                default:
                    {
                        Console.Clear();
                        MenuHelpers.ReturnToMenu("Please enter a number between 0 - 4. Press any button to return to menu");
                        Console.Clear();
                        CalorieTrackerMenu();
                    }
                    break;
            }

        }




    }


}

