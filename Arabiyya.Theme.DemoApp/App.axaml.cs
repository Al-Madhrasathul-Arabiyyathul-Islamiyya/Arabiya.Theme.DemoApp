﻿using Arabiyya.Theme.DemoApp.ViewModels;
using Arabiyya.Theme.DemoApp.Views;
using Arabiyya.Theme.Navigation.Extensions;
using Arabiyya.Theme.Navigation.Services;
using Arabiyya.Theme.ThemeServices;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

namespace Arabiyya.Theme.DemoApp;
public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        switch (ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime desktop:
                // Avoid duplicate validations from both Avalonia and the CommunityToolkit.
                // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
                DisableAvaloniaDataAnnotationValidation();

                _ = ConfigureServices();

                desktop.MainWindow = new MainWindow();

                desktop.MainWindow.InitializeTheme();
                break;
            case ISingleViewApplicationLifetime singleViewPlatform:
                _ = ConfigureServices();

                singleViewPlatform.MainView = new MainView();

                break;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }

    private ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Register navigation services
        services.AddNavigationWithDI();

        // Register your views and view models
        services.AddTransient<ColorPaletteViewModel>();
        services.AddTransient<ColorPaletteView>();
        services.AddTransient<TypographyViewModel>();
        services.AddTransient<TypographyView>();
        services.AddTransient<ButtonsViewModel>();
        services.AddTransient<ButtonsView>();
        services.AddTransient<GlassPanelsViewModel>();
        services.AddTransient<GlassPanelsView>();
        services.AddTransient<TextInputsViewModel>();
        services.AddTransient<TextInputsView>();
        services.AddTransient<InputsViewModel>();
        services.AddTransient<InputsView>();
        services.AddTransient<TabControlViewModel>();
        services.AddTransient<TabControlView>();
        services.AddTransient<GradientsViewModel>();
        services.AddTransient<GradientsView>();
        services.AddTransient<CardsViewModel>();
        services.AddTransient<CardsView>();
        services.AddTransient<GlassCardsViewModel>();
        services.AddTransient<GlassCardsView>();

        // Build service provider
        var serviceProvider = services.BuildServiceProvider();

        // Diagnostic check
        var navService = serviceProvider.GetService<INavigationService>();
        var viewFactory = serviceProvider.GetService<IViewFactory>();
        System.Diagnostics.Debug.WriteLine($"Navigation service type: {navService?.GetType().Name}");
        System.Diagnostics.Debug.WriteLine($"View factory type: {viewFactory?.GetType().Name}");

        // Store the service provider
        this.Resources.Add("ServiceProvider", serviceProvider);

        return serviceProvider;
    }
}
