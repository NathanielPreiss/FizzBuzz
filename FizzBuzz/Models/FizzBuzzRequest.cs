using System.Collections.Generic;

namespace FizzBuzz
{
    public class FizzBuzzRequest
    {
        public int? MaxValue { get; set; }

        public int? MinValue { get; set; }

        public IEnumerable<Multiple> Multiples { get; set; }
    }
}
