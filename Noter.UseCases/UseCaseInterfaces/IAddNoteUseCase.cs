using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noter.UseCases.UseCaseInterfaces
{
	public interface IAddNoteUseCase
	{
		Task<bool> ExecuteAsync(CoreBusiness.Note newNote);
	}
}
