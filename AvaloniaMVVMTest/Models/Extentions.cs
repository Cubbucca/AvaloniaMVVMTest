using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMTest.Models
{
    public static class Extentions
    {
        public static TOutput Map<TInput, TOutput>(this TInput @this, Func<TInput, TOutput> f) => f(@this);
        public static T Tee<T>(this T @this, Action<T> act)
        {
            act(@this);
            return @this;
        }
        public static T2? DoIfNotNull<T1, T2>(this T1 @this, Func<T1, T2> f) => !EqualityComparer<T1>.Default.Equals(@this, default) ? f(@this) : default;
        public static Func<TK, TV> ToLookupWithDefault<TK, TV>(this IDictionary<TK, TV> @this, TV defaultValue) => x => @this.ContainsKey(x) ? @this[x] : defaultValue;
        public static Func<TK, TV?> ToLookupWithDefault<TK, TV>(this IDictionary<TK, TV> @this) => x => @this.ContainsKey(x) ? @this[x] : default;
    }
}
