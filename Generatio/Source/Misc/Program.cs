using static System.Console;


using static Generatio.UI;
using static Generatio.GlobalSettings;
using static Generatio.CustomFunctions;
using static Generatio.CustomProcedures;
using Generatio.Source.Patterns;


namespace Generatio
{
    internal class Program
    {
        static void Main()
        {
            Title = "Generatio 2.2 TBuild";
            OutputEncoding = System.Text.Encoding.UTF8;


            ResetAll();  //  Reset settings and gallery, load previous save
            ResetUI(!gIgnoreFullScreen, true);


            Write("\n\n");
            Pattern1 aboba = new(24, 16,
                [ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Green],
                //[ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Green,
                //ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Red],
                "Aboba", true, true);

            for (var i = 0; i < 30; i++)
            {
                Write(Formulas.IncorrectTriangleElement(5, i, 0) + " ");
            }
            ReadKey();

            aboba.DrawFull();

            Write("\n\n");

            for (UInt16 i = 0; i < 8; i++)
            {
                aboba.DrawPreload(aboba.DrawTopLine(i, 0, 72, aboba.NormalizeSize().nX), 72, true);
            }
            for (UInt16 i = 8; i < 32; i++)
            {
                aboba.DrawPreload(aboba.DrawBottomLine(i, 0, 72, aboba.NormalizeSize().nX), 72, true);
            }
            Write("\n\n");

            ;


            string task = GetUserTask();

            while (task != "0") 
            {
                bool autoContinue = false;  //  Wait for additional input at the end

                switch (task) 
                {
                    case "1":
                        WriteProgramInfo();
                        autoContinue = true;  //  Dont wait for additional input
                        break;
                    case "2":
                        autoContinue = CreatePatternsLogic();
                        if (!gAutoSave) autoContinue = true;
                        break;


                    case "3":
                        Gallery.GalleryManager.NavigateGallery();
                        autoContinue = true;  //  Dont wait for additional input
                        break;
                    case "4":
                        Gallery.GalleryManager.UpdateGallery();
                        break;


                    case "5":
                        ChangeSettings();
                        if (!gShowInfo) autoContinue = true;
                        break;
                    case "6":
                        LoadSettings();
                        break;
                }

                //  Reset the UI if no patterns are in the danger of being cleared
                ResetUI(!gIgnoreFullScreen, autoContinue);

                task = GetUserTask();
            }


            //------------  Exiting the program  (Option 6)  ----------------//
            //
            Write("\n\t\t[♥]  - Спасибо что использовали мою программу");
            Write("\n\t\t       Нажмите любую кнопку для выхода\n");
            ReadKey();
        }
    }
}
