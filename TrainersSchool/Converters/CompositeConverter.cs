using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace TrainersSchool.Converters
{
	public class CompositeConverter : List<IValueConverter>, IValueConverter
	{
		public Object Convert( Object value, Type targetType, Object converterParameter, CultureInfo culture )
		{
			Object currentValue = value;

			foreach ( IValueConverter converter in this )
			{
				currentValue = converter.Convert( currentValue, targetType, converterParameter, culture );
			}

			return currentValue;
		}

		public Object ConvertBack( Object value, Type targetType, Object converterParameter, CultureInfo culture )
		{
			throw new NotImplementedException();
		}
	}
}
