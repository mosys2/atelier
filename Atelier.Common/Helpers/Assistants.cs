using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Helpers
{
    public static class Assistants
    {
        public static long LongRandomBetween(long maxValue, long minValue)
        {
            Random random = new Random((int)DateTime.Now.Ticks);

            return (long)Math.Round(random.NextDouble() * (maxValue - minValue - 1)) + minValue;
            
        }
    }
}
