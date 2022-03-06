using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMTest.Extensions
{
    public static class ListExtensions
    {
        public static Func<ObservableCollection<object>, string, ObservableCollection<object>> anyMatch = (list, tomatch) =>
        {
            return (list.Any(x => x?.GetType().GetProperty("Name")?.GetValue(x) as string == tomatch))?list:null;
        };
    }
}
