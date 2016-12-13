using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TrainersSchool.Converters
{
	public class BooleanToBrushConverter : IValueConverter
	{
		public Object Convert( Object value, Type targetType, Object converterParameter, CultureInfo culture )
		{
			return ( (Boolean) value ? TrueBrush : FalseBrush );
		}

		public Object ConvertBack( Object value, Type targetType, Object converterParameter, CultureInfo culture )
		{
			throw new NotSupportedException();
		}

		public Brush TrueBrush { get; set; }

		public Brush FalseBrush { get; set; }
	}
}
