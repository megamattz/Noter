namespace Noter.UseCases.UseCaseInterfaces
{
	public interface IEditNoteUseCase
	{
		Task<bool> ExecuteAsync(CoreBusiness.Note updatedNote);
	}
}
