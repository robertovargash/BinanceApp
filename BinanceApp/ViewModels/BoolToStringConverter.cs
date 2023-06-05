using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApp.ViewModels
{
  internal class BoolToStringConverter : IValueConverter
  {
    public object? Convert( object? value, Type targetType, object? parameter, CultureInfo culture )
    {
      if( ( bool )value )
      {
        return "BUY";
      }
      return "SELL";
    }

    public object? ConvertBack( object? value, Type targetType, object? parameter, CultureInfo culture )
    {
      throw new NotImplementedException();
    }
  }
}
