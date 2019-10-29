using System.Collections.Generic;
using System.Threading.Tasks;

namespace FizzBuzz
{
    public interface IFizzBuzzService
    {
        Task<IEnumerable<string>> FizzBuzz();
        Task<IEnumerable<string>> FizzBuzz(int? minValue, int? maxValue, params Multiple[] multiples);
    }
}
