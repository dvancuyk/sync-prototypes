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

            if (numbers.Length == 0) return 0;
            if (numbers.Length == 1) return numbers[0];

            var sorted = numbers
                .OrderBy(n => n)
                .ToArray();

            var median = (numbers.Length)/ 2;
            if(sorted.Length % 2 == 1)
            {
                return numbers[median];
            }

            return (numbers[median - 1] + numbers[median]) / 2;
        }
    }
}
