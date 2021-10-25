using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMTest.Models
{
    public interface IProductionTask
    {
        public Guid ID { get; init; }
        public string Name { get; set; }
        public int IntSetter { set; }
    }
}
