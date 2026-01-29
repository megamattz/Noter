using Noter.CoreBusiness;

namespace Noter.UseCases.DatabaseInterfaces
{
	public interface INoterDataStoreRepository
	{
		Task<List<Note>> GetNotesAsync(string searchTerm);

		Task<Note> GetNoteByIdAsync(int noteId);

		Task<bool> AddNoteAsync(Note newNote);

		Task<bool> EditNoteAsync(Note updatedNote);

		Task<bool> DeleteNoteAsync(int noteId);

		Task<int> CountNotesAsync();
	}
}
