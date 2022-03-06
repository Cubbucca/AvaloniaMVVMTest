using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReactiveUI;
using System.Collections.ObjectModel;
using AvaloniaMVVMTest.Models;

namespace AvaloniaMVVMTest.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string caption = string.Empty;
        public string Caption
        { 
            get => caption;
            set
            {
                if (value is null) return;
                this.RaiseAndSetIfChanged(ref caption, value);
                this.RaisePropertyChanged(nameof(MiscTask));
            }
        }

        public ProductionTaskMisc? MiscTask
        {
            get => List?.Where(x => x.Name.Equals(Caption)).FirstOrDefault();
            set
            {
                if (value is null) return;
                Caption = value.Name;
            }
        }

        private ObservableCollectionExtended<ProductionTaskMisc>? list;

        public ObservableCollectionExtended<ProductionTaskMisc>? List
        {
            get
            {
                if (list is null)
                    list = new ObservableCollectionExtended<ProductionTaskMisc>();
                return list;
            }
            set => this.RaiseAndSetIfChanged(ref list, value);
        }

        public void AddToList()
        {
            var newlist = List?
                .NoMatch(List, Caption, new[] {nameof(ProductionTaskMisc.Name)})?
                .AddItem(new ProductionTaskMisc() { ID = Guid.NewGuid(), Name = Caption })
                .OrderBy(x => x.Name);
            if (newlist != null)
            {
                List = new ObservableCollectionExtended<ProductionTaskMisc>(newlist);
                this.RaisePropertyChanged(nameof(Caption));
            }
        }
    }
}
