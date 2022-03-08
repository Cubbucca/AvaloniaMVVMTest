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
            get => caption ??= GetDoctor(5);
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
            get => list ??= this[1,3,5,100];
            set => this.RaiseAndSetIfChanged(ref list, value);
        }

        private ProductionTaskMisc newtask(string name)
        {
            if (name is null) return null;
            return new ProductionTaskMisc(Guid.NewGuid(), name);
        }

        public void AddToList()
        {
            if (Caption == null) return;
            if (List?
                .NoMatch(List, Caption, new[] { nameof(ProductionTaskMisc.Name) })?
                .AddItem(newtask(Caption))?
                .OrderBy(x => x.Name)
                .ToList()
                is List<ProductionTaskMisc> locallist)
            {
                List = new ObservableCollectionExtended<ProductionTaskMisc>(locallist);
                this.RaisePropertyChanged(nameof(Caption));
            }
        }

        private Maybe<ObservableCollectionExtended<ProductionTaskMisc>> GetList(ObservableCollectionExtended<ProductionTaskMisc>? locallist)
        {
            return (locallist == null) ? Maybe<ObservableCollectionExtended<ProductionTaskMisc>>.None() : new Maybe<ObservableCollectionExtended<ProductionTaskMisc>>(locallist);
        }
        public void AddToListBind()
        {
            if (Caption == null) return;
            List<ProductionTaskMisc>? listwithaddeditemandorderdbyname = GetList(List)
                ?.Bind(nm => nm.NoMatchBind(Caption, new[] { nameof(ProductionTaskMisc.Name) }))
                ?.Bind(a => a.AddItemBind(newtask(Caption)))
                ?.Bind(o => o.OrderByBind(nameof(ProductionTaskMisc.Name)))
                ?.value?.ToList();
            if (listwithaddeditemandorderdbyname is List<ProductionTaskMisc> locallist)
            {
                List = new ObservableCollectionExtended<ProductionTaskMisc>(locallist);
                this.RaisePropertyChanged(nameof(Caption));
            }
        }
        public Func<int, string> GetDoctor =
            new Dictionary<int, string>
            {
                {1, "William" },
                {2, "Patrick" },
                {3, "Jon Pertwee" },
                {4, "Tom Baker" },
                {5, "Peter Davison" }
            }.ToLookupWithDefault("NotFound");
        public ObservableCollectionExtended<ProductionTaskMisc> this[params int[] indexs] => GenNew(indexs.Select(GetDoctor));

        public ObservableCollectionExtended<ProductionTaskMisc> GenNew(IEnumerable<string> strings)
        {
            var result = new ObservableCollectionExtended<ProductionTaskMisc>();
            foreach (string s in strings)
            {
                result.Add(newtask(s));
            }
            return result;
        }

    }
}
