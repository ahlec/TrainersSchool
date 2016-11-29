using System;
using System.Globalization;
using System.Windows.Data;

namespace TrainersSchool.Converters
{
	public class InvertBooleanConverter : IValueConverter
	{
		public Object Convert( Object value, Type targetType, Object converterParameter, CultureInfo culture )
		{
			return !( (Boolean) value );
		}

		public Object ConvertBack( Object value, Type targetType, Object converterParameter, CultureInfo culture )
		{
			return !( (Boolean) value );
		}
	}
}
