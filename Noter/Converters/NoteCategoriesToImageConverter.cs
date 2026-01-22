using System.Globalization;
using Noter.CoreBusiness;

namespace Noter.Converters
{
	public class NoteCategoriesToImageConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value == null || value is not NoteCategories)
			{
				return "notepad.png";
			}
			
			NoteCategories noteCategories = (NoteCategories)value;

			return noteCategories switch
			{
				NoteCategories.General => "notepad.png",
				NoteCategories.Starred => "star.png",
				NoteCategories.Tick => "tick.png",
				NoteCategories.Game => "game.png",
				_ => "notepad.png"
			};
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
