using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Noter.CoreBusiness;
using Noter.Enums;
using Noter.UseCases.UseCaseInterfaces;
using CommunityToolkit.Maui.Views;
using Noter.Views.Popups;
using CommunityToolkit.Maui.Core;

namespace Noter.ViewModels
{
	public class NotesPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private ObservableCollection<Note> _notes = new ObservableCollection<Note>();
		private Note? _selectedNote;

		private string _searchTerm = "";

		private readonly IViewNotesUseCase _viewNotesUseCase;
		private readonly IDeleteNoteUseCase _deleteNoteUseCase;
		private readonly AboutPopupViewModel _aboutPopupViewModel;
		private readonly FilterAndSortViewModel _filterAndSortViewModel;

		private ContentPage? _currentPage;

		private readonly SemaphoreSlim _dbSemaphore = new SemaphoreSlim(1, 1);

		public Note? SelectedNote
		{
			get
			{
				return _selectedNote;
			}
			set
			{
				_selectedNote = value;
				OnPropertyChanged();
			}
		}

		public string SearchTerm
		{
			get
			{
				return _searchTerm;
			}
			set
			{
				_searchTerm = value;
				OnPropertyChanged();
			}
		}

		public ICommand AddNewNoteCommand { get; }
		public ICommand OpenNoteCommand { get; }

		public ICommand EditNoteCommand { get; }
		public ICommand DeleteNoteCommand { get; }

		public ICommand ShowAboutCommand { get; }

		public ICommand ShowFilterAndSortCommand { get; }

		public ICommand SearchCommand { get; }


		public NotesPageViewModel(IViewNotesUseCase viewNotesUseCase, IDeleteNoteUseCase deleteNoteUseCase,
			AboutPopupViewModel aboutPopupViewModel, FilterAndSortViewModel filterAndSortViewModel)
		{
			_viewNotesUseCase = viewNotesUseCase;
			_deleteNoteUseCase = deleteNoteUseCase;
			_aboutPopupViewModel = aboutPopupViewModel;
			_filterAndSortViewModel = filterAndSortViewModel;

			AddNewNoteCommand = new Command(async () => await NavigateToAddNotePage());
			OpenNoteCommand = new Command<Note>(async note => await NavigateToViewNotePage(note));
			EditNoteCommand =new Command<Note>(async note => await NavigateToEditNotePage(note));
			DeleteNoteCommand = new Command<Note>(async note => await DeleteNote(note));
			ShowAboutCommand = new Command(async () => await ShowAboutPopup());
			ShowFilterAndSortCommand = new Command(async () => await ShowFilterAndSortPopup());
			SearchCommand = new Command<string>(async searchTerm => await LoadNotesList(searchTerm));
		}

		public ObservableCollection<Note> Notes
		{
			get => _notes;
			set
			{
				_notes = value;
				OnPropertyChanged();
				SelectedNote = null;
			}
		}

		public void SetCurrentPage(ContentPage page)
		{
			_currentPage = page ?? throw new ArgumentNullException(nameof(page));
		}

		private async Task NavigateToAddNotePage()
		{
			await Shell.Current.GoToAsync($"//AddEditNotePage?sourcePage={SourcePage.ListPage.ToString()}");
		}

		private async Task NavigateToEditNotePage(Note selectedNote)
		{
			await Shell.Current.GoToAsync($"//AddEditNotePage?noteId={selectedNote.NoteId}&sourcePage={SourcePage.ListPage.ToString()}");
		}

		private async Task ShowAboutPopup()
		{
			if (_currentPage != null)
			{
				Popup popup = new AboutPopup(_aboutPopupViewModel);
				await _currentPage.ShowPopupAsync(popup);
			}
		}

		private async Task ShowFilterAndSortPopup()
		{
			if (_currentPage != null)
			{
				Popup popup = new FilterAndSortPopup(_filterAndSortViewModel);
				await _currentPage.ShowPopupAsync(popup);
			}
		}

		private async Task DeleteNote(Note noteToDelete)
		{
			Page? currentPage = Application.Current?.Windows.FirstOrDefault()?.Page;

			if (currentPage == null)
			{
				// Should only happen in rare edge cases
				System.Diagnostics.Debug.WriteLine("Cannot show delete confirmation - no active window");
				return;
			}

			bool confirmed = await currentPage.DisplayAlert(
				"Confirm Delete",
				$"Delete \"{noteToDelete.NoteTitle}\"? This cannot be undone.",
				"Delete",   
				"Cancel");

			if (confirmed) 
			{
				// Delete the note from the data store
				bool success = await _deleteNoteUseCase.ExecuteAsync(noteToDelete.NoteId);

				if (!success)
				{
					await currentPage.DisplayAlert(
						 "Error",
						 "Failed to delete the note. Please try again.",
						 "OK");
				}

				// Delete the note from the collection so you can see it updated on the UI
				_notes.Remove(noteToDelete);
			}
		}

		public async Task NavigateToViewNotePage(Note selectedNote)
		{
			if (selectedNote != null)
			{
				await Shell.Current.GoToAsync($"//ViewNotePage?noteId={selectedNote.NoteId}");
			}
		}

		public async Task LoadNotesList(string searchTerm = "")
		{
			// Prevent concurrency issues with the DB as it only supports one operation at a time
			await _dbSemaphore.WaitAsync();

			try
			{
				List<Note> notes = await _viewNotesUseCase.ExecuteAsync(searchTerm, null, SortingColumn.DateModified, SortDirection.Descending);
				Notes = new ObservableCollection<Note>(notes);
			}
			finally
			{
				{
					_dbSemaphore.Release();
				}
			}			
		}

		protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
