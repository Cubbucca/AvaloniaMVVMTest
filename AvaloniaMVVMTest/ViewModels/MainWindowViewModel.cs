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
        private string? caption;
        public string? Caption
        { 
            get => caption;
            set
            {
                this.RaiseAndSetIfChanged(ref caption, value);
                this.RaisePropertyChanged(nameof(MiscTask));
            }
        }

        public ProductionTaskMisc? MiscTask
        {
            get => List?.Where(x => x.Name.Equals(Caption)).FirstOrDefault();
            set => Caption = value?.Name;
        }

        private ObservableCollectionExtended<ProductionTaskMisc>? list;

        public ObservableCollectionExtended<ProductionTaskMisc>? List
        {
            get => list ??= new ObservableCollectionExtended<ProductionTaskMisc>();
            set => this.RaiseAndSetIfChanged(ref list, value);
        }

        public void AddToList()
        {
            if (List?
                .NoMatch(List, Caption, new[] { nameof(ProductionTaskMisc.Name) })?
                .AddItem(new ProductionTaskMisc() { ID = Guid.NewGuid(), Name = Caption })
                .OrderBy(x => x.Name)
                .ToList()
                is List<ProductionTaskMisc> locallist)
            {
                List = new ObservableCollectionExtended<ProductionTaskMisc>(locallist);
                this.RaisePropertyChanged(nameof(Caption));
            }
        }
    }
}
