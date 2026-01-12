using Noter.CoreBusiness;
using Noter.UseCases.DatabaseInterfaces;
using Noter.UseCases.UseCaseInterfaces;

namespace Noter.UseCases
{
	public class EditNoteUseCase : IEditNoteUseCase
	{

		private readonly INoterDataStoreRepository _notesDataStoreRepository;
		public EditNoteUseCase(INoterDataStoreRepository noterDataStoreRepository)
		{
			_notesDataStoreRepository = noterDataStoreRepository;
		}

		public async Task<bool> ExecuteAsync(Note updatedNote)
		{
			return await _notesDataStoreRepository.EditNoteAsync(updatedNote);
		}
	}
}
