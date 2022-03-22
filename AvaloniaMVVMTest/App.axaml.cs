using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaMVVMTest.ViewModels;
using AvaloniaMVVMTest.Views;

namespace AvaloniaMVVMTest
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var view = new MainWindow();
                var model = new MainWindowViewModel(view);
                view.DataContext = model;
                desktop.MainWindow = view;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
