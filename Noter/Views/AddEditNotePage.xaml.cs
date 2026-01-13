using Noter.ViewModels;

namespace Noter.Views;

public partial class AddEditNotePage : ContentPage
{
	private AddEditNotePageViewModel _viewModel;

	public AddEditNotePage(AddEditNotePageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;

		BindingContext = _viewModel;
	}
}