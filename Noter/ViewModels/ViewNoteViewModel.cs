using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Noter.CoreBusiness;
using Noter.UseCases.UseCaseInterfaces;

namespace Noter.ViewModels
{
	[QueryProperty(nameof(NoteIdQueryParam), "noteId")]
	public class ViewNoteViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public ICommand BackCommand { get; }

		private IViewNoteUseCase _viewNoteUseCase;
		private string? _noteIdQueryParam;

		private Note? _note;

		public Note? Note
		{ 
			get
			{
				return _note;
			}
			set

			{
				_note = value;
				OnPropertyChanged();
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
				if (value != null) 
				{
					_noteIdQueryParam = value;

					// Query Param gets passed in as a string and .NET maui does not support automatic conversion like ASP would
					// so we have to convert it manually.
					if (int.TryParse(_noteIdQueryParam, out int noteId))
					{
						_ = LoadNoteById(noteId);
						OnPropertyChanged();
					}
				}
			}
		}

		public ViewNoteViewModel(IViewNoteUseCase viewNoteUseCase)
		{
			_viewNoteUseCase = viewNoteUseCase;
			BackCommand = new Command(async () => await NavigateToViewNotePage());
		}

		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public async Task LoadNoteById(int noteId)
		{
			Note = await _viewNoteUseCase.ExecuteAsync(noteId);
		}

		public async Task NavigateToViewNotePage()
		{
			await Shell.Current.GoToAsync($"//NotesPage");			
		}
	}
}
