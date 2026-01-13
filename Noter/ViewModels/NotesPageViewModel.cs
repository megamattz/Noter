using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Noter.CoreBusiness;
using Noter.UseCases.UseCaseInterfaces;

namespace Noter.ViewModels
{
	public class NotesPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private ObservableCollection<Note> _notes = new ObservableCollection<Note>();
		private Note? _selectedNote;


		private readonly IViewNotesUseCase _viewNotesUseCase;
		private readonly IDeleteNoteUseCase _deleteNoteUseCase;

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

		public ICommand AddNewNoteCommand { get; }
		public ICommand OpenNoteCommand { get; }

		public ICommand EditNoteCommand { get; }
		public ICommand DeleteNoteCommand { get; }

		public NotesPageViewModel(IViewNotesUseCase viewNotesUseCase, IDeleteNoteUseCase deleteNoteUseCase)
		{
			_viewNotesUseCase = viewNotesUseCase;
			_deleteNoteUseCase = deleteNoteUseCase;

			AddNewNoteCommand = new Command(async () => await NavigateToAddNotePage());
			OpenNoteCommand = new Command<Note>(async note => await NavigateToViewNotePage(note));
			EditNoteCommand =new Command<Note>(async note => await NavigateToEditNotePage(note));
			DeleteNoteCommand = new Command<Note>(async note => await DeleteNote(note));
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
		
		private async Task NavigateToAddNotePage()
		{
			await Shell.Current.GoToAsync("//AddEditNotePage");
		}

		private async Task NavigateToEditNotePage(Note selectedNote)
		{
			throw new NotImplementedException();
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

		public async Task LoadNotesAsync()
		{
			List<Note> notes = await _viewNotesUseCase.ExecuteAsync();
			Notes = new ObservableCollection<Note>(notes);
		}

		protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
