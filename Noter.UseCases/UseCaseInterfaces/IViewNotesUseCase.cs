using Noter.CoreBusiness;

namespace Noter.UseCases.UseCaseInterfaces
{
	public interface IViewNotesUseCase
	{
		Task<List<Note>> ExecuteAsync(string searchTerm, NoteCategories[]? noteCategoriesFilter, SortingColumn sortingColumn, SortDirection sortDirection);
	}
}
