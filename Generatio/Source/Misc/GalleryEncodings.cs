using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using static System.Console;


using static Generatio.GalleryLogic;
using static Generatio.GlobalSettings;
using static Generatio.GlobalVariables;


using AVcontrol;
using GyroscopicDataLibrary;



namespace Generatio
{
    internal class GalleryEncodings
    {
        static public void DecodeV3(string path)
        {
            Int32 nameSplitterId, extensionId;
            UserInfo userInfo;
            bool foundAtLeastSomethingLol = false;

            List<string> allFiles = BetterDataIO.GetFilesWithExtension(path, ".patterns", true, gShowInfo, false, "\t\t");

            for (int fileId = 0; fileId < allFiles.Count; fileId++)
            {
                List<List<Byte>>? readData = ReadBinaryDataForFormatV3(path, allFiles[fileId], gShowInfo, false);

                if (readData != null)
                {
                    nameSplitterId = allFiles[fileId].IndexOf("~");
                    extensionId = allFiles[fileId].LastIndexOf(".");
                    if (nameSplitterId != -1)
                    {
                        userInfo = new UserInfo
                        (
                            allFiles[fileId].Substring(0, nameSplitterId).Replace("~", ""), //  wtf?
                            allFiles[fileId].Substring
                            (
                                nameSplitterId + 1,
                                nameSplitterId + 1
                                + (
                                    extensionId == -1 ?
                                        allFiles[fileId].Length
                                        : Math.Min(extensionId, allFiles[fileId].Length)
                                )
                            ).Replace("~", "")
                        );
                    }
                    else
                    {
                        userInfo = new UserInfo
                        (
                            "Unknown",
                            allFiles[fileId].Replace("~", "")
                        );
                    }

                    if (!foundAtLeastSomethingLol)
                    {
                        Gallery.Clear();
                        Users.Clear();
                        foundAtLeastSomethingLol = true;
                    }

                    var tryConvert = TryConversionV3(readData, gAdvInfo);
                    if (tryConvert != null) Gallery.Add(tryConvert);
                    Users.Add(userInfo);
                }
            }

            if (gShowInfo || gAdvInfo)
            {
                Write("\n\t\t[i]  - Служебный вывод завершён, нажмите любую кнопку для продолжения ");
                ReadKey();
            }
        }
        static public List<GalleryPattern>? TryConversionV3(List<List<Byte>> data, bool showInfo = false)
        {
            if (data != null && data.Count > 0)
            {
                List<GalleryPattern> converted = new List<GalleryPattern>();

                string name;
                Byte patternType;
                UInt16 width, height;
                UInt16 unpackedColAmount, packedColAmount;
                List<Byte> colors = new List<Byte>();


                for (Int32 curY = 0; curY < data.Count; curY++)
                {
                    //  Minimal size of 1 pattern data is 13 bytes
                    if (data[curY].Count > 12)
                    {
                        patternType = data[curY][0];
                        width = (UInt16)((data[curY][1] << 8) | data[curY][2]);
                        height = (UInt16)((data[curY][3] << 8) | data[curY][4]);

                        unpackedColAmount = (UInt16)((data[curY][5] << 8) | data[curY][6]);
                        packedColAmount = (UInt16)((unpackedColAmount + 1) / 2);

                        if (showInfo)
                        {
                            Write("\n\n\t\tРаспознанные параметры узора");
                            Write("\n\t\tТип: " + patternType);
                            Write("\n\t\tРазмеры: " + width + "x" + height);
                            Write("\n\t\tОжидаемое количество        всех цветов: " + unpackedColAmount);
                            Write("\n\t\tОжидаемое количество упакованных цветов: " + packedColAmount);
                            Write("\n\t\tКоличество байт информации: " + data[curY].Count);
                        }

                        //  Check for having enough data for the estimated color amount
                        if (packedColAmount + 8 <= data[curY].Count)
                        {

                            if (!showInfo)
                            {   //  Current color position
                                for (int curColPos = 7; curColPos < packedColAmount + 7; curColPos++)
                                    //  Temporary uncompresed binary encoded color handling logic
                                    colors.Add(data[curY][curColPos]);
                            }
                            else
                            {
                                Write("\n\t\tЦвета: ");

                                for (int curColPos = 7; curColPos < packedColAmount + 7; curColPos++)
                                {   //  Current color position

                                    //  Temporary uncompresed binary encoded color handling logic
                                    colors.Add(data[curY][curColPos]);

                                    //ForegroundColor = gAllColors[colors[curColPos - 7]];
                                    Write(colors[curColPos - 7] + " ");
                                }
                                ForegroundColor = ConsoleColor.White;
                            }

                            if (packedColAmount + 11 <= data[curY].Count)
                            {
                                UInt32 compactUnixPassedMinutes = (UInt32)
                                (
                                    (data[curY][packedColAmount + 7] << 24) |
                                    (data[curY][packedColAmount + 8] << 16) |
                                    (data[curY][packedColAmount + 9] << 8) |
                                    (data[curY][packedColAmount + 10])
                                );

                                Byte expectedNameLength = data[curY][packedColAmount + 11];

                                if (showInfo)
                                {
                                    DateTime4b dateTime = new DateTime4b(compactUnixPassedMinutes);

                                    Write("\n\t\tДата и время: " + dateTime.ToStringFull());
                                    Write("\n\t\tОжидаемая длина названия: " + expectedNameLength);
                                    Write("\n\t\tТребуется байт: " + (expectedNameLength + packedColAmount + 12)
                                        + ", реальное количество: " + data[curY].Count);
                                }

                                if (expectedNameLength + packedColAmount + 12 <= data[curY].Count)
                                {
                                    name = Encoding.UTF8.GetString
                                    (
                                        data[curY].ToArray(),
                                        packedColAmount + 12,
                                        expectedNameLength
                                    );

                                    colors = UnpackColors(colors, unpackedColAmount);
                                    converted.Add
                                    (
                                        new GalleryPattern
                                        (
                                            patternType,
                                            width,
                                            height,

                                            unpackedColAmount,
                                            colors.ToArray(),

                                            new DateTime4b(compactUnixPassedMinutes),
                                            name
                                        )
                                    );
                                    colors.Clear();

                                    if (showInfo) Write("\n\t\tРаспознано название узора: " + name);
                                }
                                else Write("\n\t\t[!]  - Error! Not enough data for distinguishing the patterns");
                            }
                            else Write("\n\t\t[!]  - Error! Not enough data for datetime");
                        }
                        else
                        {
                            Write("\n\t\t[!]  - Error! Expected too many colors: " + (packedColAmount + 7));
                            Write("\n\t\t       Max possible amount: " + (data[curY].Count - packedColAmount));
                        }
                    }
                }

                if (showInfo)
                {
                    if (converted.Count > 0)
                    {
                        ForegroundColor = ConsoleColor.Green;
                        Write("\n\n\t\tУспешно обнаружено: " + converted.Count + " узоров\n");
                    }
                    else
                    {
                        ForegroundColor = ConsoleColor.Red;
                        Write("\n\n\t\tНе удалось распознать сохранённые узоры: Некорректный формат данных\n");
                    }
                    ForegroundColor = ConsoleColor.White;
                }

                return converted;
            }

            //  return null if the data is invalid
            if (showInfo)
            {
                ForegroundColor = ConsoleColor.Red;
                Write("\n\t\tНе удалось распознать сохранённые узоры: Некорректный формат данных\n");
                ForegroundColor = ConsoleColor.White;
            }
            return null;
        }
        static public List<List<Byte>>? ReadBinaryDataForFormatV3(string path, string fileName,
            bool showInfo = false, bool engLang = true)
        {
            try
            {
                if (File.Exists(Path.Combine(path, fileName)))
                {
                    //  Initialize the stream to read mode
                    Stream stream = new FileStream(Path.Combine(path, fileName), FileMode.Open);
                    BinaryReader binaryDataReader = new BinaryReader(stream);

                    Int32 curY = 0;        //  Current Y position in the list list of found data
                    List<List<Byte>> foundData = new List<List<Byte>> { new List<Byte>() };

                    Int64 extendedX = 0;   //  Amount of bytes to extend the splitter length by
                    const Int64 splitAfterLength = 10;
                    Int64[] extendersFinalBytes = new long[] { 6, 11 };


                    for (Int64 curX = 0; curX < binaryDataReader.BaseStream.Length; curX++)
                    {
                        foundData[curY].Add(binaryDataReader.ReadByte());

                        //  Optimised checking for extenders
                        if (curX == extendersFinalBytes[0])
                        {
                            //  Magic for extending for the packed color amount
                            Int32 foundExtend =
                            (
                                FromBinary.BigEndian<Int32>
                                (
                                    foundData[curY].ToArray()[(foundData[curY].Count - 2)..foundData[curY].Count]
                                    //  magic numbers for extender[0] length (= 2)
                                ) + 1
                            ) / 2;

                            extendedX += foundExtend;
                            extendersFinalBytes[1] += foundExtend;
                        }
                        else if (curX == extendersFinalBytes[1])
                        {
                            Int32 foundExtend = FromBinary.BigEndian<Int32>
                            (
                                foundData[curY].ToArray()[(foundData[curY].Count - 1)..foundData[curY].Count]
                                //  magic numbers for extender[1] length (= 1)
                            );

                            extendedX += foundExtend;
                        }


                        //  If the read data length is more than the split length
                        if (curX > splitAfterLength + extendedX)
                        {
                            //  Move to a new list Y line
                            foundData.Add(new List<Byte>());
                            curY++;

                            //  Loop previous extender values by magic numbers (1 + oldExtFinalBytes)
                            extendersFinalBytes[0] = curX + 7;
                            extendersFinalBytes[1] = curX + 12;
                            extendedX += splitAfterLength + 2;
                        }
                    }

                    binaryDataReader.Close();
                    if (showInfo)
                    {
                        if (engLang)
                        {
                            Write("\t\tBinary file ");
                            ForegroundColor = ConsoleColor.DarkGray;
                            Write(">");
                            ForegroundColor = ConsoleColor.White;
                            Write(fileName);
                            ForegroundColor = ConsoleColor.DarkGray;
                            Write("<");
                            ForegroundColor = ConsoleColor.Green;
                            Write("was successfully read\n");
                        }
                        else
                        {
                            Write("\t\tДвоичный файл ");
                            ForegroundColor = ConsoleColor.DarkGray;
                            Write(">");
                            ForegroundColor = ConsoleColor.White;
                            Write(fileName);
                            ForegroundColor = ConsoleColor.DarkGray;
                            Write("<");
                            ForegroundColor = ConsoleColor.Green;
                            Write(" был успешно прочитан\n");
                        }
                        ForegroundColor = ConsoleColor.White;
                    }   //  Execution info (optional)
                    return foundData;
                }

                //  File was not found
                else if (showInfo)
                {
                    ForegroundColor = ConsoleColor.Red;
                    if (engLang)
                    {
                        Write("\t\tError while reading the binary file! ");
                        ForegroundColor = ConsoleColor.White;
                        Write("File >" + fileName + "< was not found\n");
                    }
                    else
                    {
                        Write("\t\tОшибка при чтении двоичного файла! ");
                        ForegroundColor = ConsoleColor.White;
                        Write("Файл >" + fileName + "< не найден\n");
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                if (showInfo)
                {
                    ForegroundColor = ConsoleColor.Red;
                    if (engLang)
                    {
                        Write("\t\tError while reading the binary file ");
                        ForegroundColor = ConsoleColor.DarkGray;
                        Write(">");
                        ForegroundColor = ConsoleColor.White;
                        Write(fileName);
                        ForegroundColor = ConsoleColor.DarkGray;
                        Write("<");
                        ForegroundColor = ConsoleColor.White;
                        Write("\n\t\tOutput error: " + e + "\n");
                    }
                    else
                    {
                        Write("Ошибка при чтении двоичного файла ");
                        ForegroundColor = ConsoleColor.DarkGray;
                        Write(">");
                        ForegroundColor = ConsoleColor.White;
                        Write(fileName);
                        ForegroundColor = ConsoleColor.DarkGray;
                        Write("<");
                        ForegroundColor = ConsoleColor.White;
                        Write("\n\t\tКод ошибки: " + e + "\n");
                    }
                }   //  Error message (optional)
                return null;
            }
        }



        static public void EncodeV3()
        {
            for (Int32 sizeY = 0; sizeY < Gallery.Count; sizeY++)
            {
                for (Int32 sizeX = 0; sizeX < Gallery[sizeY].Count; sizeX++)
                {
                    string fileName = Users[sizeY].DeviceName + "~" + Users[sizeY].Username + ".patterns";

                    EncodeV3(fileName, Gallery[sizeY][sizeX].PatternType,
                        Gallery[sizeY][sizeX].Width, Gallery[sizeY][sizeX].Height,
                        Gallery[sizeY][sizeX].ColAmount, Gallery[sizeY][sizeX].PackedColorBytes,
                        Gallery[sizeY][sizeX].CompactUnixDatetime, Gallery[sizeY][sizeX].PatternName);
                }
            }
        }
        static public void EncodeV3(string fileName, Byte type, UInt16 X, UInt16 Y, UInt16 colAmount,
            Byte[] colors, UInt32 compactUnixDateTime, string patternName)
        {
            //  Resolve corrupted data issues, by resetting it to default values
            if (patternName == "") patternName = "Unnamed";
            if (fileName == "") fileName = Environment.MachineName + "~" + Environment.UserName + ".patterns";

            if (gShowInfo) DevInfoEncV3(type, X, Y, colAmount, colors, compactUnixDateTime, patternName);

            List<Byte[]> patternData = new List<Byte[]>
            {
                new byte[] { type },
                ToBinary.BigEndian(X),
                ToBinary.BigEndian(Y),

                ToBinary.BigEndian(colAmount),
                colors,

                ToBinary.BigEndian(compactUnixDateTime),

                new byte[] { (Byte)patternName.Length },
                ToBinary.Utf8(patternName)
            };

            BetterDataIO.SaveBinaryData(gGalleryPath, fileName, patternData, true, "", gShowInfo, false, "\t\t");
        }



        static private void DevInfoEncV3(Byte type, UInt16 X, UInt16 Y, UInt16 colAmount,
            Byte[] colors, UInt32 compactUnixDateTime, string patternName)
        {
            Write("\n\n\t[i] ------ DATA OF ");
            ForegroundColor = ConsoleColor.DarkGray;
            Write(">");
            ForegroundColor = ConsoleColor.White;
            Write(patternName);
            ForegroundColor = ConsoleColor.DarkGray;
            Write("<");
            ForegroundColor = ConsoleColor.White;

            Write($"\n\tPATTYPE:   {type}");



            ForegroundColor = ConsoleColor.DarkCyan;
            Write("\n\tDIME");
            ForegroundColor = ConsoleColor.DarkMagenta;
            Write("NSION: ");
            ForegroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; i < ToBinary.BigEndian(X).Length; i++)
                Write(ToBinary.BigEndian(X)[i] + " ");
            ForegroundColor = ConsoleColor.White;
            Write("x ");
            ForegroundColor = ConsoleColor.DarkMagenta;
            for (int i = 0; i < ToBinary.BigEndian(Y).Length; i++)
                Write(ToBinary.BigEndian(Y)[i] + " ");
            ForegroundColor = ConsoleColor.DarkCyan;
            Write("   (X width");
            ForegroundColor = ConsoleColor.White;
            Write(" x ");
            ForegroundColor = ConsoleColor.DarkMagenta;
            Write("Y height)");



            ForegroundColor = ConsoleColor.White;
            Write("\n\tCOLAMOUNT: ");
            ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < ToBinary.BigEndian(colAmount).Length; i++)
                Write(ToBinary.BigEndian(colAmount)[i] + " ");
            ForegroundColor = ConsoleColor.White;

            Write("\n\tCOLORS:    ");
            Byte[] unpacked = UnpackColors(colors, colAmount);
            for (int i = 0; i < colors.Length; i++)
            {
                Write(colors[i].ToString());
                for (int j = 0; j < 8 - colors[i].ToString().Length; j++) Write("_");
            }

            ForegroundColor = gAllColors[unpacked[0]];
            Write("\n\tUNPACKED:  ");
            for (int i = 0; i < unpacked.Length; i += 2)
            {
                ForegroundColor = gAllColors[unpacked[i]];
                if (unpacked[i] == 15) BackgroundColor = ConsoleColor.Gray;
                Write(unpacked[i]);
                BackgroundColor = ConsoleColor.Black;
                for (int j = 0; j < 4 - unpacked[i].ToString().Length; j++) Write(" ");

                if (i + 1 < unpacked.Length)
                {
                    ForegroundColor = gAllColors[unpacked[i + 1]];
                    if (unpacked[i + 1] == 15) BackgroundColor = ConsoleColor.Gray;
                    Write(unpacked[i + 1]);
                    BackgroundColor = ConsoleColor.Black;
                    for (int j = 0; j < 4 - unpacked[i + 1].ToString().Length; j++) Write(" ");
                }
            }


            Write("\n\t");
            float fcolorOffset = (float)8 / (float)unpacked.Length;
            //if (fcolorOffset < 1) fcolorOffset = 1; 
            ForegroundColor = gAllColors[unpacked[(Int32)((float)0 / fcolorOffset)]];
            if (unpacked[(Int32)((float)0 / fcolorOffset)] == 15) BackgroundColor = ConsoleColor.DarkGray;
            Write("G");
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = gAllColors[unpacked[(Int32)((float)1 / fcolorOffset)]];
            if (unpacked[(Int32)((float)1 / fcolorOffset)] == 15) BackgroundColor = ConsoleColor.DarkGray;
            Write("R");
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = gAllColors[unpacked[(Int32)((float)2 / fcolorOffset)]];
            if (unpacked[(Int32)((float)2 / fcolorOffset)] == 15) BackgroundColor = ConsoleColor.DarkGray;
            Write("A");
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = gAllColors[unpacked[(Int32)((float)3 / fcolorOffset)]];
            if (unpacked[(Int32)((float)3 / fcolorOffset)] == 15) BackgroundColor = ConsoleColor.DarkGray;
            Write("D");
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = gAllColors[unpacked[(Int32)((float)4 / fcolorOffset)]];
            if (unpacked[(Int32)((float)4 / fcolorOffset)] == 15) BackgroundColor = ConsoleColor.DarkGray;
            Write("I");
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = gAllColors[unpacked[(Int32)((float)5 / fcolorOffset)]];
            if (unpacked[(Int32)((float)5 / fcolorOffset)] == 15) BackgroundColor = ConsoleColor.DarkGray;
            Write("E");
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = gAllColors[unpacked[(Int32)((float)6 / fcolorOffset)]];
            if (unpacked[(Int32)((float)6 / fcolorOffset)] == 15) BackgroundColor = ConsoleColor.DarkGray;
            Write("N");
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = gAllColors[unpacked[(Int32)((float)7 / fcolorOffset)]];
            if (unpacked[(Int32)((float)7 / fcolorOffset)] == 15) BackgroundColor = ConsoleColor.DarkGray;
            Write("T:");
            BackgroundColor = ConsoleColor.Black;
            Write("  ");

            for (int i = 0; i < unpacked.Length; i++)
            {
                BackgroundColor = gAllColors[unpacked[i]];
                Write("    ");
            }
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.White;



            Write("\n\tREALUNIX:  " + compactUnixDateTime + " (Since 1 jan 2025)");
            Write("\n\tDATETIME:  ");
            Byte[] datetimeBytes = ToBinary.BigEndian(compactUnixDateTime);
            fcolorOffset = (float)4 / (float)unpacked.Length;
            if (fcolorOffset < 1) fcolorOffset = 1;
            for (int i = 0; i < 4; i++)
            {
                ForegroundColor = gAllColors[unpacked[(Int32)((float)i / fcolorOffset)]];
                if (unpacked[(Int32)((float)i / fcolorOffset)] == 15) BackgroundColor = ConsoleColor.DarkGray;
                Write(datetimeBytes[i]);
                BackgroundColor = ConsoleColor.Black;
                for (int j = 0; j < 4 - datetimeBytes[i].ToString().Length; j++) Write(" ");
            }


            ForegroundColor = ConsoleColor.White;
            Write("\n\tNAMLENGTH: ");
            for (int i = 0; i < ToBinary.BigEndian((Byte)patternName.Length).Length; i++)
                Write(ToBinary.BigEndian((Byte)patternName.Length)[i] + " ");
            ForegroundColor = ConsoleColor.White;

            Write("\n\tNAMEBYTES: ");
            Byte[] pnameBytes = ToBinary.Utf8(patternName);
            fcolorOffset = (float)patternName.Length / (float)unpacked.Length;
            if (fcolorOffset < 1) fcolorOffset = 1;
            for (int i = 0; i < pnameBytes.Length; i++)
            {
                ForegroundColor = gAllColors[unpacked[(Int32)((float)i / fcolorOffset)]];
                if (unpacked[(Int32)((float)i / fcolorOffset)] == 15) BackgroundColor = ConsoleColor.DarkGray;
                Write(pnameBytes[i]);
                BackgroundColor = ConsoleColor.Black;
                for (int j = 0; j < 4 - pnameBytes[i].ToString().Length; j++) Write(" ");
            }
            ForegroundColor = ConsoleColor.White;

            Write("\n\tPATNAME:   ");
            for (int i = 0; i < patternName.Length; i++)
            {
                ForegroundColor = gAllColors[unpacked[(Int32)((float)i / fcolorOffset)]];
                if (unpacked[(Int32)((float)i / fcolorOffset)] == 15) BackgroundColor = ConsoleColor.DarkGray;
                Write(patternName[i]);
                for (int j = 0; j < pnameBytes[i].ToString().Length - 1; j++) Write(" ");
                BackgroundColor = ConsoleColor.Black;
                for (int j = 0; j < 4 - pnameBytes[i].ToString().Length; j++) Write(" ");
            }
            ForegroundColor = ConsoleColor.White;


            Write("\n");
        }
    }
}