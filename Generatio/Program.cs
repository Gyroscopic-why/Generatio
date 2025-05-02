using static System.Console;


using static Generatio.GalleryLogic;
using static Generatio.GlobalSettings;
using static Generatio.CustomFunctions;
using static Generatio.CustomProcedures;



namespace Generatio
{
    internal class Program
    {
        static void Main()
        {
            Title = "Generatio 2.1.2";
            OutputEncoding = System.Text.Encoding.UTF8;


            //  Reset settings and gallery, load previous save
            ResetAll();
            //  Reset the console UI
            ResetUI(!gIgnoreFullScreen, true);


            // User is navigating menu
            string task = GetUserTask();

            while (task != "0") 
            {
                //  Wait for additional input at the end
                bool autoContinue = false;

                switch (task) 
                {
                    case "1":
                        WriteProgramInfo();           //  Printing info about this program
                        autoContinue = true;          //  Dont wait for additional input
                        break;


                    case "2":
                        autoContinue = CreatePatternsLogic();  //  Creating patterns according to the user input
                        if (!gAutoSave) autoContinue = true;
                        break;


                    case "3":
                        NavigateGallery();     //  Navigate the gallery of stored patterns
                        autoContinue = true;   //  Dont wait for additional input
                        break;
                    case "4":
                        UpdateStockGallery();  //  Update the stock patterns save
                        UpdateUserGallery();   //  Update the user  patterns save
                        break;


                    case "5":
                        ChangeSettings();  //  Update the program settings
                        if (!gShowInfo) autoContinue = true;
                        break;
                    case "6":
                        LoadSettings();    //  Load the previous settings save
                        break;
                }


                //  Reset the UI if no patterns are in the danger of being cleared
                ResetUI(!gIgnoreFullScreen, autoContinue);
                

                //  User is navigating the menu
                task = GetUserTask();
            }


            //------------  Exiting the program  (Option 6)  ----------------//
            //
            //  Write a thankfull goodbye message
            Write("\n\t\t[♥]  - Спасибо что использовали мою программу");
            Write("\n\t\t       Нажмите любую кнопку для выхода\n");
            //
            //  Wait for any key to exit the program
            ReadKey();
        }
    }
}
