using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMTest.Models
{
    public enum Gender
    {
        None,
        Male,
        Female,
    }
    public static class ExtentionsForPerson
    {
        public static int GetPositiveNumber(this int n) => n > 0 ? n : 0;
        public static int Age(this DateTime birthdate) => ((DateTime.Now - birthdate).Days/365).GetPositiveNumber();
        //decimal[] already has Sum but this shows how to write extention function.
        public static decimal SumAssets(this decimal[] arr) => arr.Sum(x => x);
        public static string? ValidateName(this string name) => name.Map(x => x.Length > 3 ? x : "To Short")?.Map(x => x.Length < 16 ? x : "To Long");

        public static DateTime PraseDate(this string date) => DateTime.TryParse(date,out DateTime prased) == true ? prased : default;

    }
    public class Person : ReactiveObject
    {
        public Person(string name, DateTime birth, Gender gender, decimal[] assets)
        {
            ID = Guid.NewGuid();
            Name = name.ValidateName();
            Age = birth.Age();
            Gender = gender;
            Wealth = assets.SumAssets();
        }
        private Guid guid;
        public Guid ID
        {
            get => guid;
            init => guid = value;
        }
        [Reactive]
        public string? Name { get; set; }
        [Reactive]
        public int Age { get; set; }
        [Reactive]
        public Gender Gender { get; set; }
        [Reactive]
        public decimal Wealth { get; set; }
    }
}
