using Noter.CoreBusiness;

namespace Noter.UseCases.UseCaseInterfaces
{
	public interface IViewNotesUseCase
	{
		Task<List<Note>> ExecuteAsync(string searchTerm);
	}
}
