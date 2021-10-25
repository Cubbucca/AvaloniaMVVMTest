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


        private ObservableCollection<ProductionTaskMisc>? list;

        public ObservableCollection<ProductionTaskMisc>? List
        {
            get => list;
            set => this.RaiseAndSetIfChanged(ref list, value);
        }

        public void AddToList()
        {
            if (List is null) List = new ObservableCollection<ProductionTaskMisc>();
            if (List != null && !List.Where(x => x.Name.Equals(Caption)).Any())
            {
                var newitem = new ProductionTaskMisc() {ID = Guid.NewGuid(), Name = Caption };
                List.Add(newitem);
                List = new ObservableCollection<ProductionTaskMisc>(List.OrderBy(x => x.Name).ToList());
            }
            this.RaisePropertyChanged(nameof(Caption));
        }
    }
}
