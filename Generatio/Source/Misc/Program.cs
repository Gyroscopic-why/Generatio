using System;
using static System.Console;


using static Generatio.UI;
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
            ResetUI(true);


            Write("\n\n");
            Pattern1 aboba = new(41, 16,
                //[ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Green],
                //[ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Green,
                //ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Red],
                [ConsoleColor.DarkRed, ConsoleColor.Red, ConsoleColor.Blue,
                ConsoleColor.DarkBlue, ConsoleColor.DarkMagenta, ConsoleColor.Magenta],
                "Aboba", true, true);

            //for (var i = 0; i < 30; i++)
            //{
            //    Write(Formulas.IncorrectTriangleElement(5, i, 0) + " ");
            //}
            //ReadKey();

            aboba.DrawFull();

            UInt16 originY = 14;

            Write("\n\n");
            UInt16 nX = aboba.NormalizeSize().nX;
            for (UInt16 i = 0; i < originY; i++)
            {
                aboba.DrawPreload(aboba.DrawTopLine(i, nX, (UInt16)((nX) / 2)), -1, true);
            }
            for (UInt16 i = originY; i < 2 * originY; i++)
            {
                aboba.DrawPreload(aboba.DrawBottomLine(i, nX, (UInt16)((nX) / 2), originY), -1, true);
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
                        if (!Settings.autoSaveAll) autoContinue = true;
                        break;


                    case "3":
                        Gallery.GalleryManager.NavigateGallery();
                        autoContinue = true;  //  Dont wait for additional input
                        break;
                    case "4":
                        Gallery.GalleryManager.UpdateGallery();
                        break;


                    case "5":
                        Settings.Change();
                        if (!Settings.showBasicInfo) autoContinue = true;
                        break;
                    case "6":
                        Settings.Load();
                        break;
                }

                //  Reset the UI if no patterns are in the danger of being cleared
                ResetUI(autoContinue);

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
