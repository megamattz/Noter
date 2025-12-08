namespace Noter.Views;

using System.Collections.ObjectModel;
using Noter.UseCases.UseCaseInterfaces;

public partial class NotesPage : ContentPage
{

	IViewNotesUseCase _viewNotesUseCase;

	public NotesPage(IViewNotesUseCase viewNotesUseCase)
	{
		InitializeComponent();
		_viewNotesUseCase = viewNotesUseCase;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await RefreshNotes();
	}

	private async Task RefreshNotes()
	{
		List<CoreBusiness.Note> notes = await _viewNotesUseCase.ExecuteAsync();
		ObservableCollection<CoreBusiness.Note> notesObservable = new ObservableCollection<CoreBusiness.Note>(notes);

		lstNotes.ItemsSource = notesObservable;

	}
}