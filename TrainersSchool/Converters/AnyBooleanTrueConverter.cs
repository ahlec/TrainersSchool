using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace TrainersSchool.Converters
{
	public class AnyBooleanTrueConverter : IMultiValueConverter
	{
		public Object Convert( Object[] values, Type targetType, Object parameter, CultureInfo culture )
		{
			return values.Any( value => (Boolean) value );
		}

		public Object[] ConvertBack( Object value, Type[] targetTypes, Object parameter, CultureInfo culture )
		{
			throw new NotSupportedException();
		}
	}
}
