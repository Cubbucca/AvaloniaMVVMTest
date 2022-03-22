using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvaloniaMVVMTest.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        internal Window? window;
        public ViewModelBase(object? view)
        {
            if (view is Window win)
                window = win;
        }
    }
}
