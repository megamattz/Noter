using Noter.UseCases.DatabaseInterfaces;
using Noter.UseCases.UseCaseInterfaces;

namespace Noter.UseCases
{
	public class DeleteNoteUseCase : IDeleteNoteUseCase
	{
		private readonly INoterDataStoreRepository _notesDataStoreRepository;

		public DeleteNoteUseCase(INoterDataStoreRepository noterDataStoreRepository)
		{
			_notesDataStoreRepository = noterDataStoreRepository;
		}

		public async Task<bool> ExecuteAsync(int noteID)
		{
			return await _notesDataStoreRepository.DeleteNoteAsync(noteID);
		}
	}
}
