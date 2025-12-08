namespace Noter.CoreBusiness
{
	// All the code in this file is included in all platforms.
	public class Note
	{
		public int NoteId { get; set; }
		public string NoteTitle { get; set; } = "";
		public string NoteText { get; set; } = "";
		//public DateTimeOffset NoteCreationDate { get; set; }
		//public DateTimeOffset NoteModifiedDate { get;set; }

		// public NoteCategories NoteCetegory { get; set; }
	}
}
