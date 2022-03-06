using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMTest.Models
{
    public class ObservableCollectionExtended<T> : ObservableCollection<T>
    {
        public ObservableCollectionExtended() : base() { }
        public ObservableCollectionExtended(IEnumerable<T> collection):base(collection) { }

        public Func<ObservableCollectionExtended<T>, string, string[], ObservableCollectionExtended<T>?> anyMatch = (list, tomatch, fieldtomatch) =>
        {
            foreach(var phrase in fieldtomatch)
            {
                if (list.Any(x => x?.GetType().GetProperty(phrase)?.GetValue(x) as string == tomatch))
                    return new ObservableCollectionExtended<T>(list);
            }
            return  null;
        };

        public Func<ObservableCollectionExtended<T>, string ,string[], ObservableCollectionExtended<T>?> NoMatch = (list, tomatch, fieldtomatch) =>
        {
            return (list.anyMatch(list,tomatch, fieldtomatch) == null) ? new ObservableCollectionExtended<T>(list) : null;
        };

        public ObservableCollectionExtended<T> AddItem(T toadd)
        {
            var result = new ObservableCollectionExtended<T>(this);
            result.Add(toadd);
            return result;
        }
    }
}
