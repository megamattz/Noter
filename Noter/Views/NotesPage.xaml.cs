namespace Noter.Views;

using System.Collections.ObjectModel;
using System.ComponentModel;
using Noter.UseCases.UseCaseInterfaces;

public partial class NotesPage : ContentPage, INotifyPropertyChanged
{
	public ObservableCollection<CoreBusiness.Note> NotesCollection
	{
		get
		{
			return _notesCollection;
		}
		set
		{
			_notesCollection = value;
			OnPropertyChanged();
		}
	}

	public new event PropertyChangedEventHandler? PropertyChanged;

	private ObservableCollection<CoreBusiness.Note> _notesCollection = new ObservableCollection<CoreBusiness.Note>();
	private IViewNotesUseCase _viewNotesUseCase;
	

	public NotesPage(IViewNotesUseCase viewNotesUseCase)
	{
		InitializeComponent();
		_viewNotesUseCase = viewNotesUseCase;

		BindingContext = this;

		// Setup the buttons
		btnAdd.Clicked += btnAdd_Clicked;
	}

	private async void btnAdd_Clicked(object? sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddNotePage());
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await RefreshNotes();
	}

	protected new void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	private async Task RefreshNotes()
	{
		List<CoreBusiness.Note> notes = await _viewNotesUseCase.ExecuteAsync();
		NotesCollection = new ObservableCollection<CoreBusiness.Note>(notes);
	}
}