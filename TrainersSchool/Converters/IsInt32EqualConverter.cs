using System;
using System.Globalization;
using System.Windows.Data;

namespace TrainersSchool.Converters
{
	public class IsInt32EqualConverter : IValueConverter
	{
		public Object Convert( Object value, Type targetType, Object converterParameter, CultureInfo culture )
		{
			return ( (Int32) value == NumberToEqual );
		}

		public Object ConvertBack( Object value, Type targetType, Object converterParameter, CultureInfo culture )
		{
			throw new NotSupportedException();
		}

		public Int32 NumberToEqual { get; set; }
	}
}
