using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Noter.CoreBusiness;
using Noter.Models;

namespace Noter.ViewModels
{
	public class FilterAndSortPopupViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public ICommand ClosePopup { get; }
		public ICommand SaveSortAndFilterOptions { get; }

		private Popup? _popup;

		private SortingColumn _sortingColumn;
		private SortDirection _sortDirection;

		private bool _showGeneralNotesCategory;
		private bool _showStarredNotesCategory;
		private bool _showListNotesCategory;
		private bool _showFunNotesCategory;

		public SortingColumn SortingColumn
		{
			get
			{
				return _sortingColumn;
			}
			set
			{
				_sortingColumn = value;
				OnPropertyChanged(nameof(SortingColumn));
			}
		}

		public SortDirection SortDirection
		{
			get
			{
				return _sortDirection;
			}
			set
			{
				_sortDirection = value;
				OnPropertyChanged(nameof(SortDirection));
			}
		}

		public bool ShowGeneralNotesCategory
		{
			get
			{
				return _showGeneralNotesCategory;
			}
			set
			{
				_showGeneralNotesCategory = value;
				OnPropertyChanged(nameof(ShowGeneralNotesCategory));
			}
		}

		public bool ShowStarredNotesCategory
		{
			get
			{
				return _showStarredNotesCategory;
			}
			set
			{
				_showStarredNotesCategory = value;
			}
		}

		public bool ShowListNotesCategory
		{
			get
			{
				return _showListNotesCategory;
			}
			set
			{
				_showListNotesCategory = value;
				OnPropertyChanged(nameof(ShowListNotesCategory));
			}
		}

		public bool ShowFunNotesCategory
		{
			get
			{
				return _showFunNotesCategory;
			}
			set
			{
				_showFunNotesCategory = value;
				OnPropertyChanged(nameof(ShowFunNotesCategory));
			}
		}

		public FilterAndSortPopupViewModel()
		{
			ClosePopup = new Command(async () => await CloseFilterAndSortPopup());
			SaveSortAndFilterOptions = new Command(async () => await SaveFilterAndSortOptions());
		}

		public void SetCurrentPopupView(Popup popup)
		{
			_popup = popup;
		}

		public async Task CloseFilterAndSortPopup()
		{
			if (_popup != null)
			{
				await _popup.CloseAsync();
			}
		}

		public async Task SaveFilterAndSortOptions()
		{
			if (_popup != null)
			{
				FilterAndSortResult filterAndSortResult = new FilterAndSortResult()
				{
					SortingColumn = SortingColumn,
					SortDirection = SortDirection,
					SelectedCategories = GetSelectedCategories(),
				};			

				await _popup.CloseAsync(filterAndSortResult);
			}
		}

		protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private NoteCategories[] GetSelectedCategories()
		{
			List<NoteCategories> noteCategories = new List<NoteCategories>();

			if (_showGeneralNotesCategory) {noteCategories.Add(NoteCategories.General); }
			if (_showStarredNotesCategory) { noteCategories.Add(NoteCategories.Starred); }
			if (_showListNotesCategory) { noteCategories.Add(NoteCategories.Tick); }
			if (_showFunNotesCategory) { noteCategories.Add(NoteCategories.Game); }

			return noteCategories.ToArray();
		}
	}
}
