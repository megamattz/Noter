using CommunityToolkit.Maui.Views;
using Noter.ViewModels;
using Noter.Models;
using CommunityToolkit.Maui.Core;

namespace Noter.Views.Popups;

public partial class FilterAndSortPopup : Popup
{
	private readonly FilterAndSortPopupViewModel _viewModel;

	public FilterAndSortPopup(FilterAndSortPopupViewModel viewModel)
	{
		InitializeComponent();

		_viewModel = viewModel;
		BindingContext = _viewModel;

		Opened += FilterAndSortPopup_Opened;
	}

	private void FilterAndSortPopup_Opened(object? sender, CommunityToolkit.Maui.Core.PopupOpenedEventArgs e)
	{
		if (_viewModel is FilterAndSortPopupViewModel vm)
		{
			vm.SetCurrentPopupView(this);
		}
	}	
}