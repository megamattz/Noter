using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Noter.CoreBusiness;
using Noter.UseCases.UseCaseInterfaces;

namespace Noter.ViewModels
{
	public class AddEditNotePageViewModel : INotifyPropertyChanged
	{
		
		public event PropertyChangedEventHandler? PropertyChanged;
		public ICommand SaveNoteCommand { get; }
		public Note Note { get; set; } = new Note();

		private readonly IAddNoteUseCase _addNoteUseCase;
		private readonly IEditNoteUseCase _editNoteUseCase;

		private string _noteTitle = "";
		private string _noteText = "";


		public string NoteTitle
		{
			get
			{
				return _noteTitle;
			}
			set
			{
				_noteTitle = value;
				OnPropertyChanged();
			}
		}

		public string NoteText
		{
			get
			{
				return _noteText;
			}
			set
			{
				_noteText = value;
				OnPropertyChanged();
			}
		}

		public AddEditNotePageViewModel(IAddNoteUseCase addNoteUseCase, IEditNoteUseCase editNoteUseCase)
		{
			_addNoteUseCase = addNoteUseCase;
			_editNoteUseCase = editNoteUseCase;
			SaveNoteCommand = new Command(async () => await SaveNote());
		}

		private async Task SaveNote()
		{
			// Save the new note
			Note note = new Note()
			{
				NoteTitle = _noteTitle,
				NoteText = _noteText,
			};

			await _addNoteUseCase.ExecuteAsync(note);

			// Navigate back to the notes list
			await Shell.Current.GoToAsync("//NotesPage");
		}

		protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
