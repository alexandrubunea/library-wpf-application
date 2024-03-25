using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Library_Application.Utils
{
    class ErrorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable errors)
            {
                foreach (var error in errors)
                {
                    if (error is ValidationError validationError && validationError.ErrorContent?.ToString() is { } errorContent && !string.IsNullOrEmpty(errorContent))
                    {
                        return errorContent;
                    }
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
