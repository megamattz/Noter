using Noter.CoreBusiness;

namespace Noter.UseCases.DatabaseInterfaces
{
	public interface INoterDataStoreRepository
	{
		Task<List<Note>> GetNotesAsync();

		Task<bool> AddNoteAsync(Note newNote);
	}
}
