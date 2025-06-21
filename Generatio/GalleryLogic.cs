using System;
using System.Collections.Generic;

using static System.Console;


using static Generatio.PatternSource;
using static Generatio.GlobalSettings;
using static Generatio.CustomFunctions;
using static Generatio.CustomProcedures;

using GyroscopicDataLibrary;
using CompactDateTimeLibrary;


namespace Generatio
{
    internal class GalleryLogic
    {
        public class GalleryPattern
        {
            //-----------------------   Variables  ----------------------------------//


            //  Pattern number index or type 
            private readonly byte patternType;

            //  Dimensions for the pattern
            private readonly UInt16 width;
            private readonly UInt16 height;

            //  Amount of colors
            static private UInt16 colAmount;

            //  Array of colors (in bytes))
            private readonly byte[] colors = new byte[colAmount];

            //  Name of the current pattern (origin/date/time/author/etc)
            private readonly string name;

            //-----------------------  Functions  ----------------------------------//



            public GalleryPattern(byte iPatternType,
                UInt16 iWidth, UInt16 iHeight,
                UInt16 iColAmount, byte[] iColors,
                string iName = "Unnamed")
            {
                // i - input

                patternType = iPatternType;
                width = iWidth;
                height = iHeight;
                colAmount = iColAmount;
                colors = iColors;
                name = iName;
            }
                 //  Constructor

            public void Generate()
            {
                //  All posible pattern types dictionary
                var patterns = new Dictionary<byte, Action>()  {
                    { 1,  () => Pattern1 (width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                    { 2,  () => Pattern2 (width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                    { 3,  () => Pattern3 (width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                    { 4,  () => Pattern4 (width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                    { 5,  () => Pattern5 (width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                    { 6,  () => Pattern6 (width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                    { 7,  () => Pattern7 (width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                    { 8,  () => Pattern8 (width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                    { 9,  () => Pattern9 (width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                    { 10, () => Pattern10(width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                };

                //  Generete the chosen pattern from the dictionary
                patterns[patternType]();
            }
            //  Universal pattern generation by code

            public byte GetPatternType() { return patternType; }
                 //  Returns the pattern type

            public UInt16 GetWidth() { return width; }
                 //  Returns the pattern width

            public UInt16 GetHeight() { return height; }
                 //  Returns the pattern height

            public UInt16 GetColAmount() { return colAmount; }
                 //  Returns the amount of colors in the pattern 

            public byte[] GetColorsBytes() { return colors; }
                 //  Returns the colors in bytes[]

            public ConsoleColor[] GetColorsConsole() { return ConvertColorsToConsole(colors); }
                 //  Returns the colors in ConsoleColor[]

            public string GetFullPatternName() { return name; }

            /*public string GetUsername()
            {
                string username = "Unnamed";

                int id = name.LastIndexOf(" ");

                if ()
            }*/
        }
        public class GalleryName
        {
            public string patternName;
            public string userName;
            public CompactType.CompactDateTime datetime;
        }

        static public List<GalleryPattern> StockGallery  = new List<GalleryPattern>();
        static public List<GalleryPattern> UserGallery   = new List<GalleryPattern>();
        static public List<GalleryPattern> GalleryBuffer = new List<GalleryPattern>();

        static public List<List<GalleryPattern>> UsersGallery = new List<List<GalleryPattern>>();
        static public List<string> Users = new List<string>();



        static public List<GalleryPattern> ConvertToGalleryPattern(List<string> data, bool showInfo = false)
        {
            if (data != null && data.Count > 1)
            {
                //  Storing here the successfully converted gallery patterns
                List<GalleryPattern> converted = new List<GalleryPattern>();

                string[] parserHelper;
                List<byte> colors = new List<byte>();

                string name = "Unnamed";
                byte patternType;
                UInt16 width;
                UInt16 height;
                UInt16 expectedColAmount;

                for (int i = 0; i < data.Count; i++)
                {
                    //  If the line is expected to have the pattern name
                    if (data[i].IndexOf(",") == -1)
                    {
                        name = data[i].Trim();

                        if (showInfo)
                        {
                            //  Show the name of the pattern
                            Write("\n\t\tРаспознанно название узора: " + name + "\n");
                        }
                    }

                    //  If the line is expected to have pattern data
                    else
                    {
                        //  Transform the data into more easily usable
                        parserHelper = SplitForParsing(data[i], ",");

                        //  If the data is at lest a full pattern with 2+ colors
                        if (parserHelper.Length > 5)
                        {
                            //  Try parse the pattern type
                            if (byte.TryParse(parserHelper[0], out patternType)
                                && patternType > 0 && patternType < 11)
                            {
                                //  Try parse the pattern size
                                if (UInt16.TryParse(parserHelper[1].Trim(), out width)
                                    && UInt16.TryParse(parserHelper[2].Trim(), out height)
                                    && width > 0 && height > 0)
                                {
                                    //  Try parse the amount of colors (2+ not 0 and not 1)
                                    if (UInt16.TryParse(parserHelper[3].Trim(), out expectedColAmount)
                                        && expectedColAmount > 1)
                                    {
                                        //  Show the parsed base parameters
                                        if (showInfo)
                                        {
                                            Write("\n\t\tУспешно распознанны базовые параметры!");
                                            Write("\n\t\tТип узора: " + patternType);
                                            Write("\n\t\tРазмеры узора: " + width + "x" + height);
                                            Write("\n\t\tКоличество цветов: " + expectedColAmount + "\n");
                                        }

                                        //  Try parse the colors
                                        for (int j = 4; j < 4 + expectedColAmount; j++)
                                        {
                                            //  If the color is out of bounds - break from loop
                                            if (j >= parserHelper.Length) j += expectedColAmount;
                                            else
                                            {
                                                //  Try parse the color
                                                if (byte.TryParse(parserHelper[j], out byte holdColor))
                                                {
                                                    //  If the color is valid - add it to the list
                                                    colors.Add(holdColor);
                                                }
                                                else if (showInfo)
                                                {
                                                    //  If the color is invalid - output error
                                                    Write("\n\t\tНе удалось распознать цвет: " + parserHelper[j] + "\n");
                                                }
                                            }
                                        }

                                        //  Print the parsed colors
                                        if (showInfo)
                                        {
                                            if (colors.Count > 0)
                                            {
                                                Write("\n\t\tУспешно распознанны цвета: ");
                                                for (int j = 0; j < colors.Count; j++)
                                                {
                                                    //  Convert colors to a byte array
                                                    //  For the special display feature
                                                    byte[] colorBuffer = new byte[colors.Count];
                                                    for (int k = 0; k < colors.Count; k++) colorBuffer[k] = colors[k];


                                                    //  Show the successfully parsed color
                                                    ForegroundColor = ConvertColorsToConsole(colorBuffer)[j];
                                                    if (colorBuffer[j] == 15) BackgroundColor = ConsoleColor.White;
                                                    Write(colors[j]);


                                                    // Reset text highlighting
                                                    BackgroundColor = ConsoleColor.Black;
                                                    ForegroundColor = ConsoleColor.White;
                                                    Write("  ");
                                                }
                                            }
                                            else
                                            {
                                                //  If the color parsing failed - output error
                                                Write("\n\t\tНе удалось распознать цвета\n");
                                            }
                                        }


                                        //  After data parsing and conversion
                                        //  We check if we have enough data to check for its validation
                                        if (patternType > 0 && width > 0 && height > 0 && expectedColAmount > 0 && colors.Count > 0)
                                        {
                                            //  If the real (parsed) colors amount is equal to the expected one
                                            if (colors.Count == expectedColAmount)
                                            {
                                                //  Add the successfully converted pattern to the list
                                                converted.Add(new GalleryPattern(patternType, width, height, expectedColAmount, colors.ToArray(), name));

                                                if (showInfo)
                                                {
                                                    //  Highlight the success of a pattern conversion
                                                    ForegroundColor = ConsoleColor.Green;
                                                    Write("\n\t\tУспешно распознан узор!\n");
                                                    ForegroundColor = ConsoleColor.White;
                                                }
                                            }
                                            else if (showInfo)
                                            {
                                                //  Highlight pattern conversion error
                                                ForegroundColor = ConsoleColor.Red;
                                                Write("\n\t\tОшибка данных: настоящее количество цветов не совпадает с указанным");
                                                ForegroundColor = ConsoleColor.White;
                                                Write("\n\t\tУказано: " + expectedColAmount + ", обнаружено: " + colors.Count + "\n");
                                            }

                                            //  Reset the colors and name to avoid data mixing
                                            name = "Unnamed";
                                            colors.Clear();
                                        }
                                    }
                                    else if (showInfo)
                                        //  Try parse the last parameter (amount of colors)
                                        //  If failes - output error
                                        Write("\n\t\tНе удалось распознать количество цветов: " + parserHelper[3] + "\n");
                                }
                                //  Show error parsing the size if needed
                                else if (showInfo) Write("\n\t\tНе удалось распознать размеры узора: " + parserHelper[1] + "x" + parserHelper[2] + "\n");
                            }
                            //  Show error parsing the type if needed
                            else if (showInfo) Write("\n\t\tНе удалось распознать тип узора: " + parserHelper[0] + "\n");
                        }
                    }
                }

                //  Show the amount of successfully converted patterns or an error
                if (showInfo)
                {
                    if (converted.Count > 0) Write("\n\t\tУспешно обнаружено: " + converted.Count + " узоров" + "\n");
                    else Write("\n\t\tНе удалось распознать сохранённые узоры: Некорректный формат данных\n");
                }

                //  Return the successfully converted patterns
                return converted;
            }

            //  return null if the data is invalid
            if (showInfo) Write("\n\t\tНе удалось распознать сохранённые узоры: Некорректный формат данных\n");
            return null;
        }


        static public List<int> ParseGalleryChoice(int gallerySize, out bool choiceIsExit)
        {
            string userInput;

            //  Final list of all parsed patterns
            List<int> parsedId = new List<int>();
            
            //  Temporary buffer for the help of parsing the user input
            int[] validId = new int[2];
            int ignoreThisId = 0;


            //  Get the user input
            Write("\n\t\t[->] - Ваш выбор: ");
            userInput = ReadLine().Trim();


            //  Gallery exit logic
            if (userInput == "0")
            {
                choiceIsExit = true;
                return null;
            }

            try
            {
                //  Parse multiple patterns choice
                string[] rawId = SplitForParsing(userInput, "-/, ");
                string splitters  = GetSplitters(userInput, "-/, ");

                if (rawId.Length > 0)
                {
                    for (int i = 0; i < rawId.Length; i++)
                    {
                        if (int.TryParse(rawId[i], out validId[0]))
                        {
                            //  If we found an interval
                            if (i + 1 < rawId.Length && splitters[i] == '-' &&
                                int.TryParse(rawId[i + 1], out validId[1]))
                            {
                                //----------  Limit the parsed intervals  ----------//
                                validId[0] = Math.Min(validId[0], gallerySize);  //
                                validId[0] = Math.Max(validId[0], 1);             //

                                validId[1] = Math.Min(validId[1], gallerySize);  //
                                validId[1] = Math.Max(validId[1], 1);             //


                                //  Print success message
                                if (gAdvInfo) Write("\n\t\tРаспознан интервал с " + validId[0] + " по " + validId[1]);


                                //  Check for a normal interval (from lower to bigger)
                                for (int j = 0; j <= validId[1] - validId[0]; j++)
                                {
                                    //  Add normal interval
                                    if (validId[0] + j != ignoreThisId) parsedId.Add(validId[0] + j - 1);
                                }


                                //  Check for an inverted interval (from bigger to lower)
                                for (int j = 0; j <= validId[0] - validId[1]; j++)
                                {
                                    //  Add inverted interval
                                    if (validId[0] - j != ignoreThisId) parsedId.Add(validId[0] - j - 1);
                                }

                                //  Ignore the printing of the last id
                                ignoreThisId = validId[1];
                            }


                            //  If this id isnt included in any intervals
                            else
                            {
                                //  Check for inbounds of the gallery data
                                if (validId[0] > 0 && validId[0] < gallerySize + 1)
                                {
                                    if (validId[0] != ignoreThisId)
                                    {
                                        parsedId.Add(validId[0] - 1);
                                        if (gAdvInfo) Write("\n\t\tРаспознанный узор: " + validId[0]);
                                    }
                                }
                                else Write("\n\t\tИндекс вне границ базы данных: " + validId[0] + ". Повторите ввод: ");

                                ignoreThisId = 0;
                            }
                        }
                        else ignoreThisId = 0;
                    }
                }
                else Write("\n\t\t[!]  - Не удалось распознать ни один индекс. Повторите ввод: ");
            }
            catch (Exception e)
            {
                if (gShowInfo)
                { 
                    Write("\n\t\t[!]  - Фатальная ошибка при парсинге данных!");
                    Write("\n\t\t       Код ошибки: " + e);
                }
            }

            choiceIsExit = false;
            return parsedId;
        }


        static public void UpdateStockGallery()
        {
            //  Read the stock data for the gallery
            List<string> parsedData = BetterDataIO.ReadData(gGalleryPath, "1stock.patterns", gShowInfo, false, "\t\t", "\n");
            
            //  Parse the data into more easily usable
            parsedData = BetterDataIO.ParseData(parsedData, true, true, "*", "", "*", gShowInfo, false, "\t\t");

            //  Convert the data into patterns
            GalleryBuffer = ConvertToGalleryPattern(parsedData, gAdvInfo);

            if (GalleryBuffer != null)
            {
                //  Update the gallery
                StockGallery = GalleryBuffer;
            }
        }

        static public void UpdateUserGallery()
        {
            List<string> patternFiles = ToStringList(BetterDataIO.GetAllFiles(gGalleryPath, true, gShowInfo, false, "\t\t"));

            if (patternFiles != null)
            {
                //  Remove the unrelated files from the search
                for (int i = 0; i < patternFiles.Count; i++)
                {
                    //  Check if the file doesnt have the .patterns extention
                    if (patternFiles[i].Length < 9 ||
                        patternFiles[i].IndexOf(".patterns", patternFiles[i].Length - 9) == -1 ||
                        patternFiles[i].IndexOf("stock.") != -1)
                    {
                        //  Remove the unrelated file, including:
                        //  - Not .patterns files
                        //  - Stock patterns
                        patternFiles.RemoveAt(i);
                        i--;
                    }
                }

                //  Reset the previous gallery save
                UserGallery.Clear();

                //  Loop through all the pattern files
                foreach (string file in patternFiles)
                {
                    //  Read the stock data for the gallery
                    List<string> parsedData = BetterDataIO.ReadData(gGalleryPath, file, gShowInfo, false, "\t\t");

                    //  Parse the data into more easily usable
                    parsedData = BetterDataIO.ParseData(parsedData, true, true, "*", "", "*", gShowInfo, false, "\t\t");

                    //  Convert the data into patterns
                    GalleryBuffer = ConvertToGalleryPattern(parsedData, gAdvInfo);

                    //  Update the gallery
                    foreach (GalleryPattern pattern in GalleryBuffer)
                    {
                        //  Add the pattern to the gallery
                        UserGallery.Add(pattern);

                        for (int i = 0; i < Users.Count; i++)
                        {

                        }
                    }
                }
            }
        }


        /*static public void GenerateGallery()
        {
            gGeneratedGpatterns = true;

            byte[] Colors0 = { 4, 8, 10, 4, 0, 6, 0, 4, 2, 8, 2, 5, 4, 10, 6 };
            GalleryPattern item0 = new GalleryPattern(3, 60, 30, 15, Colors0);
            Gallery.Add(item0);

            byte[] Colors1 = { 10, 4, 0, 6, 8, 2, 11, 5 };
            GalleryPattern item1 = new GalleryPattern(3, 60, 30, 8, Colors1);
            Gallery.Add(item1);

            byte[] Colors2 = { 11, 8, 7, 1, 2, 5, 11, 5, 2, 1, 7, 8, 11, 4, 10 };
            StoredPatterns[2] = new GalleryPattern(3, 40, 40, 15, Colors2);

            byte[] Colors3 = { 11, 8, 10, 4, 10, 8, 11, 5 };
            StoredPatterns[3] = new GalleryPattern(7, 60, 30, 8, Colors3);

            byte[] Colors4 = { 9, 3, 1, 7, 1, 7, 1, 3, 9 };
            StoredPatterns[4] = new GalleryPattern(4, 60, 30, 9, Colors4);

            byte[] Colors5 = { 3, 9, 0, 6, 10, 4, 10, 6, 0, 9, 3 };
            StoredPatterns[5] = new GalleryPattern(1, 60, 30, 11, Colors5);

            byte[] Colors6 = { 12, 13, 15, 14, 2, 13, 14, 12, 15, 14, 13, 2, 8, 2, 13, 12 };
            StoredPatterns[6] = new GalleryPattern(9, 70, 40, 16, Colors6);

            byte[] Colors7 = { 3, 9, 0, 6, 0, 6, 0, 9, 3 };
            StoredPatterns[7] = new GalleryPattern(3, 60, 30, 9, Colors7);

            byte[] Colors8 = { 11, 8, 7, 1, 2, 5, 11, 5, 2, 1, 7, 8, 11, 4, 10 };
            StoredPatterns[8] = new GalleryPattern(3, 40, 40, 15, Colors8);

            byte[] Colors9 = { 5, 11, 2, 8, 10, 4, 6, 0, 9, 3, 1, 7 };
            StoredPatterns[9] = new GalleryPattern(2, 40, 20, 12, Colors9);

            byte[] Colors10 = { 10, 4, 8, 2, 11, 5, 7, 1, 3, 9, 0, 6, 0, 6, 0, 9, 3, 1, 7, 5, 11, 2, 8, 4, 10, 4, 10, 4 };
            StoredPatterns[10] = new GalleryPattern(7, 60, 30, 28, Colors10);

            byte[] Colors11 = { 5, 11, 2, 8, 10, 4, 6, 0, 9, 3, 1, 7 };
            StoredPatterns[11] = new GalleryPattern(1, 40, 20, 12, Colors11);

            byte[] Colors12 = { 10, 4, 8, 2, 11, 5, 7, 1, 3, 9, 0, 6, 0, 6, 0, 9, 3, 1, 7, 5, 11, 2, 8, 4, 10, 4, 10, 4 };
            StoredPatterns[12] = new GalleryPattern(1, 60, 30, 28, Colors12);

            byte[] Colors13 = { 4, 10, 6, 0, 6, 8, 2, 1, 7, 1 };
            StoredPatterns[13] = new GalleryPattern(6, 64, 32, 10, Colors13);

            byte[] Colors14 = { 1, 7, 2, 8, 4, 10, 4, 6, 0, 6, 4, 10, 4, 8, 2 };
            StoredPatterns[14] = new GalleryPattern(3, 60, 30, 15, Colors14);

            byte[] Colors15 = { 11, 8, 10, 4, 10, 8, 11, 0, 6, 0, 11, 8, 10, 4, 10, 8, 11, 5 };
            StoredPatterns[15] = new GalleryPattern(6, 30, 18, 18, Colors15);

            byte[] Colors16 = { 0, 6, 8, 2 };
            StoredPatterns[16] = new GalleryPattern(3, 46, 23, 4, Colors16);

            byte[] Colors17 = { 5, 11, 2, 8, 0, 6, 4, 10, 4, 6, 0, 8, 2, 11, 5, 11 };
            StoredPatterns[17] = new GalleryPattern(1, 38, 25, 16, Colors17);

            byte[] Colors18 = { 2, 1, 7, 1, 2, 8, 11, 8, 2, 1, 7, 1 };
            StoredPatterns[18] = new GalleryPattern(3, 46, 23, 13, Colors18);

            byte[] Colors19 = { 15, 14, 13, 12, 13, 12, 13, 14, 15, 14 };
            StoredPatterns[19] = new GalleryPattern(1, 46, 23, 10, Colors19);

            byte[] Colors20 = { 4, 10, 4, 6, 0, 2, 8, 7, 1, 11 };
            StoredPatterns[20] = new GalleryPattern(6, 72, 36, 10, Colors20);

            byte[] Colors21 = { 0, 1, 0, 1, 0, 1, 7, 6, 7, 6, 7, 6, 0, 1, 0, 1 };
            StoredPatterns[21] = new GalleryPattern(9, 70, 40, 16, Colors21);
        }*/
             // Set some basic gallery patterns so the gallery isnt empty

        static public void NavigateGallery()
        {
            //  Logic for exiting the gallery
            bool exitFlag = false;

            //  Choice of all the patterns the user wants to see
            List<int> choice;

            //  Amount of different patterns in the gallery
            int stockAmount = StockGallery.Count;
            int userAmount = UserGallery.Count;

            //  Amount of patterns present in the gallery
            int gallerySize = stockAmount + userAmount;

            WriteGalleryInfo(stockAmount, userAmount);

            //  While the user doesnt want to exit, show him the gallery
            while (!exitFlag)
            {
                //  Get the user pattern choice
                choice = ParseGalleryChoice(gallerySize, out exitFlag);

                //  If the input is not (invalid or exit)
                if (choice != null)
                {
                    //  Print every chosen pattern
                    for (int i = 0; i < choice.Count; i++)
                    {
                        Write("\n\n");
                        if (choice[i] < stockAmount) StockGallery[choice[i]].Generate();
                        else UserGallery[choice[i] - stockAmount].Generate();
                    }
                }
            }
        }
        // For navigating the stored patterns (gallery)


        static public void WriteGalleryInfo(int stockGallerySize, int userGallerySize)
        {
            //  Full gallery size
            int gallerySize = stockGallerySize + userGallerySize;

            ResetUI(!gIgnoreFullScreen, true);
            Write("\n\t\t\t\t\t\tВыбрано: --- === Просмотр галереи === ---\n\n\n");

            ForegroundColor = ConsoleColor.DarkCyan;
            Write("\n\t\tДобро пожаловать в галерею!\n");
            ForegroundColor = ConsoleColor.White;



            Write("\n\t\tЗдесь хранятся ");
            ForegroundColor = ConsoleColor.Yellow;
            Write("стоковые и пользовательские сохранённые узоры, ");
            ForegroundColor = ConsoleColor.White;
            Write("а так же их параметры.");


            Write("\n\t\tНа данный момент узоров в галереи: ");
            if (gallerySize > 100) ForegroundColor = ConsoleColor.Green;
            else ForegroundColor = ConsoleColor.Red;
            Write(gallerySize + "\n");
            ForegroundColor = ConsoleColor.White;

            if (gallerySize > 0)
            {
                Write("\n\t\tДля просмотра узоров введите:");
                Write("\n\t\t   > Одно число - для просмотра одного узора (под этим номером)");
                Write("\n\t\t   > Два числа через '-' - для просмотра узоров под номерами от первого до второго числа");
                Write("\n\t\t   > Несколько чисел через '/' - для просмотра нескольких узоров (только под этими номерами)\n");

                Write("\n\n\n\t\t[i]  - Доступный функционал на данный момент:");
            }
            else
            {
                Write("\n\n\t\t[!]  - На данный момент галерея пуста   :(\n");
                Write("\n\t\t       Не забывайте сохранять свои узоры - чтобы они появлялись здесь");
                Write("\n\t\t[i]  - Совет: проверьте правильность указанного пути к сохранённым узорам\n\n");
            }
            if (stockGallerySize > 0)
            {
                
                Write("\n\t\t         > ");
                Write(1 + "-" + stockGallerySize);

                //  Calculate margin for the gallery output
                string margin = "         ";
                int tempBuffer = stockGallerySize;
                while(tempBuffer > 0)
                {
                    if(margin.Length > 0) margin = margin.Remove(0, 1);
                    tempBuffer /= 10;
                }

                Write(" < " + margin + " - Стоковые узоры");
            }
            if (userGallerySize > 0)
            {
                Write("\n\t\t         > ");
                Write(stockGallerySize + 1 + "-" + gallerySize);

                //  Calculate margin for the gallery output
                string margin = "          ";
                int tempBuffer = stockGallerySize;
                while (tempBuffer > 0)
                {
                    if (margin.Length > 0) margin = margin.Remove(0, 1);
                    tempBuffer /= 10;
                }
                tempBuffer = stockGallerySize + userGallerySize;
                while (tempBuffer > 0)
                {
                    if (margin.Length > 0) margin = margin.Remove(0, 1);
                    tempBuffer /= 10;
                }

                Write(" < " + margin + " - Пользовательские узоры");
            }
            Write("\n\t\t         > 0 <            - Выход из галереи\n");
        }
    }
}