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

		private IViewNotesUseCase _viewNotesUseCase;

		public ICommand AddNewNoteCommand { get; }

		public NotesPageViewModel(IViewNotesUseCase viewNotesUseCase)
		{
			_viewNotesUseCase = viewNotesUseCase;
			AddNewNoteCommand = new Command(async () => await NavigateToAddNotePage());
		}

		public ObservableCollection<Note> Notes
		{
			get => _notes;
			set
			{
				_notes = value;
				OnPropertyChanged();
			}
		}
	
		private async Task NavigateToAddNotePage()
		{
			await Shell.Current.GoToAsync("//AddNotePage");
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
