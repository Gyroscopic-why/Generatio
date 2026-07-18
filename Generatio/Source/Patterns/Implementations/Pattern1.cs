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

        public override ConsoleColor[] DrawLine(UInt16 curY)
            => curY < (SizeY + 1) / 2
                ? DrawTopLine   (curY, SizeX, SizeX)
                : DrawBottomLine(curY, SizeX, (UInt16)((SizeX + 1) / 2), (UInt16)((SizeY + 1) / 2));

        public ConsoleColor[] DrawLine(UInt16 curY, UInt16 nsizeX, UInt16 originX, UInt16 originY)
            => curY < (originY + 1) / 2
                ? DrawTopLine   (curY, nsizeX, originX)
                : DrawBottomLine(curY, nsizeX, originX, originY);



        public ConsoleColor[] DrawTopLine(UInt16 curY, UInt16 nsizeX, UInt16 originX)
        {
            Int32 colorCount = Colors.Length, delta = nsizeX - nsizeX / 2 * 2 - 1;
            Int32 pivot = curY + Math.Min(originX, nsizeX), renew = pivot + originX, end = renew - nsizeX;

            ConsoleColor[] result = new ConsoleColor[nsizeX];
            for (Int32 curX = curY; curX < pivot; curX++)
                result[curX - curY] = Colors[Formulas.TriangleElement(colorCount, curX, 0)];

            for (Int32 curX = pivot; curX > end; curX--)
                result[renew - curX] = Colors[Formulas.TriangleElement(colorCount, curX, delta)];

            return result;
        }
        public ConsoleColor[] DrawBottomLine(
            UInt16 curY, UInt16 nsizeX,
            UInt16 originX, UInt16 originY)
        {
            Int32 colorCount = Colors.Length, delta = nsizeX - nsizeX / 2 * 2 - 1;
            Int32 start = 2 * originY - curY - 1, pivot = start + Math.Min(originX, nsizeX);
            Int32 renew = pivot + originX, end = renew - nsizeX;

            ConsoleColor[] result = new ConsoleColor[nsizeX];
            for (Int32 curX = start; curX < pivot; curX++)
                result[curX - start] = Colors[Formulas.TriangleElement(colorCount, curX, 0)];

            for (Int32 curX = pivot; curX > end; curX--)
                result[renew - curX] = Colors[Formulas.TriangleElement(colorCount, curX, delta)];
            
            return result;
        }
    }
}