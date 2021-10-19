using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReactiveUI;
using System.Collections.ObjectModel;

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
            }
        }

        private ObservableCollection<string>? list;

        public ObservableCollection<string>? List
        {
            get => list;
            set => this.RaiseAndSetIfChanged(ref list, value);
        }

        public void AddToList()
        {
            if (List is null) List = new ObservableCollection<string>();
            if (List != null && !List.Where(x => x.Equals(Caption)).Any())
            {
                List.Add(Caption);
                List = new ObservableCollection<string>(List.OrderBy(x => x).ToList());
            }
            this.RaisePropertyChanged(nameof(Caption));
        }
    }
}
