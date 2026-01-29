using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Noter.UseCases.UseCaseInterfaces;

namespace Noter.ViewModels
{
	public class AboutPopupViewModel : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler? PropertyChanged;

		public ICommand ClosePopup { get; }

		private Popup? _popup;
		private int? _totalNoteCount;

		private ICountNotesUseCase _countNotesUseCase;

		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}


		public string ApplicationVersion
		{
			get
			{
				return AppInfo.Current.VersionString;
			}
		}

		public async Task LoadTotalNoteCountAsync()
		{
			int count = await _countNotesUseCase.ExecuteAsync();
			_totalNoteCount = count;
			
			OnPropertyChanged(nameof(TotalNoteCount));
		}

		public string TotalNoteCount
		{
			get
			{
				return _totalNoteCount?.ToString() ?? "";
			}
		}

		public AboutPopupViewModel(ICountNotesUseCase countNotesUseCase)
		{
			_countNotesUseCase = countNotesUseCase;
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
