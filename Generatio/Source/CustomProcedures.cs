using CompactDateTimeLibrary;
using GyroscopicDataLibrary;
using System;
using System.Collections.Generic;
using static Generatio.CustomFunctions;
using static Generatio.GalleryLogic;
using static Generatio.GlobalSettings;
using static Generatio.GlobalVariables;
using static Generatio.PatternSource;
using static Generatio.UI;
using static System.Console;



namespace Generatio
{
    internal class CustomProcedures
    {
        static public void EncodePattern(short type, Int32 X, Int32 Y, Int32 colAmount,
            ConsoleColor[] colors, string patternName = "",
            bool savePattern = false, string fileName = "", string path = "",
            bool showInfo = false)
        {
            //  Convert parameters into short string codes
            //string patternCode = type + "/" + X + "/" + Y + "/" + colAmount + "/";
            string formatedCode = type + "," + X + "," + Y + "," + colAmount + ",";


            //  Get a name for an unnamed pattern
            if (patternName == "Unnamed" || patternName == "")
            {
                //  Get current date and time
                //
                //  Overwrite the pattern name
                //  Save the date and time to the pattern name
                patternName = $"{DateTime.Now:dd.MM.yyyy HH:mm}";


                //  Save the computer name, and the user name (currently logged in) to the pattern name
                patternName += " " + Environment.MachineName + " " + Environment.UserName;


                //  Replace banned character with a similar non-banned version
                //  ( the , symbol is used in the parsing of the file data )
                patternName = patternName.Replace(",", ".");
            }


            //  Get a file name for the patterns
            if (fileName == "") fileName = Environment.UserName + ".patterns";


            //  Convert the colors to a byte array for a string code transformation
            byte[] colorBytes = ConvertColorsToBytes(colors);


            //  Convert the colors to string codes
            //patternCode += string.Join("-", colorBytes);
            formatedCode += string.Join(",", colorBytes);


            //  If dev mode is on - show the pattern info
            if (showInfo)
            {
                Write("\t\t[i]  - Закодированная информация об узоре:\n");
                Write("\n\t\t          " + patternName);
                Write("\n\t\t          " + formatedCode + "\n");

                ForegroundColor = ConsoleColor.DarkGray;
                Write("\n\t\t          > [1] <  -  Название узора");
                Write("\n\t\t          > [2] <  -  Тип узора, Размеры (Ширина, Высота), Кол-во цветов");
                Write("\n\t\t          > [3] <  -  Используемые цвета (в байтах)\n\n\n");
                ForegroundColor = ConsoleColor.White;

                //Write("\n\t\t       Быстрая команда: " + patternCode + "\n\n");
            }

            if (savePattern)
            { 
                List<string> patternData = new List<string>
                {
                    //  Add a special character for the ParseData function
                    //  So it doesn't remove spaces from the name string
                    "*" + patternName,

                    //  Add parameters + colors
                    formatedCode + "\n",
                };

                if (path == "") path = gGalleryPath;
                BetterDataIO.SaveData(path, fileName, patternData, true, "\n", showInfo, false, "\t\t");
                Write("\n\n");
            }
        }
             //  Writing dev info and/or saving the pattern
             //  Write info for developers (usefull for changing the gallery parameters)


        //------------------  Pattern related procedures  ----------------------------------------//


        static public bool CreatePatternsLogic()
        {
            UInt16 colAmount;
            ConsoleColor[] colors;
            UInt16 width = 0, height = 0;

            while (height == 0)
            {
                width = GetSize("ширин");
                if (width == 0) return true;  //  Option: leave - Exit the pattern creation

                height = GetSize("высот", width);
            }

            if (ChooseColorType(width, height))
            {   //  User color choice
                colAmount = GetColorsAmount(Math.Min(Math.Min(width, height), (UInt16)65535));
                colors = ConvertColorsToConsole(GetCustomColors(colAmount));
            }
            else
            {   //  Asset colors choice
                colors = StoredColors[GetColorAssetID(width, height)];
                colAmount = (UInt16)colors.Length;
            }

            List<Pattern> patterns = new List<Pattern>()
            {
                new PatternType1 (width, height, colors, "1",  true, true),
                new PatternType2 (width, height, colors, "2",  true, true),
                new PatternType3 (width, height, colors, "3",  true, true),
                new PatternType4 (width, height, colors, "4",  true, true),
                new PatternType5 (width, height, colors, "5",  true, true),
                new PatternType6 (width, height, colors, "6",  true, true),
                new PatternType7 (width, height, colors, "7",  true, true),
                new PatternType8 (width, height, colors, "8",  true, true),
                new PatternType9 (width, height, colors, "9",  true, true),
                new PatternType10(width, height, colors, "10", true, true)
            };
            RankPatterns(colAmount, out List<Byte> best, out List<Byte> remaining);
            DrawPatterns(patterns, best, remaining);

            UInt32 compactUnixDateTime = CompactType.CompactDateTime.Now.PassedTotalMinutes;
            if (!gAutoSave)
            {
                List<byte> selected = new List<byte>();

                Write("\n\t\t[?]  - Какие узоры вы хотели бы сохранить в галерею?");
                Write("\n\t\t[i]  - Введите номера узоров через ','");
                Write("\n\t\t       Если хотите добавить узору название напишите: номерузора = название\n");
                Write("\n\t\t> 0 <   - Вернуться в меню\n");

                while (selected.Count > 0)
                {
                    Write("\n\t\t[->]  - Ваш выбор: ");
                    selected = SelectPatternsForSaving(out List<string> names);

                    for (Int32 i = 0; i < selected.Count; i++) patterns[i].Save(compactUnixDateTime);
                }
            }
            else for (Int32 i = 0; i < patterns.Count; i++) patterns[i].Save(compactUnixDateTime);

            Write("\n\n\n\t\tГенерация узоров завершена.\n\n\n\n\n");
            return false;  //  false = dont auto continue (dont erase generated patterns as of now)
        }
             //  All the logic for generating a user-prompted pattern
    }
}