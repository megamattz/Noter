using Noter.CoreBusiness;

namespace Noter.UseCases.UseCaseInterfaces
{
	public interface IViewNoteUseCase
	{
		Task<Note> ExecuteAsync(int noteId);
	}
}
