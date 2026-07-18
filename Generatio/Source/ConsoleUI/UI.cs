using Generatio.Source;
using System;

using static System.Console;



namespace Generatio
{
    internal class UI
    {
        static public void ForceFullScreen(Int32 X = 0, Int32 Y = 0)
        {
            UInt16 Counter = 0;
            if (X == 0) X = LargestWindowWidth / 13;
            if (Y == 0) Y = LargestWindowHeight / 26;
            if (!Settings.ignoreFullScreen)
            {
                if (WindowWidth < X || WindowHeight < Y)
                {
                    Write("\t\tДля продолжения работы программы требуется увеличить разрешение экрана\n");
                    Write("\t\tнастоятельно рекомендуется перейти в полноэкранный режим.\n");
                    Write("\t\tДля этого нажмите fullscreen/РАЗВЕРНУТЬ или клавишу F11/(Fn+F11)\n");
                    Write("\t\tПосле увеличения размера окна, нажмите любую кнопку для продолжения\n");
                }
                while (WindowWidth <= X || WindowHeight <= Y)
                {
                    Counter++;
                    if (Counter > 65001) Counter -= 65000;
                    ForegroundColor = ConsoleColor.White;
                    Write("\n\n\t\tНеобходимый размер окна: не менее чем " + X + "x" + Y + "\n\t\tТекущий размер: ");
                    ForegroundColor = ConsoleColor.Red;
                    Write(WindowWidth + "x" + WindowHeight);
                    ForegroundColor = ConsoleColor.Black;
                    ReadKey();
                }
                if (Counter > 1) Write("\n\n");
                ForegroundColor = ConsoleColor.White;
            }
        }
             //  Force the user to enlarge the window to prevent limitations of the size of the patterns

        static public void ResetAll()
        {
            Settings.Reset();  //  Reset settings to default (fail save)
            Settings.Load();   //  Load previous save of the settings

            Gallery.GalleryManager.UpdateGallery();  //  UpdateGallerySave
        }



        //----------------  Procedures exclusively meant for printing info  -----------------------------------//


        static public void ResetUI(bool autoContinue)
        {
            if (!autoContinue)
            {
                Write("\n\t\tГотово! Нажмите любую кнопку чтобы продолжить: ");
                ReadKey();
            }

            if (Settings.ignoreFullScreen) ForceFullScreen(70, 30);

            Write(new string ('\n', WindowHeight));
            Clear();

            Write("\n\n\n\n\n\n");
            PrintLogo();
        }

        static public void ResetSizeUI(string sizeType, UInt16 maxSize, UInt32 realMax, UInt16 last = 0)
        {
            ResetUI(true);
            Write("\n\t\t\t\t\t\tВыбрано: --- === Создание узоров === ---\n\n\n");

            if (last != 0) Write("\t\t[=]  - Выбранная ширина: " + last + "\n\n");


            string margin = "       ";
            UInt16 tempBuffer = maxSize;
            while (tempBuffer > 0)
            {
                if (margin.Length > 0) margin = margin.Remove(0, 1);
                tempBuffer /= 10;
            }


            Write("\n\t\t[?]  - Выберете " + sizeType + "у узора:");

            Write("\n\t\t         > " + 5 + " - " + maxSize);
            if (maxSize == 65535)
            {
                Write(" (рекоменд: " + realMax + ") <    - Выбор в интервале");
                Write("\n\t\t         > СЛУЧ/RAND <                   - Случайное значение");
                Write("\n\t\t         > 0 <                           - Назад");
            }
            else
            {
                Write(" <" + margin + " - Выбор в интервале");
                Write("\n\t\t         > СЛУЧ/RAND <   - Случайное значение");
                Write("\n\t\t         > 0 <           - Назад");
            }


            Write("\n\t\t[i]  - Чем больше " + sizeType + "а узора, тем он красивее!\n");
        }



        static public void WriteProgramInfo()
        {
            ResetUI(true);
            Write("\n\t\t\t\t\t\tВыбрано: --- === Информация о программе === ---\n\n\n");

            //-----------------------  WRITTING INFO ABOUT THE PROGRAM  -----------------------------//


            Write("\n\t\tЭта программа была создана в учебных целях учеником Гунько Егором, для облегчения работы художникам.");
            Write("\n\t\tТехническая информация:  v2.2 technical build\n");

            Write("\n\t\t[i]  - Функционал:");
            Write("\n\t\t         > Создание узоров (выбор размеров, цветов, названия для созданных узоров");
            Write("\n\t\t         > Просмотр  сгенерированных узоров");
            Write("\n\t\t         > Изменение настроек для более удобной работы с программой\n");

            Write("\n\t\t[i]  - Инструкция по сохранению узоров в галерею:");
            Write("\n\t\t         > Вводите номера узоров, которые желаете сохранить через ','");
            Write("\n\t\t       Если хотите добавить узору название напишите:");
            Write("\n\t\t         > номерузора = название\n");

            ForegroundColor = ConsoleColor.DarkGray;
            Write("\n\t\t       Примеры ввода:");
            Write("\n\t\t         > 1, 3, 10, 9");
            Write("\n\t\t         > 1=Солнце,4=Море");
            Write("\n\t\t         > 9, 8=20.2.2020 Февраль, 7, 2 = мойшедевр\n");
            ForegroundColor = ConsoleColor.White;



            Write("\n\t\t[i]  - Инструкция по работе с галереей:");
            Write("\n\t\t         > Одно число - для просмотра одного узора (под этим номером)");
            Write("\n\t\t         > Два числа через '-' - для просмотра узоров под номерами от первого до второго числа");
            Write("\n\t\t         > Несколько чисел через '/' - для просмотра нескольких узоров (только под этими номерами)\n");


            ForegroundColor = ConsoleColor.DarkGray;
            Write("\n\t\t       Примеры ввода:");
            Write("\n\t\t         > 12 <        - будет показан только один узор под номером 12");
            Write("\n\t\t         > 3-8 <       - будут показаны узоры под номерами 3, 4, 5, 6, 7 и 8");
            Write("\n\t\t         > 15/2/7/1 <  - будут показаны узоры под номерами 15, 2, 7 и 1 (в таком же порядке)\n");
            ForegroundColor = ConsoleColor.White;


            Write("\n\t\tНадеюсь вам поможет моя программа, удачи! ");
            ReadKey();
        }
        //  Write the info about the program



        static public void PrintLogo()
        {
            Write("\t\t");
            BackgroundColor = ConsoleColor.DarkBlue;
            for (Int32 i = 0; i < 124; i++) Write(" ");
            BackgroundColor = ConsoleColor.Black;
            Write("\n"); //1 Empty

            /////////////////////////////////2
            for (Int32 j = 0; j < 1; j++)
            {
                BackgroundColor = ConsoleColor.Black;
                Write("\t\t");
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("   ");
                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("       ");//G
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("       ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("        ");//E
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("  ");//N
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("   ");
                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("  ");
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("        ");//E
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("      ");//R
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("        ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("       ");//A
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("       ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("          ");//T
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("      ");//I
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("       ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("      ");//O
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("   ");
                BackgroundColor = ConsoleColor.Black;
                Write("\n");
            }

            /////////////////////////////////3
            for (Int32 j = 0; j < 1; j++)
            {
                BackgroundColor = ConsoleColor.Black;
                Write("\t\t");
                BackgroundColor = ConsoleColor.Blue;
                Write("  ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//G
                BackgroundColor = ConsoleColor.Blue;
                Write("             ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//E
                BackgroundColor = ConsoleColor.Blue;
                Write("            ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("   ");//N
                BackgroundColor = ConsoleColor.Blue;
                Write("  ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");
                BackgroundColor = ConsoleColor.Blue;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//E
                BackgroundColor = ConsoleColor.Blue;
                Write("            ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//R
                BackgroundColor = ConsoleColor.Blue;
                Write("   ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");
                BackgroundColor = ConsoleColor.Blue;
                Write("      ");


                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("   ");//A
                BackgroundColor = ConsoleColor.Blue;
                Write("   ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("   ");
                BackgroundColor = ConsoleColor.Blue;
                Write("          ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//T
                BackgroundColor = ConsoleColor.Blue;
                Write("            ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//I
                BackgroundColor = ConsoleColor.Blue;
                Write("        ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//O
                BackgroundColor = ConsoleColor.Blue;
                Write("    ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");
                BackgroundColor = ConsoleColor.Blue;
                Write("  ");
                BackgroundColor = ConsoleColor.Black;
                Write("\n");
            }

            /////////////////////////////////4
            for (Int32 j = 0; j < 1; j++)
            {
                BackgroundColor = ConsoleColor.Black;
                Write("\t\t");
                BackgroundColor = ConsoleColor.Blue;
                Write("  ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//G
                BackgroundColor = ConsoleColor.Blue;
                Write("   ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("    ");
                BackgroundColor = ConsoleColor.Blue;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("     ");//E
                BackgroundColor = ConsoleColor.Blue;
                Write("         ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("    ");//N
                BackgroundColor = ConsoleColor.Blue;
                Write(" ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");
                BackgroundColor = ConsoleColor.Blue;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("     ");//E
                BackgroundColor = ConsoleColor.Blue;
                Write("         ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("      ");//R
                BackgroundColor = ConsoleColor.Blue;
                Write("       ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//A
                BackgroundColor = ConsoleColor.Blue;
                Write("     ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");
                BackgroundColor = ConsoleColor.Blue;
                Write("          ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//T
                BackgroundColor = ConsoleColor.Blue;
                Write("            ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//I
                BackgroundColor = ConsoleColor.Blue;
                Write("        ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//O
                BackgroundColor = ConsoleColor.Blue;
                Write("    ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//O
                BackgroundColor = ConsoleColor.Blue;
                Write("  ");
                BackgroundColor = ConsoleColor.Black;
                Write("\n");
            }

            /////////////////////////////////5
            for (Int32 j = 0; j < 1; j++)
            {
                BackgroundColor = ConsoleColor.Black;
                Write("\t\t");
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("  ");
                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//G
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("     ");
                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//E
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("            ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//N
                BackgroundColor = ConsoleColor.DarkCyan;
                Write(" ");
                BackgroundColor = ConsoleColor.DarkRed;
                Write("    ");
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//E
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("            ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//R
                BackgroundColor = ConsoleColor.DarkCyan;
                Write(" ");
                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("        ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("         ");//A
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("          ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//T
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("            ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//I
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("        ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//O
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("    ");
                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//O
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("  ");
                BackgroundColor = ConsoleColor.Black;
                Write("\n");
            }

            /////////////////////////////////6
            for (Int32 j = 0; j < 1; j++)
            {
                BackgroundColor = ConsoleColor.Black;
                Write("\t\t");
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("   ");
                BackgroundColor = ConsoleColor.DarkRed;
                Write("       ");//G
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("       ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("        ");//E
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//N
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("  ");
                BackgroundColor = ConsoleColor.DarkRed;
                Write("   ");
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("        ");//E
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//R
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("  ");
                BackgroundColor = ConsoleColor.DarkRed;
                Write("   ");
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//A
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("     ");
                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("          ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//T
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("          ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("      ");//I
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("       ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("      ");//O
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("   ");
                BackgroundColor = ConsoleColor.Black;
                Write("\n");
            }

            BackgroundColor = ConsoleColor.Black;
            Write("\t\t");
            BackgroundColor = ConsoleColor.Cyan;
            for (Int32 i = 0; i < 124; i++) Write(" ");
            BackgroundColor = ConsoleColor.Black;
            Write("\n\n\n"); //7 Empty
        }
        //  Logo is stored here, consists of: 6 layers + 2 empty (8 total)
    }
}