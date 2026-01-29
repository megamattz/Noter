using CommunityToolkit.Maui.Views;
using Noter.ViewModels;

namespace Noter.Views.Popups;

public partial class AboutPopup : Popup
{
	private readonly AboutPopupViewModel _viewModel;

	public AboutPopup()
	{
		InitializeComponent();

		_viewModel = new AboutPopupViewModel();
		BindingContext = _viewModel;
		Opened += AboutPopup_Opened;
	}

	private void AboutPopup_Opened(object? sender, CommunityToolkit.Maui.Core.PopupOpenedEventArgs e)
	{
		if (_viewModel is AboutPopupViewModel vm)
		{
			vm.SetCurrentPopupView(this);
		}
	}	
}