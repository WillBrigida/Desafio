using System;

namespace BrownianMotion;

public static class ShellExtensions
{
    public static async Task ToastAlert(this Shell shell, string message, TimeSpan timeSpan = default)
    {
        if (timeSpan == default)
        {
            timeSpan = TimeSpan.FromSeconds(2);
        }

        var toast = new Border
        {
            Opacity = 0,
            BackgroundColor = Colors.White,
            Margin = new Thickness(5, 10),
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Start,
            MinimumHeightRequest = 100,
            MaximumWidthRequest = 500,
            StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 7 }
        };
        
        var labelMessage = new Label
        {
            Margin = new Thickness(10, 5),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.Black,
            FontSize = 22,
            Text = message
        };

        var grid = new Grid();
        grid.Add(labelMessage);

        toast.Content = grid;

        var absoluteLayout = new AbsoluteLayout { InputTransparent = true };
        AbsoluteLayout.SetLayoutBounds(toast, new Rect(0, 0, 1, 1));
        AbsoluteLayout.SetLayoutFlags(toast, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.All);

        var currentPage = shell.CurrentPage as ContentPage;
        
        if (currentPage?.Content is Layout layout)
        {
            absoluteLayout.Children.Add(toast);
            layout.Children.Add(absoluteLayout);

            var timer = CreateTimer(toast, timeSpan, layout);
            timer.Start();
            toast.TranslationY = -50;

            await Task.WhenAll(
                toast.FadeTo(1, 500, Easing.CubicInOut),
                toast.TranslateTo(0, 0, 250, Easing.CubicInOut)
            );
        }
    }

    private static IDispatcherTimer CreateTimer(View toast, TimeSpan timeSpan, Layout layout)
    {
        var timer = Application.Current!.Dispatcher.CreateTimer();
        timer.Interval = timeSpan;
        timer.Tick += async (s, e) =>
        {
            await Task.WhenAll(
                toast.FadeTo(0, 150, Easing.CubicInOut),
                toast.TranslateTo(0, -50, 200, Easing.CubicInOut));
            layout.Remove(toast.Parent as Layout);
            timer.Stop();
        };
        return timer;
    }
}
