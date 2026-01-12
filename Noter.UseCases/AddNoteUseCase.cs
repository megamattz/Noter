using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noter.CoreBusiness;
using Noter.UseCases.DatabaseInterfaces;
using Noter.UseCases.UseCaseInterfaces;

namespace Noter.UseCases
{
	public class AddNoteUseCase : IAddNoteUseCase
	{
		private readonly INoterDataStoreRepository _notesDataStoreRepository;

		public AddNoteUseCase(INoterDataStoreRepository noterDataStoreRepository)
		{
			_notesDataStoreRepository = noterDataStoreRepository;
		}

		public async Task<bool> ExecuteAsync(Note newNote)
		{
			return await _notesDataStoreRepository.AddNoteAsync(newNote);
		}
	}
}
