using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SchoolManagementSystem.Helpers
{
    /* Reference:
     * MVVM - Binding Multiple Radio Buttons To a single Enum Property in WPF
     * Source: http://www.amazedsaint.com/2009/08/mvvm-binding-multiple-radio-buttons-to.html
     * Author: Amazedsaint's Tech Journal
     * Date: August 19, 2009
     */

    public class RadioTeacherTypeConvertHelper : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value.ToString().Equals(parameter.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Return Either 'P' for Part Time or 'F' for Full Time
            return (char)parameter;
        }
    }
}
