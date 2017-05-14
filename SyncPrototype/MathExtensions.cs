using System;
using System.Linq;

namespace SyncPrototype
{
    public static class MathExtensions
    {
        public static double Median(this long[] numbers)
        {
            if(numbers == null)
            {
                throw new ArgumentNullException(nameof(numbers));
            }

            var sorted = numbers
                .OrderBy(n => n)
                .ToArray();

            var median = (numbers.Length + 1)/ 2;
            if(sorted.Length % 2 == 1)
            {
                return numbers[median];
            }

            return (numbers[median] + numbers[median + 1]) / 2;
        }
    }
}
