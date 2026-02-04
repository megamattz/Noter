using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noter.Converters
{
	public class UTCToLocalTimeConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value is DateTime dateTime)
			{
				if (dateTime.Kind == DateTimeKind.Local)
				{
					return dateTime; 
				}

				if (dateTime.Kind == DateTimeKind.Utc || dateTime.Kind == DateTimeKind.Unspecified)
				{
					return dateTime.ToLocalTime();
				}
			}

			return value;
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
