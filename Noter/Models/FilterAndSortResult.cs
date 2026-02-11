using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noter.CoreBusiness;

namespace Noter.Models
{
	public class FilterAndSortResult
	{
		public SortingColumn SortingColumn { get; set; }
		public SortDirection SortDirection { get; set; }

		public NoteCategories[] SelectedCategories { get; set; } = new NoteCategories[0];

		public FilterAndSortResult()
		{
			// Default sort and filter options, show last updated on top and don't filter out anything
			SortingColumn = SortingColumn.DateModified;
			SortDirection = SortDirection.Descending;
			SelectedCategories = new NoteCategories[]
			{
				NoteCategories.General,
				NoteCategories.Starred,
				NoteCategories.Tick,
				NoteCategories.Game
			};
		}
	}
}
