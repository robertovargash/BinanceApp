using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BinanceApp.ViewModels;
using BinanceApp.Views;

namespace BinanceApp
{
  public partial class App : Application
  {
    public override void Initialize()
    {
      AvaloniaXamlLoader.Load( this );
    }

    public override void OnFrameworkInitializationCompleted()
    {
      if( ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop )
      {
        var viewmodel = new MainWindowViewModel();
        viewmodel.Start();
        desktop.MainWindow = new MainWindow
        {
          DataContext = viewmodel,
        };
      }

      base.OnFrameworkInitializationCompleted();
    }
  }
}