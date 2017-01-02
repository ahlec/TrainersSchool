using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace TrainersSchool.Converters
{
	[ContentProperty( nameof( Converter ) )]
	public class DebugConverter : IValueConverter
	{
		public Object Convert( Object value, Type targetType, Object parameter, CultureInfo culture )
		{
			Object convertedValue = Converter.Convert( value, targetType, parameter, culture );
			return convertedValue;
		}

		public Object ConvertBack( Object value, Type targetType, Object parameter, CultureInfo culture )
		{
			Object convertedValue = Converter.ConvertBack( value, targetType, parameter, culture );
			return convertedValue;
		}

		public IValueConverter Converter { get; set; }
	}
}
