using Noter.UseCases.UseCaseInterfaces;

namespace Noter.Views;

public partial class AddNotePage : ContentPage
{
	private IAddNoteUseCase _addNoteUseCase;

	public AddNotePage(IAddNoteUseCase addNoteUseCase)
	{
		InitializeComponent();
		_addNoteUseCase = addNoteUseCase;
	}
}