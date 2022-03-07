using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMTest.Models
{
    public class Maybe<T> where T : class
    {
        public readonly T? value;

        public Maybe(T someValue)
        {
            if (someValue == null)
                throw new ArgumentNullException(nameof(someValue));
            this.value = someValue;
        }

        private Maybe(){}

        public Maybe<U> Bind<U>(Func<T, Maybe<U>> func) where U : class
        {
            return value != null ? func(value) : Maybe<U>.None();
        }

        public static Maybe<T> None() => new();
    }
}
