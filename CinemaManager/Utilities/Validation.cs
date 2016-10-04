using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Validation
    {
        private static readonly string DEFAULT_VALUE = "null";
        private static string GetStringValueOrDefault<T>(T source)
        {
            return (source == null ? DEFAULT_VALUE : source.ToString());
        }

        public static bool isGreaterThan<T>(T shouldBeGreater, T shouldBeLesser)
        {
            return isGreaterThan(shouldBeGreater, shouldBeLesser, 
                $"Value {GetStringValueOrDefault(shouldBeGreater)} must be greater than value {GetStringValueOrDefault(shouldBeLesser)}");
        }

        public static bool isGreaterThan<T>(T shouldBeGreater, T shouldBeLesser,string message)
        {
            if (GenericCompare(shouldBeGreater, shouldBeLesser) > 0)
            {
                return true;
            }
            else
            {
                throw new ArgumentException(message);
            }
        }

        public static bool isLessThanOrEqual<T>(T shouldBeLesser, T shouldBeGreater)
        {
            if (GenericCompare(shouldBeLesser, shouldBeGreater) <= 0)
            {
                return true;
            }
            else
            {
                throw new ArgumentException(
                    $"Value {GetStringValueOrDefault(shouldBeLesser)} must be less than or equal to value {GetStringValueOrDefault(shouldBeGreater)}");
            }
        }

        public static bool isGreaterThanOrEqual<T>(T shouldBeGreater, T shouldBeLesser)
        {
            if(GenericCompare(shouldBeGreater,shouldBeLesser) >= 0 )
            {
                return true;
            }
            else
            {
                throw new ArgumentException(
                    $"Value {GetStringValueOrDefault(shouldBeGreater)} must be greater than or equal to value {GetStringValueOrDefault(shouldBeLesser)}");
            }
        }

        public static bool isNotNull(object property, string propertyName)
        {
            if(property != null )
            {
                return true;
            }
            else
            {
                throw new ArgumentException($"Property {GetStringValueOrDefault(propertyName)} cannot be null");
            }
        }

        private static int GenericCompare<T>(T first, T second)
        {
            return Comparer<T>.Default.Compare(first, second);
        }

    }

    

}
