using Microsoft.EntityFrameworkCore;
using Noter.CoreBusiness;
using Noter.UseCases.DatabaseInterfaces;

namespace Noter.Database.SqlLite
{
	// All the code in this file is included in all platforms.
	public class SqlLiteRepository : INoterDataStoreRepository
	{
		NoterDBContext _dbContext;

		public SqlLiteRepository(NoterDBContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<Note>> GetNotesAsync()
		{
			
			// Temp only for testing until add is implemented

			//await _dbContext.Notes.AddAsync(new Note()
			//{
			//	NoteId = 1,
			//	NoteText = "Hello World",
			//	NoteTitle = "Title",
			//});

			await _dbContext.SaveChangesAsync();

			//List<Note> notes = new List<Note>();
			//notes.Add(new Note()
			//{
			//	NoteId = 1,
			//	NoteText = "Hello World",
			//	NoteTitle = "Title",
			//});

			return await _dbContext.Notes.ToListAsync();
		}
	}
}
