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
				NoteModifiedDate = DateTime.UtcNow,
				NoteCreationDate = DateTime.UtcNow,
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
			note.NoteModifiedDate = DateTime.UtcNow;
			note.NoteCategory = updatedNote.NoteCategory;

			int recordsAdded = await _dbContext.SaveChangesAsync();

			return recordsAdded > 0;
		}

		/// <summary>
		/// Returns a list of notes
		/// </summary>
		/// <param name="searchTerm">search term filter. If empty no filter is applied</param>
		/// <returns></returns>
		public async Task<List<Note>> GetNotesAsync(string searchTerm, NoteCategories[] noteCategoriesFilter, SortingColumn sortingColumn, SortDirection sortDirection)
		{
			IQueryable<Note> query = _dbContext.Notes;
			string searchTermLowerCase = searchTerm.Trim().ToLowerInvariant();

			if (!string.IsNullOrEmpty(searchTermLowerCase))
			{
				query = query.Where(q => q.NoteText.ToLower().Contains(searchTermLowerCase) ||
								q.NoteTitle.ToLower().Contains(searchTermLowerCase));

			}

			if (noteCategoriesFilter != null && noteCategoriesFilter.Length > 0)
			{
				List<NoteCategories> filterList = noteCategoriesFilter.ToList();
				query = query.Where(q => filterList.Any(c => c == q.NoteCategory));
			}

			if (sortingColumn == SortingColumn.DateModified)
			{
				query = sortDirection == SortDirection.Descending ? query.OrderByDescending(q => q.NoteModifiedDate) : query.OrderBy(q => q.NoteModifiedDate);
			}

			else if (sortingColumn == SortingColumn.DateCreated)
			{
				query = sortDirection == SortDirection.Descending ? query.OrderByDescending(q => q.NoteCreationDate) : query.OrderBy(q => q.NoteCreationDate);
			}

			else
			{
				query.OrderByDescending(q => q.NoteId);
			}

			return await query.ToListAsync();
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

		public async Task<int> CountNotesAsync()
		{
			return await _dbContext.Notes.CountAsync();
		}
	}
}
