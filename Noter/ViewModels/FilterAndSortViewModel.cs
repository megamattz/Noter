using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;

namespace Noter.ViewModels
{
	public class FilterAndSortViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public ICommand ClosePopup { get; }

		private Popup? _popup;

		public FilterAndSortViewModel()
		{
			ClosePopup = new Command(async () => await CloseFilterAndSortPopup());
		}

		public void SetCurrentPopupView(Popup popup)
		{
			_popup = popup;
		}

		public async Task CloseFilterAndSortPopup()
		{
			if (_popup != null)
			{
				await _popup.CloseAsync();
			}
		}
	}
}
