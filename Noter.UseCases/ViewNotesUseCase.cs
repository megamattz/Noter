using Noter.CoreBusiness;
using Noter.UseCases.DatabaseInterfaces;
using Noter.UseCases.UseCaseInterfaces;

namespace Noter.UseCases
{
	public class ViewNotesUseCase : IViewNotesUseCase
	{
		private readonly INoterDataStoreRepository _notesDataStoreRepository;

		public ViewNotesUseCase(INoterDataStoreRepository notesDataStoreRepository)
		{
			_notesDataStoreRepository = notesDataStoreRepository;
		}

		public async Task<List<Note>> ExecuteAsync(string searchTerm)
		{
			return await _notesDataStoreRepository.GetNotesAsync(searchTerm);
		}
	}
}
