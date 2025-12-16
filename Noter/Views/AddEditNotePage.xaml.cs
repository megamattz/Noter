using Noter.ViewModels;

namespace Noter.Views;

public partial class AddNotePage : ContentPage
{
	private AddEditNotePageViewModel _viewModel;

	public AddNotePage(AddEditNotePageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;

		BindingContext = _viewModel;
	}
}