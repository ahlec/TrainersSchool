using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace TrainersSchool.Converters
{
	public class EnumToStringConverter : IValueConverter
	{
		public Object Convert( Object value, Type targetType, Object converterParameter, CultureInfo culture )
		{
			Enum val = (Enum) value;
			DescriptionAttribute description = val.GetAttributeOfType<DescriptionAttribute>();
			if ( description != null )
			{
				return description.Description;
			}
			return val.ToString();
		}

		public Object ConvertBack( Object value, Type targetType, Object converterParameter, CultureInfo culture )
		{
			throw new NotSupportedException();
		}
	}
}
