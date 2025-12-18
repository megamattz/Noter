using Noter.ViewModels;

namespace Noter.Views;

public partial class ViewNotePage : ContentPage
{
	private readonly ViewNoteViewModel _viewModel;

	public ViewNotePage(ViewNoteViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;

		// Setup the binding context
		BindingContext = _viewModel;
	}
}