using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;

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
            var sv = this.FindControl<ScrollViewer>("scrollview");
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
        private void ScrollViewChanged(object? sender, ScrollChangedEventArgs args)
        {
            if (sender is ScrollViewer sv)
            {
                var overlay = sv.FindControl<StackPanel>("overlay");
                //Cant use VerticalScrollBarVisibility because it is set to Auto
                if ((sv.Extent.Height - sv.Height) < 1)
                {
                    overlay.Background = new SolidColorBrush(Color.Parse("Transparent"));
                    return;
                }
                var overlaycolor = Color.FromArgb(124, 200, 200, 0);
                var max = sv.Extent.Height - sv.Height;
                overlay.Height = sv.Height;
                var current = sv.Offset.Y;
                var gradient = new LinearGradientBrush();
                gradient.StartPoint = RelativePoint.Parse("0%,0%");
                gradient.EndPoint = RelativePoint.Parse("0%,100%");
                var percent = 15 / overlay.Height;
                if (current > 0)
                {
                    gradient.GradientStops.Add(new GradientStop() { Color = overlaycolor, Offset = 0.0 });
                    gradient.GradientStops.Add(new GradientStop() { Color = Color.Parse("Transparent"), Offset = percent });
                }
                if (current < max)
                {
                    gradient.GradientStops.Add(new GradientStop() { Color = Color.Parse("Transparent"), Offset = 1 - percent });
                    gradient.GradientStops.Add(new GradientStop() { Color = overlaycolor, Offset = 1.0 });
                }
                overlay.Background = gradient;
            }
        }
    }
}
