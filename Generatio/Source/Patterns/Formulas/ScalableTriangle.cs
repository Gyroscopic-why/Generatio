using System;



namespace Generatio.Source.Patterns
{
    static public partial class Formulas
    {
        static public Int32 ScalableTriangleElement(Int32 limit, Int32 x, Int32 offset)
        {
            Int32 peak = limit - 1;
            Int32 period = 2 * peak;
            Int32 absX = (x + offset) % period;
            absX += absX >> 31 & period;
            Int32 diff = absX - peak;
            return peak - (diff ^ diff >> 31) - (diff >> 31);  //  bitwise abs
        }
    }
}