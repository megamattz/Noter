using CommunityToolkit.Maui.Views;
using Noter.ViewModels;

namespace Noter.Views.Popups;

public partial class FilterAndSortPopup : Popup
{
	private readonly FilterAndSortViewModel _viewModel;

	public FilterAndSortPopup(FilterAndSortViewModel viewModel)
	{
		InitializeComponent();

		_viewModel = viewModel;
		BindingContext = _viewModel;

		Opened += FilterAndSortPopup_Opened;
	}

	private void FilterAndSortPopup_Opened(object? sender, CommunityToolkit.Maui.Core.PopupOpenedEventArgs e)
	{
		if (_viewModel is FilterAndSortViewModel vm)
		{
			vm.SetCurrentPopupView(this);
		}
	}
}