using System;

namespace Generatio
{
    internal class GlobalVariables
    {

            //  Global random class object to not initialize it more than once
        static public Random gRandom = new Random();


            //  Track of a force exit from the pattern creation menu
        static public bool gForceExit = false;




        //------------------------  Color variables  ----------------------------------------------------------//



            // Color names (for GetColors function) to write them compactly
        static public string[] gColorNames = {
                "Красный",
                "Зелёный",
                "Синий",
                "Жёлтый",
                "Фиолетовый",
                "Голубой",

                "Тёмно-Красный",
                "Тёмно-Зелёный",
                "Тёмно-Синий",
                "Тёмно-Жёлтый",
                "Тёмно-Фиолетовый",
                "Тёмно-Голубой",

                "Белый",
                "Серый",
                "Тёмно-серый",
                "Чёрный",
        };

            // Color codes for compact and easy parsing of the user input
        static public string[] gColorKeycodes = {
                "красный",
                "кр",
                "к",

                "зеленый",
                "зел",
                "з",

                "синий",
                "син",
                "с",

                "желтый",
                "жел",
                "ж",

                "фиолетовый",
                "фиол",
                "ф",

                "голубой",
                "голуб",
                "г",

                ////////////////

                "темнокрасный",
                "ткрасный",
                "тк",

                "темнозеленый",
                "тзеленый",
                "тз",

                "темносиний",
                "тсиний",
                "тс",

                "темножелтый",
                "тжелтый",
                "тж",

                "темнофиолетовый",
                "тфиолетовый",
                "тф",

                "темноголубой",
                "тголубой",
                "тг",

                ///////////////
                
                "белый",
                "бел",
                "б",

                "серый",
                "сер",
                "чб",

                "темносерый",
                "тсерый",
                "тсер",

                "черный",
                "чер",
                "ч"
        };



            // Storing all the possible console colors for an easy access through this array
            // (instead of a large switch-case statement everywhere)
        readonly static public ConsoleColor[] gAllColors = {
                ConsoleColor.Red,
                ConsoleColor.Green,
                ConsoleColor.Blue,
                ConsoleColor.Yellow,
                ConsoleColor.Magenta,
                ConsoleColor.Cyan,

                ConsoleColor.DarkRed,
                ConsoleColor.DarkGreen,
                ConsoleColor.DarkBlue,
                ConsoleColor.DarkYellow,
                ConsoleColor.DarkMagenta,
                ConsoleColor.DarkCyan,

                ConsoleColor.White,
                ConsoleColor.Gray,
                ConsoleColor.DarkGray,
                ConsoleColor.Black
        };



        //------------------  Stored color assets for a fast generation  --------------------------------------//

            //  sc  -  stored colors
            //  2, 4, 5, 7, 8, 12 - amount of colors in the array


        

        readonly static public ConsoleColor[] scWhiteBlack4 = {
                ConsoleColor.White,
                ConsoleColor.Gray,
                ConsoleColor.DarkGray,
                ConsoleColor.Black
        };

        readonly static public ConsoleColor[] scBlue4 = {
                ConsoleColor.Cyan,
                ConsoleColor.DarkCyan,
                ConsoleColor.Blue,
                ConsoleColor.DarkBlue,
        };

        readonly static public ConsoleColor[] scRedBlue4 = {
                ConsoleColor.Red,
                ConsoleColor.DarkRed,
                ConsoleColor.DarkBlue,
                ConsoleColor.Blue
        };

        readonly static public ConsoleColor[] scMagentaRed4 = {

                ConsoleColor.Magenta,
                ConsoleColor.DarkMagenta,
                ConsoleColor.DarkRed,
                ConsoleColor.Red
        };

        readonly static public ConsoleColor[] scBlueWhite5 = {
                ConsoleColor.DarkBlue,
                ConsoleColor.Blue,
                ConsoleColor.DarkCyan,
                ConsoleColor.Cyan,
                ConsoleColor.White
        };

        readonly static public ConsoleColor[] scBlueMagentaGreen7 = {
                ConsoleColor.DarkCyan,
                ConsoleColor.Blue,
                ConsoleColor.Magenta,
                ConsoleColor.Green,
                ConsoleColor.DarkGreen,
                ConsoleColor.DarkMagenta,
                ConsoleColor.DarkBlue,

        };

        readonly static public ConsoleColor[] scRedBlueGreenMagenta8 = {
                ConsoleColor.DarkRed,
                ConsoleColor.Red,
                ConsoleColor.DarkBlue,
                ConsoleColor.Blue,
                ConsoleColor.Green,
                ConsoleColor.DarkGreen,
                ConsoleColor.DarkMagenta,
                ConsoleColor.Magenta

        };

        readonly static public ConsoleColor[] scRedBlue8 = {
                ConsoleColor.DarkMagenta,
                ConsoleColor.Magenta,
                ConsoleColor.Red,
                ConsoleColor.DarkRed,

                ConsoleColor.DarkBlue,
                ConsoleColor.Blue,
                ConsoleColor.DarkCyan,
                ConsoleColor.Cyan
        };

        readonly static public ConsoleColor[] scMix12 = {
                ConsoleColor.Cyan,
                ConsoleColor.DarkCyan,
                ConsoleColor.Blue,
                ConsoleColor.DarkBlue,

                ConsoleColor.DarkMagenta,
                ConsoleColor.Magenta,
                ConsoleColor.DarkRed,
                ConsoleColor.Red,

                ConsoleColor.DarkYellow,
                ConsoleColor.Yellow,
                ConsoleColor.Green,
                ConsoleColor.DarkGreen,
        };

        readonly static public ConsoleColor[][] StoredColors = {
            scWhiteBlack4,
            scBlue4,
            scRedBlue4,
            scMagentaRed4,
            scBlueWhite5,
            scBlueMagentaGreen7,
            scRedBlueGreenMagenta8,
            scRedBlue8,
            scMix12,
        };

    }
}