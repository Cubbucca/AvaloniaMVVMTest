using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMTest.Models
{
    public static class Extentions{
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
    public class ObservableCollectionExtended<T> : ObservableCollection<T>
    {
        public ObservableCollectionExtended() : base() { }
        public ObservableCollectionExtended(IEnumerable<T> collection):base(collection) { }

        public Func<ObservableCollectionExtended<T>, string?, string[], ObservableCollectionExtended<T>?> anyMatch = (list, tomatch, fieldtomatch) =>
        {
            foreach(var phrase in fieldtomatch)
            {
                if (list.Any(x => x?.GetType().GetProperty(phrase)?.GetValue(x) as string == tomatch))
                    return new ObservableCollectionExtended<T>(list);
            }
            return  null;
        };

        

        public Func<ObservableCollectionExtended<T>, string? ,string[], ObservableCollectionExtended<T>?> NoMatch = (list, tomatch, fieldtomatch) =>
        {
            return (list.anyMatch(list,tomatch, fieldtomatch) == null) ? new ObservableCollectionExtended<T>(list) : null;
        };

        public ObservableCollectionExtended<T>? AddItem(T toadd)
        {
            if(toadd == null) return null;
            var result = new ObservableCollectionExtended<T>(this);
            result.Add(toadd);
            return result;
        }

        public Maybe<ObservableCollectionExtended<T>> NoMatchBind(string? tomatch, string[] filters)
        {
            var result = NoMatch(this, tomatch, filters);
            return (result != null) ? new Maybe<ObservableCollectionExtended<T>>(result) : Maybe<ObservableCollectionExtended<T>>.None();
        }

        public Maybe<ObservableCollectionExtended<T>> AddItemBind(T newitem)
        {
            var result = AddItem(newitem);
            return (result != null) ? new Maybe<ObservableCollectionExtended<T>>(result) : Maybe<ObservableCollectionExtended<T>>.None();
        }

        public Maybe<ObservableCollectionExtended<T>> OrderByBind(string sortby)
        {
            var result = new ObservableCollectionExtended<T>(this.OrderBy(x => sortby));
            return (result != null) ? new Maybe<ObservableCollectionExtended<T>>(result) : Maybe<ObservableCollectionExtended<T>>.None();
        }
    }
}
