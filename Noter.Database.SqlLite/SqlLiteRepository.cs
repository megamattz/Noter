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
				NoteCategory = newNote.NoteCategory,
				NoteModifiedDate = DateTimeOffset.Now,
				NoteCreationDate = DateTimeOffset.Now,
			});

			int recordsAdded = await _dbContext.SaveChangesAsync();
			return recordsAdded > 0;
		}

		/// <summary>
		/// Updates an existing note
		/// </summary>
		/// <param name="updatedNote"></param>
		/// <returns></returns>
		public async Task<bool> EditNoteAsync(Note updatedNote)
		{
			Note? note = await _dbContext.Notes.FirstOrDefaultAsync(n => n.NoteId == updatedNote.NoteId);

			if (note == null)
			{
				throw new KeyNotFoundException($"Note with ID {updatedNote.NoteId} not found to update");
			}

			note.NoteText = updatedNote.NoteText;
			note.NoteTitle = updatedNote.NoteTitle;
			note.NoteModifiedDate = DateTimeOffset.Now;
			note.NoteCategory = updatedNote.NoteCategory;

			int recordsAdded = await _dbContext.SaveChangesAsync();

			return recordsAdded > 0;
		}

		/// <summary>
		/// Returns a list of notes
		/// </summary>
		/// <param name="searchTerm">search term filter. If empty no filter is applied</param>
		/// <returns></returns>
		public async Task<List<Note>> GetNotesAsync(string searchTerm)
		{
			IQueryable<Note> query = _dbContext.Notes;
			string searchTermLowerCase = searchTerm.Trim().ToLowerInvariant();

			if (!string.IsNullOrEmpty(searchTermLowerCase))
			{
				query = query.Where(q => q.NoteText.ToLower().Contains(searchTermLowerCase) ||
								q.NoteTitle.ToLower().Contains(searchTermLowerCase));

			}

			return await query.OrderByDescending(q => q.NoteId).ToListAsync();
		}

		public async Task<Note> GetNoteByIdAsync(int nodeId)
		{
			Note? note = await _dbContext.Notes.FirstOrDefaultAsync(n => n.NoteId == nodeId);

			if (note == null)
			{
				throw new KeyNotFoundException($"Note with ID {nodeId} not found");
			}

			return note;
		}


		/// <summary>
		/// Deletes a note
		/// </summary>
		/// <param name="noteId"></param>
		/// <returns></returns>
		public async Task<bool> DeleteNoteAsync(int noteId)
		{
			Note note = await GetNoteByIdAsync(noteId);

			_dbContext.Notes.Remove(note);
			int recordsDeleted = await _dbContext.SaveChangesAsync();

			return recordsDeleted > 0;
		}
	}
}
