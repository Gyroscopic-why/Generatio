using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;



namespace Generatio.Source.Patterns
{
    public class Pattern1 : IPattern
    {
        public Pattern1(UInt16 sizeX, UInt16 sizeY,
            ConsoleColor[] colors, string patternName,
            bool useMargin, bool showInfo)
            : base(1, sizeX, sizeY, colors, patternName, useMargin, showInfo) { }



        
        public override void DrawFull()
        {
            Int32 curColor, colAmount = Colors.Length;
            (UInt16 X, UInt16 Y) = NormalizeSize();



            string margin = CalculateMargin(X, Console.WindowWidth, 0);

            for (Int32 curY = 0; curY < (Y + 1) / 2; curY++)
            {
                Console.Write(margin);
                for (Int32 curX = curY; curX < (X + 1) / 2 + curY; curX++)
                {
                    curColor = curX % (colAmount * 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                    Console.BackgroundColor = Colors[curColor];
                    Console.Write("  ");
                }
                for (Int32 curX = X / 2 + curY; curX > curY; curX--)
                {
                    curColor = (curX - 1) % (colAmount * 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                    Console.BackgroundColor = Colors[curColor];
                    Console.Write("  ");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\n");
            }
            for (Int32 curY = Y / 2; curY > 0; curY--)
            {
                Console.Write(margin);
                for (Int32 curX = curY; curX < (X + 1) / 2 + curY; curX++)
                {
                    curColor = (curX - 1) % (colAmount * 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                    Console.BackgroundColor = Colors[curColor];
                    Console.Write("  ");
                }
                for (Int32 curX = X / 2 + curY; curX > curY; curX--)
                {
                    curColor = (curX - 2) % (colAmount * 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                    Console.BackgroundColor = Colors[curColor];
                    Console.Write("  ");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\n");
            }
        }

        public override ConsoleColor[] DrawLine(UInt16 targetY)
            => targetY < (SizeY + 1) / 2
                ? DrawTopLine   (targetY, 0, SizeX, SizeX)
                : DrawBottomLine(targetY, 0, SizeX, SizeX);

        public override ConsoleColor[] DrawLine(UInt16 targetY, UInt16 startX, UInt16 length, UInt16 nX, UInt16 nY)
            => targetY < (nY + 1) / 2
                ? DrawTopLine   (targetY, startX, length, nX)
                : DrawBottomLine(targetY, startX, length, nX);



        public ConsoleColor[] DrawTopLine(UInt16 targetY, UInt16 startX, UInt16 length, UInt16 nX)
        {
            ConsoleColor[] resBuffer = new ConsoleColor[length], cycle = new ConsoleColor[nX];
            Int32 colorId, bufferId, colorCount = Colors.Length, shortCount = colorCount - 1;
            startX %= nX;

            for (Int32 curX = targetY; curX < (nX + 1) / 2 + targetY; curX++)
            {
                //colorId = curX % (colorCount * 2);
                //if (colorId > colorCount - 1)
                //    colorId = colorCount - colorId % colorCount - 1;

                bufferId = (curX + 2 * shortCount) % (2 * shortCount) - shortCount;
                colorId = shortCount - ((bufferId ^ (bufferId >> 31)) - (bufferId >> 31));

                cycle[curX - targetY] = Colors[colorId];
            }
            for (Int32 curX = nX / 2 + targetY; curX > targetY; curX--)
            {
                //colorId = (curX - 1) % (colorCount * 2);
                //if (colorId > colorCount - 1)
                //    colorId = colorCount - colorId % colorCount - 1;

                bufferId = (curX + 2 * shortCount - 1) % (2 * shortCount) - shortCount;
                colorId = shortCount - ((bufferId ^ (bufferId >> 31)) - (bufferId >> 31));

                cycle[nX + targetY - curX] = Colors[colorId];
            }

            if (startX + length <= nX) return cycle[startX..(startX + length)];
            else
            {
                for (var i = startX; i < startX + length; i++)
                    resBuffer[i - startX] = cycle[i % nX];
                return resBuffer;
            }
        }
        public ConsoleColor[] DrawBottomLine(UInt16 targetY, UInt16 startX, UInt16 length, UInt16 nX)
        {
            ConsoleColor[] resBuffer = new ConsoleColor[length], cycle = new ConsoleColor[nX];
            Int32 colorId, bufferId, colorCount = Colors.Length, shortCount = colorCount - 1;
            startX %= nX;

            for (Int32 curX = -targetY; curX < (nX + 1) / 2 - targetY; curX++)
            {
                bufferId = (-curX + 2 * shortCount - 1) % (2 * shortCount) - shortCount;
                colorId = shortCount - ((bufferId ^ (bufferId >> 31)) - (bufferId >> 31));

                cycle[curX + targetY] = Colors[colorId];
            }
            for (Int32 curX = nX / 2 - targetY; curX > -targetY; curX--)
            {
                bufferId = (-curX + 2 * shortCount) % (2 * shortCount) - shortCount;
                colorId = shortCount - ((bufferId ^ (bufferId >> 31)) - (bufferId >> 31));

                cycle[nX - targetY - curX] = Colors[colorId];
            }

            if (startX + length <= nX) return cycle[startX..(startX + length)];
            else
            {
                for (var i = startX; i < startX + length; i++)
                    resBuffer[i - startX] = cycle[i % nX];
                return resBuffer;
            }
        }
    }
}