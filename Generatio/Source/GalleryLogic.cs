using System;
using System.Linq;
using System.Collections.Generic;

using static System.Console;


using static Generatio.UI;
using static Generatio.PatternSource;
using static Generatio.GlobalVariables;
using static Generatio.GlobalSettings;
using static Generatio.CustomFunctions;
using static Generatio.GalleryEncodings;


using GyroscopicDataLibrary;
using CompactDateTimeLibrary;
using static CompactDateTimeLibrary.CompactType;



namespace Generatio
{
    internal class GalleryLogic
    {
        public class GalleryPattern
        {
            private readonly Pattern pattern;
            private readonly Byte _patternType;
            private readonly UInt16 _width, _height;

            private readonly UInt16 _colAmount;
            private readonly Byte[] _colors;

            private readonly CompactDateTime _datetime;  //  Date and time of the pattern creation
            private readonly string _patternName;



            public GalleryPattern(Byte patternType,
                UInt16 width, UInt16 height,
                UInt16 colAmount, Byte[] colors,
                CompactDateTime datetime,
                string patternName)
            {
                switch(patternType)
                {
                    case 1: 
                        pattern = new PatternType1(width, height, ConvertColorsToConsole(colors), patternName, true, true);
                        break;
                    case 2:
                        pattern = new PatternType2(width, height, ConvertColorsToConsole(colors), patternName, true, true);
                        break;
                    case 3:
                        pattern = new PatternType3(width, height, ConvertColorsToConsole(colors), patternName, true, true);
                        break;
                    case 4:
                        pattern = new PatternType4(width, height, ConvertColorsToConsole(colors), patternName, true, true);
                        break;
                    case 5:
                        pattern = new PatternType5(width, height, ConvertColorsToConsole(colors), patternName, true, true);
                        break;
                    case 6:
                        pattern = new PatternType6(width, height, ConvertColorsToConsole(colors), patternName, true, true);
                        break;
                    case 7:
                        pattern = new PatternType7(width, height, ConvertColorsToConsole(colors), patternName, true, true);
                        break;
                    case 8:
                        pattern = new PatternType8(width, height, ConvertColorsToConsole(colors), patternName, true, true);
                        break;
                    case 9:
                        pattern = new PatternType9(width, height, ConvertColorsToConsole(colors), patternName, true, true);
                        break;
                    default:
                        pattern = new PatternType10(width, height, ConvertColorsToConsole(colors), patternName, true, true);
                        break;
                }


                _patternType = patternType;
                _width = width;
                _height = height;

                _colAmount = colAmount;
                _colors = colors;

                _datetime = datetime;
                _patternName = patternName;
            }
            public GalleryPattern(Byte patternType,
                UInt16 width, UInt16 height,
                Byte[] packedcolors, UInt16 unpackedAmount,
                CompactDateTime datetime,
                string patternName)
            {
                _patternType = patternType;
                _width = width;
                _height = height;

                _colAmount = unpackedAmount;
                _colors = UnpackColors(packedcolors, unpackedAmount);

                _datetime = datetime;
                _patternName = patternName;
            }


            public void Draw() => pattern.Draw();



            public Byte[] PackedColorBytes => PackColors();
            private Byte[] PackColors()
            {
                Int32 packedLength = (_colors.Length + 1) >> 1;
                Byte[] packed = new Byte[packedLength];

                for (Int32 i = 0; i < _colors.Length; i++)
                {
                    Int32 packedIndex = i >> 1;
                    Byte color = (Byte)(_colors[i] & 0x0F); //  Gives 0-15 as a result

                    if (i % 2 == 0) packed[packedIndex] = (Byte)(color << 4);  // Upper 4 bits
                    else packed[packedIndex] |= color;      //  Lower 4 bits
                }

                return packed;
            }
            private Byte[] UnpackColors(Byte[] packed, UInt16 unpackedLength)
            {
                Byte[] colors = new Byte[unpackedLength];

                for (int i = 0; i < packed.Length * 2 && i < unpackedLength; i += 2)
                {
                    colors[i] = (Byte)((packed[i] >> 4) & 0x0F);  //  Upper 4 bits
                    if (i + 1 < unpackedLength) colors[i + 1] = (Byte)(packed[i] & 0x0F);  //  Lower 4 bits
                }

                return colors;
            }




            public Byte PatternType => _patternType;
            public UInt16 Width => _width;
            public UInt16 Height => _height;



            public UInt16 ColAmount => _colAmount;
            public Byte[] ColorsBytes => _colors;
            public ConsoleColor[] ColorsConsole => ConvertColorsToConsole(_colors);



            public string StandardTimeString  => _datetime.CurrentDatetimeToStandardString();
            public CompactDateTime Datetime   => _datetime;
            public UInt32 CompactUnixDatetime => _datetime.PassedTotalMinutes;



            public string PatternName => _patternName;

        }
        public class UserInfo
        {
            private readonly string _deviceName;
            private readonly string _userName;


            public UserInfo(string deviceName = "Unknown", string userName = "Unknown")
            {
                _deviceName = deviceName;
                _userName = userName;
            }


            public string DeviceName => _deviceName;
            public string Username => _userName;
        }


        static public List<List<GalleryPattern>> GalleryBuffer = new List<List<GalleryPattern>>();
        static public List<List<GalleryPattern>> Gallery = new List<List<GalleryPattern>>();
        static public List<UserInfo> Users = new List<UserInfo>();


        static public Byte[] PackColors(Byte[] colors)
        {
            Int32 packedLength = (colors.Length + 1) >> 1;
            Byte[] packed = new Byte[packedLength];

            for (Int32 i = 0; i < colors.Length; i++)
            {
                Int32 packedIndex = i >> 1;
                Byte color = (Byte)(colors[i] & 0x0F); //  Gives 0-15 as a result

                if (i % 2 == 0) packed[packedIndex] = (Byte)(color << 4);  // Upper 4 bits
                else packed[packedIndex] |= color;      //  Lower 4 bits
            }

            return packed;
        }
        static public List<Byte> PackColors(List<Byte> colors)
        {
            List<Byte> packed = new List<Byte>();
            for (Int32 i = 0; i < colors.Count; i++)
            {
                Byte color = (Byte)(colors[i] & 0x0F); //  Gives 0-15 as a result

                if (i % 2 == 0) packed.Add((Byte)(color << 4));  // Upper 4 bits
                else packed[i >> 1] |= color;      //  Lower 4 bits
            }

            return packed;
        }
        static public Byte[] UnpackColors(Byte[] packed, UInt16 unpackedLength)
        {
            Byte[] colors = new Byte[unpackedLength];

            for (int i = 0; i < packed.Length && i * 2 < unpackedLength; i++)
            {
                colors[i * 2] = (Byte)((packed[i] >> 4) & 0x0F);  //  Upper 4 bits
                if (i * 2 + 1 < unpackedLength) colors[i * 2 + 1] = (Byte)(packed[i] & 0x0F);  //  Lower 4 bits
            }

            return colors;
        }
        static public List<Byte> UnpackColors(List<Byte> packed, UInt16 unpackedLength)
        {
            List<Byte> colors = new List<Byte>();

            for (int i = 0; i < packed.Count && i < unpackedLength; i++)
            {
                colors.Add((Byte)((packed[i] >> 4) & 0x0F));  //  Upper 4 bits
                if (i * 2 + 1 < unpackedLength) colors.Add((Byte)(packed[i] & 0x0F));  //  Lower 4 bits
            }

            return colors;
        }


        
        static public void UpdateGallery()
        {
            DecodeV3(gGalleryPath);
        }

        static public List<Int32> ParseGalleryChoice(Int32 gallerySize, out bool choiceIsExit)
        {
            string userInput;

            //  Final list of all parsed patterns
            List<Int32> parsedId = new List<Int32>();
            
            //  Temporary buffer for the help of parsing the user input
            Int32[] validId = new Int32[2];
            Int32 ignoreThisId = 0;


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
                    for (Int32 i = 0; i < rawId.Length; i++)
                    {
                        if (Int32.TryParse(rawId[i], out validId[0]))
                        {
                            //  If we found an interval
                            if (i + 1 < rawId.Length && splitters[i] == '-' &&
                                Int32.TryParse(rawId[i + 1], out validId[1]))
                            {
                                //----------  Limit the parsed intervals  ----------//
                                validId[0] = Math.Min(validId[0], gallerySize);  //
                                validId[0] = Math.Max(validId[0], 1);             //

                                validId[1] = Math.Min(validId[1], gallerySize);  //
                                validId[1] = Math.Max(validId[1], 1);             //


                                //  Print success message
                                if (gAdvInfo) Write("\n\t\tРаспознан интервал с " + validId[0] + " по " + validId[1]);


                                //  Check for a normal interval (from lower to bigger)
                                for (Int32 j = 0; j <= validId[1] - validId[0]; j++)
                                {
                                    //  Add normal interval
                                    if (validId[0] + j != ignoreThisId) parsedId.Add(validId[0] + j - 1);
                                }


                                //  Check for an inverted interval (from bigger to lower)
                                for (Int32 j = 0; j <= validId[0] - validId[1]; j++)
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
        static public void NavigateGallery()
        {
            bool exitFlag = false;
            List<Int32> choice;

            Int32 userCount = Users.Count, patternsCount = 0;
            for (Int32 userId = 0; userId < userCount; userId++) patternsCount += Gallery[userId].Count;

            WriteGalleryInfo(patternsCount, userCount);

            //  While the user doesnt want to exit, show him the gallery
            while (!exitFlag)
            {
                //  Get the user pattern choice
                choice = ParseGalleryChoice(patternsCount, out exitFlag);

                //  If the input is not (invalid or exit)
                if (choice != null)
                {
                    //  Print every chosen pattern
                    for (Int32 id = 0; id < choice.Count; id++)
                    {
                        Write("\n\n");

                        Int32 chosenUserNum = 0;
                        for (int userId = 0; userId < userCount &&
                            choice[id] >= Gallery[userId].Count; userId++)
                        {
                            choice[id] -= Gallery[userId].Count;
                            chosenUserNum++;
                        }

                        if (choice[id] < Gallery[chosenUserNum].Count) Gallery[chosenUserNum][choice[id]].Draw();
                        else Write("\n\n\t\t[!]  - Указанный узор не найден в галерее");
                    }
                }
            }
        }
        // For navigating the stored patterns (gallery)


        static public void WriteGalleryInfo(Int32 patternsCount, Int32 userCount)
        {
            ResetUI(!gIgnoreFullScreen, true);

            Write("\n\t\t\t\t\t\tВыбрано: --- === Просмотр галереи === ---\n\n\n");

            ForegroundColor = ConsoleColor.DarkCyan;
            Write("\n\t\tДобро пожаловать в галерею!\n");
            ForegroundColor = ConsoleColor.White;

            Write("\n\t\tЗдесь хранятся ");
            ForegroundColor = ConsoleColor.Yellow;
            Write("сохранённые узоры, ");
            ForegroundColor = ConsoleColor.White;
            Write("а так же их параметры.");


            Write("\n\t\tНа данный момент узоров в галерее: ");
            if (patternsCount > 1000) ForegroundColor = ConsoleColor.Green;
            else if (patternsCount > 500) ForegroundColor = ConsoleColor.DarkCyan;
            else if (patternsCount > 250) ForegroundColor = ConsoleColor.DarkYellow;
            else if (patternsCount > 100) ForegroundColor = ConsoleColor.DarkMagenta;
            else ForegroundColor = ConsoleColor.Red;

            Write(patternsCount + "\n");
            ForegroundColor = ConsoleColor.White;

            if (patternsCount > 0)
            {
                Write("\n\t\tДля просмотра узоров введите:");
                Write("\n\t\t   > Одно число - для просмотра одного узора (под этим номером)");
                Write("\n\t\t   > Два числа через '-' - для просмотра узоров под номерами от первого до второго числа");
                Write("\n\t\t   > Несколько чисел через '/' - для просмотра нескольких узоров (только под этими номерами)\n");

                Write("\n\n\n\t\t[i]  -   Индексы узоров:  автор коллекции:");


                Int32 alreadyCountedPatterns = 1;
                List<string> userNameDuplicates = new List<string>();

                for (Int32 userId = 0; userId < userCount; userId++)
                {
                    for (Int32 searchId = userId + 1; searchId < userCount; searchId++)
                    {
                        if (Users[userId].Username == Users[searchId].Username)
                        {
                            userNameDuplicates.Add(Users[userId].Username);
                            searchId += userCount;
                        }
                    }
                }

                for (Int32 userId = 0; userId < userCount; userId++)
                {
                    if (Gallery[userId].Count > 0)
                    {
                        Write("\n\t\t         > ");
                        Write(alreadyCountedPatterns + "-" + (Gallery[userId].Count + alreadyCountedPatterns - 1) + " <");

                        //  Calculate margin for the gallery output
                        Int32 infoMargin = (alreadyCountedPatterns.ToString() + (Gallery[userId].Count + alreadyCountedPatterns - 1).ToString()).ToString().Length;
                        for (int margin = 0; margin < 15 - infoMargin; margin++) Write(" ");
                        Write(" - " + Users[userId].Username);

                        if (userNameDuplicates.Contains(Users[userId].Username))
                        {
                            ForegroundColor = ConsoleColor.DarkGray;
                            Write(" [" + Users[userId].DeviceName + "]");
                            ForegroundColor = ConsoleColor.White;
                        }
                        alreadyCountedPatterns += Gallery[userId].Count;
                    }
                }
            }
            else
            {
                Write("\n\n\t\t[!]  - На данный момент галерея пуста   :(\n");
                Write("\n\t\t       Не забывайте сохранять свои узоры - чтобы они появлялись здесь");
                Write("\n\t\t[i]  - Совет: проверьте правильность указанного пути к сохранённым узорам\n\n");
            }

            Write("\n\t\t         > 0 <            - Выход из галереи\n");
        }
        

        static public void DrawPatterns(List<Pattern> patterns, List<Byte> bestIds, List<Byte> remainingIds)
        {
            Int32 randomId;
            UInt16 sizeX = patterns[0].Width, sizeY = patterns[0].Height;

            if (!gIgnoreFullScreen) ForceFullScreen(2 * sizeX, sizeY);
            Write("\n\n\n\t\t\t\t\tВот лучшие сгенерированые узоры:\n\n");

            while (bestIds.Count > 0)
            { 
                randomId = gRandom.Next(0, bestIds.Count);
                Write("\n\n");
                patterns[bestIds[randomId] - 1].Draw();
                bestIds.RemoveAt(randomId);
            }

            if (gAlwaysDrawAll || Continue())
            {   // Asking if the user wants to Draw more patters

                if (!gIgnoreFullScreen) ForceFullScreen(2 * sizeX, sizeY);
                while (remainingIds.Count > 0)
                {
                    randomId = gRandom.Next(0, remainingIds.Count);
                    Write("\n\n");
                    patterns[remainingIds[randomId] - 1].Draw();
                    remainingIds.RemoveAt(randomId);
                }
            }
        }
    }
}