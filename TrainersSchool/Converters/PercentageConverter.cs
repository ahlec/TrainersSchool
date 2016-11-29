using System;
using System.Globalization;
using System.Windows.Data;

namespace TrainersSchool.Converters
{
	public class PercentageConverter : IMultiValueConverter
	{
		public Object Convert( Object[] values, Type targetType, Object converterParameter, CultureInfo culture )
		{
			Int32 numerator = (Int32) values[0];
			Int32 denominator = (Int32) values[1];

			if ( denominator == 0 )
			{
				return "UNDEFINED";
			}

			Double percentage = Math.Round( ( (Double) numerator / denominator ) * 100D, 2 );
			return percentage.ToString();
		}

		public Object[] ConvertBack( Object value, Type[] targetTypes, Object converterParameter, CultureInfo culture )
		{
			throw new NotSupportedException();
		}
	}
}
