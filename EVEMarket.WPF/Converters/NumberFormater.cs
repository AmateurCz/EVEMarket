using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EVEMarket.WPF.Converters
{
    public class NumberFormater : IValueConverter
    {
        public NumberFormatInfo NumberFormat { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(string))
                throw new NotSupportedException("This converter can covert only to string!");

            if (value is double d)
            {
                return d.ToString("N", NumberFormat);
            }

            throw new NotImplementedException($"Conversion for type {value.GetType()} is not implemented.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                if(targetType == typeof(double))
                {
                    return double.Parse(str, NumberFormat);
                }

                throw new NotImplementedException($"Backward conversion for type {value.GetType()} is not implemented.");
            }

            throw new NotSupportedException("This converter can covert back only from string!");
        }
    }
}
