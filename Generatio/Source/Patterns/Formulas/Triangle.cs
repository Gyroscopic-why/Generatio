using System;



namespace Generatio.Source.Patterns
{
    static public partial class Formulas
    {
        static public Int32 TriangleElement(Int32 limit, Int32 x, Int32 offset)
        {
            Int32 peak = limit - 1;
            Int32 period = 2 * peak;
            Int32 absX = (x + offset - 1) % period;
            absX += absX >> 31 & period;
            Int32 diff = absX - peak;
            return peak - 1 - (diff ^ diff >> 31) - (diff >> 31);  //  bitwise abs
        }
    }
}