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
        public ProductionTaskMisc(Guid id, string name)
        {
            ID = id;
            Name = name;
        }

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
        public string? Title { get => this.GetClassTitle(); }
    }
}
