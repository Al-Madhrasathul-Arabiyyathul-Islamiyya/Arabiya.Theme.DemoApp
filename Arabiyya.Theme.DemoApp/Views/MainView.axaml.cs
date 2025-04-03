using System;
using Arabiyya.Theme.DemoApp.ViewModels;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Arabiyya.Theme.DemoApp.Views;
public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();

        if (Design.IsDesignMode)
        {
            return;
        }

        var serviceProvider = (ServiceProvider)Avalonia.Application.Current!.Resources["ServiceProvider"]!;

        ArgumentNullException.ThrowIfNull(serviceProvider);

        DataContext = new MainViewModel(serviceProvider);
    }
}
