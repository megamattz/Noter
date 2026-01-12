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

		public NotesPageViewModel(IViewNotesUseCase viewNotesUseCase)
		{
			_viewNotesUseCase = viewNotesUseCase;
			AddNewNoteCommand = new Command(async () => await NavigateToAddNotePage());
			OpenNoteCommand = new Command<Note>(async note => await NavigateToViewNotePage(note));
			EditNoteCommand =new Command<Note>(async note => await NavigateToEditNotePage(note));
			DeleteNoteCommand = new Command(async () => await DeleteNote());
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
			await Shell.Current.GoToAsync("//AddNotePage");
		}

		private async Task NavigateToEditNotePage(Note selectedNote)
		{
			throw new NotImplementedException();
		}

		private async Task DeleteNote()
		{
			throw new NotImplementedException();
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
