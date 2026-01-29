using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noter.UseCases.DatabaseInterfaces;
using Noter.UseCases.UseCaseInterfaces;

namespace Noter.UseCases
{
	public class CountNotesUseCase : ICountNotesUseCase
	{
		private readonly INoterDataStoreRepository _notesDataStoreRepository;

		public CountNotesUseCase(INoterDataStoreRepository noterDataStoreRepository)
		{
			_notesDataStoreRepository = noterDataStoreRepository;
		}
		public Task<int> ExecuteAsync()
		{
			return _notesDataStoreRepository.CountNotesAsync();
		}
	}
}
