using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AvaloniaMVVMTest.Models
{
    public class ProductionTaskMisc : ReactiveObject, IProductionTask
    {
        private Guid guid;
        public Guid ID
        {
            get => guid;
            init => guid = value;
        }
        [Reactive]
        public string Name { get; set; } = string.Empty;
        private int intgetterprivate;
        public int IntSetter { set => intgetterprivate = value;}
    }
}
