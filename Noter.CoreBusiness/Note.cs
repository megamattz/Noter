using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Noter.CoreBusiness
{
	// All the code in this file is included in all platforms.
	public class Note
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int NoteId { get; set; }
		public string NoteTitle { get; set; } = "";
		public string NoteText { get; set; } = "";
		public DateTime NoteCreationDate { get; set; }
		public DateTime NoteModifiedDate { get;set; }
		public NoteCategories NoteCategory { get; set; } = NoteCategories.General;
	}
}
