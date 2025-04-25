using static System.Console;


using static Generatio.GalleryLogic;
using static Generatio.GlobalSettings;
using static Generatio.CustomFunctions;
using static Generatio.CustomProcedures;



namespace Generatio
{
    internal class Program
    {
        //---------------  MAIN PROGRAM  ------------------------------------------------------------//

        static void Main()
        {
            OutputEncoding = System.Text.Encoding.UTF8;
            string task;

            //  Reset settings and gallery, load previous save
            ResetAll();



            Title = "Generatio 2.0";
            Write("\n\n\n\n\n\n");
            if (!gIgnoreFullScreen) ForceFullScreen();
            Clear();

            Write("\n\n\n\n\n\n");    //
            PrintLogo();              // Print the LOGO

            task = GetUserTask();     // User is navigating menu

            while (task != "0") {
                switch (task) {
                    case "1":           // Writing program info         -----  OPTION 1
                        WriteInfo();
                        break;

                    case "2":           // Generating patterns          -----  OPTION 2
                        GenerateUserPatternsLogic();  // Generate patterns by the user input
                        break;


                    case "3":     // Pattern gallery                    -----  OPTION 3
                        NavigateGallery();  // Navigate the gallery of stored patterns
                        break;


                    case "4":     // Update the gallery                 -----  OPTION 4
                        UpdateStockGallery();  // Update the stock patterns save
                        UpdateUserGallery();   // Update the user  patterns save
                        break;


                    case "5":     // Change the program settings        -----  OPTION 6
                        ChangeSettings();      // Update the program settings
                        break;

                    case "6":   // Generate a pattern by a shortcut   -----  OPTION 5
                                //
                                // Absolutely temporary because I want to parse the shortcut straight from the menu
                                // That way it will save even more time
                        break;

                    default:    // Exit the program                   -----  OPTION 0
                        break;
                }

                task = GetUserTask(); // User is navigating the menu
            }


            // Exiting the program  -----  OPTION 6

            // Write a thankfull goodbye message
            Write("\n\t\t[♥]  - Спасибо что использовали мою программу");
            Write("\n\t\t       Нажмите любую кнопку для выхода\n");

            // Wait for any key to exit the program
            ReadKey();
        }
    }
}
