using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace AvaloniaMVVMTest.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            AddHandler(DragDrop.DragOverEvent, DragOver);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        private Control? lastdraged;
        private async void DoDrag(object? sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                if(sender is Control control)
                {
                    lastdraged = control;
                    DataObject dragData = new DataObject();
                    dragData.Set("control", control);
                    var result = await DragDrop.DoDragDrop(e, dragData, DragDropEffects.Move);
                }
            }
        }
        private int overs = 0;
        private void DragOver(object? sender, DragEventArgs e)
        {
            if(lastdraged is Control rec)
            {
                if (lastdraged is Label lb && lb.Name == "ShowOvers")
                {
                    lb.Content = $"{++overs}";
                }
                var x = e.GetPosition(this).X;
                var y = e.GetPosition(this).Y;
                Canvas.SetLeft(rec, x - 22);
                Canvas.SetTop(rec, y - 22);
            }
        }
    }
}
