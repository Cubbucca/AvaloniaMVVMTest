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
            if (e.MouseButton == MouseButton.Left)
            {
                var control = sender as Control;
                if(sender is Control rec)
                {
                    lastdraged = rec;
                }
                control.Focusable = false;
                DataObject dragData = new DataObject();
                dragData.Set("task", control);
                var result = await DragDrop.DoDragDrop(e, dragData, DragDropEffects.Move);
            }
        }
        private int overs = 0;
        private void DragOver(object? sender, DragEventArgs e)
        {
            e.DragEffects = e.DragEffects & (DragDropEffects.Move);
            if(lastdraged is Control rec)
            {
                if (lastdraged is Label lb && lb.Name == "ShowOvers")
                {
                    lb.Content = $"{++overs}";
                }
                var x = e.GetPosition(sender as Control).X;
                var y = e.GetPosition(sender as Control).Y;
                Canvas.SetLeft(rec, x-22);
                Canvas.SetTop(rec, y-22);
            }
        }
    }
}
