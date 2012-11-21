using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace ToolingManWPF.Helper
{
    public class OverValidateConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool notover= true;
            switch ((string)parameter)
            {
                case "Date":
                    {
                        double days;
                        DateTime date;
                        if (DateTime.TryParse(values[1].ToString(), out date) && double.TryParse(values[0].ToString(), out days))
                        {
                            notover = DateTime.Now < date.AddDays(days);
                        }

                        break;
                    }
                case "Numerics":
                    {
                        double value0, value1;
                        if (double.TryParse(values[0].ToString(), out value0) && double.TryParse(values[1].ToString(), out value1))
                        {
                            notover = value0 > value1;
                        }
                        break;
                    }
                case "Numeric":
                    {
                        double value0;
                        if (double.TryParse(values[0].ToString(), out value0))
                        {
                            notover = value0 >= 0;
                        }
                        break;
                    }
                default:
                    notover = false;
                    break;
            }
            return notover ? Visibility.Collapsed : Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
