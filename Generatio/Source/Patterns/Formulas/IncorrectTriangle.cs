using System;



namespace Generatio.Source.Patterns
{
    static public partial class Formulas
    {
        static public Int32 IncorrectTriangleElement(Int32 limit, Int32 x, Int32 offset)
        {
            Int32 period = 2 * limit;

            Int32 t = (x + offset) % period;
            t += (t >> 31) & period;  //  bit abs

            Int32 val1 = (t - 1) & ~((t - 1) >> 31);   // up, bit MAX(0, v)
            Int32 val2 = period - t;  //  down

            // mask = 0, if t <= limit, else -1
            Int32 mask = (limit - t) >> 31;
            return (val1 & ~mask) | (val2 & mask);
        }
    }
}