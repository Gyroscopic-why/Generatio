using System;
using static System.Console;


using static Generatio.GalleryLogic;
using static Generatio.GlobalSettings;
using static Generatio.GlobalVariables;
using static Generatio.CustomFunctions;
using static Generatio.CustomProcedures;


namespace Generatio
{
    internal class Program
    {
        //---------------  MAIN PROGRAM  ------------------------------------------------------------//

        static void Main()
        {
            OutputEncoding = System.Text.Encoding.Unicode;
            byte task;
            byte[] bestPatterns;
            int colorsAmount;
            ConsoleColor[] colors;

            //  Updating the gallery save  //
            UpdateStockGallery();          //  For the stock patterns
            UpdateUserGallery();           //  For the user  patterns

            ResetSettings();               //  Reset the settings to default values
            //  LoadSettings();            //  Load the settings from the file

            Title = "Generatio 1.7";
            Write("\n\n\n\n\n\n");
            if (!gIgnoreFullScreen) ForceFullScreen();
            Clear();

            Write("\n\n\n\n\n\n");    //
            PrintLogo();              // Print the LOGO

            task = GetUserTask();     // User is navigating menu

            while (task != 0) {
                switch (task) {
                    case 1:           // Writing program info         -----  OPTION 1
                        WriteInfo();
                        break;

                    case 2:           // Generating patterns          -----  OPTION 2

                        if (!gGeneratedPatterns) Clear();  // Clear console if never generated any patterns
                        gGeneratedPatterns = true;         // 
                        Write("\n\n\n\n\n\n");             //
                        PrintLogo();                       //
                        Write("\n\t\t\t\t\t\t\tВыбрано: --- === Генерация узоров === ---\n\n");

                        int height = GetSize("высот");   //
                        int width  = GetSize("ширин");   // Getting the pattern sizes

                        if (ChooseColorType())  //-----------   User colors choice
                        {         
                            // Getting the amount of the colors
                            colorsAmount = GetColorsAmount(Math.Min(height, width));

                            // Converting colors from numbers to console colors
                            colors = ConvertColorsToConsole(GetCustomColors(colorsAmount));    
                        }
                        else  //-----------------------------   Asset colors choice
                        {
                            // Getting the asset colors array
                            colors = StoredColors[GetAssetColorsID()];

                            // Getting the amount of the colors
                            colorsAmount = colors.Length;               
                        }

                        // Choosing the best patterns
                        bestPatterns = GetBestPatterns(colorsAmount, width, height);

                        // Printing them
                        PrintPatterns(bestPatterns, width, height, colors);           

                        // Write success message
                        Write("\n\n\n\t\tГенерация узоров завершена.\n\n\n\n\n");
                        break;


                    case 3:     // Pattern gallery                    -----  OPTION 3
                        NavigateGallery();  // Navigate the gallery of stored patterns
                        break;


                    case 4:     // Update the gallery                 -----  OPTION 4
                        UpdateStockGallery();  // Update the stock patterns save
                        UpdateUserGallery();   // Update the user  patterns save
                        break;


                    case 5:     // Change the program settings        -----  OPTION 6
                        ChangeSettings();      // Update the program settings
                        break;

                    case 6:     // Generate a pattern by a shortcut   -----  OPTION 5
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
