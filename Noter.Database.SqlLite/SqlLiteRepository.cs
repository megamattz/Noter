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

		/// <summary>
		/// Adds a new note
		/// </summary>
		/// <param name="newNote"></param>
		/// <returns></returns>
		public async Task<bool> AddNoteAsync(Note newNote)
		{
			await _dbContext.Notes.AddAsync(new Note()
			{
				NoteText = newNote.NoteText,
				NoteTitle = newNote.NoteTitle,
				NoteModifiedDate = DateTimeOffset.Now,
				NoteCreationDate = DateTimeOffset.Now,
			});

			int recordsAdded = await _dbContext.SaveChangesAsync();
			return recordsAdded > 0;
		}

		/// <summary>
		/// Returns a list of notes
		/// </summary>
		/// <returns></returns>
		public async Task<List<Note>> GetNotesAsync()
		{
			return await _dbContext.Notes.ToListAsync();
		}
	}
}
