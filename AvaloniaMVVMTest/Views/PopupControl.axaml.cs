using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using ReactiveUI.Fody.Helpers;

namespace AvaloniaMVVMTest.Views
{
    public partial class PopupControl : UserControl
    {
        public PopupControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        [Reactive]
        public string? Password { get; set; }

        public void OKClicked()
        {

        }

        public void CancelClicked()
        {

        }

        public void AKeyUpEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                CancelClicked();
            if(e.Key == Key.Enter)
                OKClicked();
        }
    }
}
