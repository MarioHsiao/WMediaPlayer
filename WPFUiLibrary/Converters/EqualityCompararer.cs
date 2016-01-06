﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFUiLibrary.Converters
{
    public class EqualityComparer : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values[0] == values[1];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}