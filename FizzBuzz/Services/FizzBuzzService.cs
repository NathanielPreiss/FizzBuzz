using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FizzBuzz
{
    public class FizzBuzzService : IFizzBuzzService
    {
        private readonly DefaultsConfig _config;

        public FizzBuzzService(DefaultsConfig config)
        {
            _config = config;
        }

        public Task<IEnumerable<string>> FizzBuzz() => FizzBuzz(_config.DefaultMinValue, _config.DefaultMaxValue, _config.DefaultMultiples);
        
        public Task<IEnumerable<string>> FizzBuzz(int? minValue, int? maxValue, params Multiple[] multiples) => 
            FizzBuzz(minValue ?? _config.DefaultMinValue, maxValue ?? _config.DefaultMaxValue, multiples?.Length == 0 ? _config.DefaultMultiples : multiples);

        private Task<IEnumerable<string>> FizzBuzz(int minValue, int maxValue, params Multiple[] multiples)
        {
            static IEnumerable<int> GetRange(int min, int max) => Enumerable.Range(min, (min*-1) + max + 1);
            string Translate(int i) => multiples.Aggregate((string) null, (s, m) => i % m.Value == 0 ? (s == null ? m.Name : s + m.Name) : s) ?? i.ToString();

            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(maxValue), maxValue, $"Value must be more than {nameof(minValue)}.");

            if(multiples == null || multiples.Length == 0)
                throw new ArgumentNullException(nameof(multiples), "At least one multiple is required.");

            var valueRange = GetRange(minValue, maxValue);
            return Task.FromResult(valueRange.Select(Translate));
        }
    }
}
 