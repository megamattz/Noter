using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Noter.CoreBusiness;
using Noter.Enums;
using Noter.UseCases.UseCaseInterfaces;

namespace Noter.ViewModels
{
	[QueryProperty(nameof(NoteIdQueryParam), "noteId")]
	[QueryProperty(nameof(SourcePageAsString), "sourcePage")]
	public class AddEditNotePageViewModel : INotifyPropertyChanged
	{
		
		public event PropertyChangedEventHandler? PropertyChanged;
		public ICommand SaveNoteCommand { get; }
		public ICommand CancelCommand { get; }

		private readonly IAddNoteUseCase _addNoteUseCase;
		private readonly IEditNoteUseCase _editNoteUseCase;
		private readonly IViewNoteUseCase _viewNoteUseCase;

		private string _noteTitle = "";
		private string _noteText = "";
		private string? _noteIdQueryParam;
		private SourcePage? _sourcePage;
		private int? _editingNoteId = null;
		private NoteCategories _noteCategory = NoteCategories.General;


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

		public NoteCategories Category
		{
			get
			{ 
				return _noteCategory; 
			}
			set
			{
				_noteCategory = value;
				OnPropertyChanged();
			}
		}

		public SourcePage? SourcePage
		{
			get
			{
				return _sourcePage;
			}
		}

		public string? SourcePageAsString
		{
			get
			{
				return _sourcePage.ToString(); ;
			}

			set
			{
				if (Enum.TryParse<SourcePage>(value, ignoreCase: true, out var parsed))
				{
					_sourcePage = parsed;
				}
				else
				{
					_sourcePage = null;
					Console.WriteLine($"Unkown Source Page {value}");
				}

				OnPropertyChanged(nameof(SourcePage));
				OnPropertyChanged(nameof(SourcePageAsString));
			}
		}

		public string? NoteIdQueryParam
		{
			get
			{
				return _noteIdQueryParam;
			}
			set
			{
				_noteIdQueryParam = value;

				if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, out int noteId) && noteId > 0)
				{
					// Query Param gets passed in as a string and .NET maui does not support automatic conversion like ASP would
					// so we have to convert it manually.
					_ = LoadNoteById(noteId);
					OnPropertyChanged();					
				}
				else
				{
					ClearNote();
				}
			}
		}

		public AddEditNotePageViewModel(IAddNoteUseCase addNoteUseCase, IEditNoteUseCase editNoteUseCase, IViewNoteUseCase viewNoteUseCase)
		{
			_addNoteUseCase = addNoteUseCase;
			_editNoteUseCase = editNoteUseCase;
			_viewNoteUseCase = viewNoteUseCase;

			SaveNoteCommand = new Command(async () => await SaveNote());
			CancelCommand = new Command(async () => await Cancel());
		}

		public async Task LoadNoteById(int noteId)
		{
			Note note = await _viewNoteUseCase.ExecuteAsync(noteId);

			_editingNoteId = note.NoteId;
			NoteTitle = note.NoteTitle ?? "";
			NoteText = note.NoteText ?? "";
		}

		public void ClearNote()
		{
			_editingNoteId = null;
			NoteTitle = "";
			NoteText = "";
		}

		private async Task Cancel()
		{
			// Navigate back to the notes list
			await Shell.Current.GoToAsync("//NotesPage");
		}

		private async Task SaveNote()
		{
			// New Note
			if (_editingNoteId == null)
			{
				// Save the new note
				Note note = new Note()
				{
					NoteTitle = _noteTitle,
					NoteText = _noteText,
					NoteCategory = _noteCategory,
				};

				await _addNoteUseCase.ExecuteAsync(note);
			}
			else
			{
				Note updatedNote = new Note()
				{
					NoteTitle = _noteTitle,
					NoteText = _noteText,
					NoteId = _editingNoteId.Value,
					NoteCategory = _noteCategory
				};

				await _editNoteUseCase.ExecuteAsync(updatedNote);
			}

			// Navigate back to the notes list or the view note page if thats where we came from
			if (_sourcePage == Enums.SourcePage.ViewNotePage && _editingNoteId != null)
			{
				await Shell.Current.GoToAsync($"//ViewNotePage?noteId={_editingNoteId}");
			}
			else
			{
				await Shell.Current.GoToAsync("//NotesPage");
			}
		}

		protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
