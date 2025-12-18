namespace Noter.Views;

using System.Windows.Input;
using Noter.ViewModels;

public partial class NotesPage : ContentPage
{
	private readonly NotesPageViewModel _viewModel;

	public NotesPage(NotesPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;

		// Setup the binding context
		BindingContext = _viewModel;
	}

	private async void btnAdd_Clicked(object? sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//AddNotePage");
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.LoadNotesAsync();
	}
}