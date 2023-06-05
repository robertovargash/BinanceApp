using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApp.ViewModels
{
  public class OpositeBoolToVisibilityConverter : IValueConverter
  {

    public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
    {
      bool bValue = false;
      if( value is bool )
      {
        bValue = ( bool )value;
      }

      return ( bValue ) ? FalseValue : TrueValue;
    }

    public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
    {
      return value as Visibility? == FalseValue;
    }
  }

}
