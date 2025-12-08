using Noter.CoreBusiness;
using Noter.UseCases.DatabaseInterfaces;

namespace Noter.Database.SqlLite
{
	// All the code in this file is included in all platforms.
	public class SqlLiteRepository : INoterDataStoreRepository
	{
		public Task<List<Note>> GetNotesAsync()
		{
			// Temp only for testing until sqllite is implemented

			List<Note> notes = new List<Note>();
			notes.Add(new Note()
			{
				NoteId = 1,
				NoteText = "Hello World",
				NoteTitle = "Title",
			});

			return Task.FromResult(notes);
		}
	}
}
