using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using FluentAvalonia.Styling;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Media;
using FluentAvalonia.UI.Windowing;
using Sonar.AutoSwitch.Pages;
using Sonar.AutoSwitch.Services.Win32;

namespace Sonar.AutoSwitch;

public partial class MainWindow : AppWindow
{
    private readonly Frame _frameView;

    public MainWindow()
    {
        InitializeComponent();
        _frameView = this.FindControl<Frame>("FrameView")!;
        _frameView.Navigate(typeof(Home));
        this.Closing += OnClosing;
    }

    private void OnClosing(object? sender, WindowClosingEventArgs e)
    {
        Hide();
        MemoryUtils.MinimizeFootprint();
        e.Cancel = true;
    }

    private void NavigationView_OnSelectionChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
    {
        _frameView.Navigate(e.SelectedItem switch
        {
            NavigationViewItem {Tag: "Home"} => typeof(Home),
            NavigationViewItem {Tag: "About"} => typeof(About),
            NavigationViewItem {Name: "SettingsItem"} => typeof(Settings),
            _ => throw new ArgumentOutOfRangeException()
        });
    }
}