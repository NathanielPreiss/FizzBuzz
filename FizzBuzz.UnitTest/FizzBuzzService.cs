using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FizzBuzz.UnitTest
{
    [TestClass]
    public class FizzBuzzServiceTest
    {
        [DynamicData(nameof(TestDefaultsData), DynamicDataSourceType.Method)]
        [DataTestMethod]
        public async Task TestDefaults(int min, int max)
        {
            var multiples = new[] { new Multiple { Name = "Fizz", Value = 3 } };
            var config = new DefaultsConfig { DefaultMinValue = min, DefaultMaxValue = max, DefaultMultiples = multiples };
            var service = new FizzBuzzService(config);

            var result = await service.FizzBuzz();

            FizzBuzzValidation(min, max, result, multiples);
        }

        [DynamicData(nameof(TestDefaultMultiplesData), DynamicDataSourceType.Method)]
        [DataTestMethod]
        public async Task TestDefaultMultiples(Multiple[] multiples)
        {
            const int min = 1, max = 100;
            var config = new DefaultsConfig { DefaultMinValue = min, DefaultMaxValue = max, DefaultMultiples = multiples };
            var service = new FizzBuzzService(config);

            var result = await service.FizzBuzz();

            FizzBuzzValidation(min, max, result, multiples);
        }

        private static void FizzBuzzValidation(int min, int max, IEnumerable<string> results, params Multiple[] multiples)
        {
            var currentValue = min;
            foreach (var val in results)
            {
                var testResult = multiples.Where(multiple => currentValue % multiple.Value == 0)
                    .Aggregate(string.Empty, (current, multiple) => current + multiple.Name);

                if (testResult == string.Empty)
                    Assert.AreEqual(int.Parse(val), currentValue);
                else
                    Assert.AreEqual(val, testResult);

                currentValue++;
            }

            Assert.AreEqual(currentValue - 1, max);
        }

        private static IEnumerable<object[]> TestDefaultsData()
        {
            yield return new object[] { -100, -1 };
            yield return new object[] { -50, 0 };
            yield return new object[] { -50, 50 };
            yield return new object[] { 0, 50 };
            yield return new object[] { 1, 100 };
        }

        private static IEnumerable<object[]> TestDefaultMultiplesData()
        {
            yield return new object[] { new[] { new Multiple { Name = "Fizz", Value = 1 } } };
            yield return new object[] { new[] { new Multiple { Name = "Fizz", Value = 3 }, new Multiple { Name = "Buzz", Value = 5 } } };
            yield return new object[] { new[] { new Multiple { Name = "Fizz", Value = 2 }, new Multiple { Name = "Buzz", Value = 5 }, new Multiple { Name = "Tazz", Value = 7 } } };
        }
    }
}
