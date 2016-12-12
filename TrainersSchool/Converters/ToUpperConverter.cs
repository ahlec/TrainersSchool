using System;
using System.Globalization;
using System.Windows.Data;

namespace TrainersSchool.Converters
{
	public class ToUpperConverter : IValueConverter
	{
		public Object Convert( Object value, Type targetType, Object converterParameter, CultureInfo culture )
		{
			return value?.ToString().ToUpperInvariant();
		}

		public Object ConvertBack( Object value, Type targetType, Object converterParameter, CultureInfo culture )
		{
			throw new NotSupportedException();
		}
	}
}
