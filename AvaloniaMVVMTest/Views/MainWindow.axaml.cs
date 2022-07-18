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
                var overlaycolor = Color.FromArgb(124, 75, 75, 0);
                var max = sv.Extent.Height - sv.Height;
                overlay.Height = sv.Height;
                var current = sv.Offset.Y;
                if(current > 0 && current < max)
                {
                    var gradient = new LinearGradientBrush();
                    gradient.StartPoint = RelativePoint.Parse("0%,0%");
                    gradient.EndPoint = RelativePoint.Parse("0%,100%");
                    gradient.GradientStops.Add(new GradientStop() { Color = overlaycolor, Offset = 0.0 });
                    gradient.GradientStops.Add(new GradientStop() { Color = Color.Parse("Transparent"), Offset = 0.2 });
                    gradient.GradientStops.Add(new GradientStop() { Color = Color.Parse("Transparent"), Offset = 0.8 });
                    gradient.GradientStops.Add(new GradientStop() { Color = overlaycolor, Offset = 1.0 });
                    overlay.Background = gradient;                }
                if(current == max)
                {
                    var gradient = new LinearGradientBrush();
                    gradient.StartPoint = RelativePoint.Parse("0%,0%");
                    gradient.EndPoint = RelativePoint.Parse("0%,100%");
                    gradient.GradientStops.Add(new GradientStop() { Color = overlaycolor, Offset = 0.0 });
                    gradient.GradientStops.Add(new GradientStop() { Color = Color.Parse("Transparent"), Offset = 0.2 });
                    overlay.Background = gradient;
                }
                if (current == 0)
                {
                    var gradient = new LinearGradientBrush();
                    gradient.StartPoint = RelativePoint.Parse("0%,0%");
                    gradient.EndPoint = RelativePoint.Parse("0%,100%");
                    gradient.GradientStops.Add(new GradientStop() { Color = Color.Parse("Transparent"), Offset = 0.8 });
                    gradient.GradientStops.Add(new GradientStop() { Color = overlaycolor, Offset = 1.0 });
                    overlay.Background = gradient;
                }
            }
        }
    }
}
