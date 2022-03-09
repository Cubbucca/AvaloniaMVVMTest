using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReactiveUI;
using System.Collections.ObjectModel;
using AvaloniaMVVMTest.Models;
using ReactiveUI.Fody.Helpers;

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
        public string? Title { get => this.GetClassTitle(); }

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
        private Person? selectedperson;
        public Person? SelectedPerson
        {
            get => selectedperson ??= People?.FirstOrDefault();
            set => this.RaiseAndSetIfChanged(ref selectedperson, value);
        }

        private ObservableCollectionExtended<Person>? people;

        public ObservableCollectionExtended<Person>? People
        {
            get => people ??= GetPeople();
            set => this.RaiseAndSetIfChanged(ref people, value);
        }

        private ObservableCollectionExtended<Person> GetPeople()
        {
            return new ObservableCollectionExtended<Person>()
            {
                new Person("Timothy Morty", "7/4/1986".PraseDate() ,Gender.Male,new[]{100000M,55000M,100.70M }),
                new Person("SS", "7/4/50".PraseDate() ,Gender.Male,new[]{100000M,55100M,670.70M }),
                new Person("Looooooooooooooooooooooooong", "1/1/3014".PraseDate() ,Gender.Female,new[]{1023000M,5500M,100.70M }),
                new Person("Sam Niel", "7/4/97".PraseDate() ,Gender.Male,new[]{-100000M,55000M,-100.70M }),
                new Person("Ayeaye Captain", "10/9/1904".PraseDate() ,Gender.None,new[]{100000M,-55000M,100.70M }),
                new Person("Jimmy Jammer", "13/1/1999".PraseDate() ,Gender.Male,new[]{-100000M,-55000M,100.70M}),
            };

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
