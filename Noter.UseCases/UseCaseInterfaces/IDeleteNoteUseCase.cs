using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noter.UseCases.UseCaseInterfaces
{
	public interface IDeleteNoteUseCase
	{
		Task<bool> ExecuteAsync(int noteID);
	}
}
