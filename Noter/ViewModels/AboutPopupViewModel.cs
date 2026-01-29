using System.Windows.Input;
using CommunityToolkit.Maui.Views;

namespace Noter.ViewModels
{
	public class AboutPopupViewModel
    {
		public ICommand ClosePopup { get; }

		private Popup? _popup;

		public string ApplicationVersion
		{
			get
			{
				return "Verson " + AppInfo.Current.VersionString;
			}
		}

		public AboutPopupViewModel()
		{
			ClosePopup = new Command(async () => await CloseAboutPopup());
		}

		public void SetCurrentPopupView(Popup popup)
		{
			_popup = popup;
		}

		public async Task CloseAboutPopup()
		{
			if (_popup != null) 
			{
				await _popup.CloseAsync();
			}
		}

	}
}
