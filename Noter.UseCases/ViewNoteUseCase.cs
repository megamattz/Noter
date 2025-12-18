using Noter.CoreBusiness;
using Noter.UseCases.DatabaseInterfaces;
using Noter.UseCases.UseCaseInterfaces;

namespace Noter.UseCases
{
	public class ViewNoteUseCase : IViewNoteUseCase
	{
		private INoterDataStoreRepository _notesDataStoreRepository;

		public ViewNoteUseCase(INoterDataStoreRepository noterDataStoreRepository)
		{
			_notesDataStoreRepository = noterDataStoreRepository;
		}

		public async Task<Note> ExecuteAsync(int noteId)
		{
			return await _notesDataStoreRepository.GetNoteByIdAsync(noteId);			
		}
	}
}
