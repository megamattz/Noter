using Noter.Views;

namespace Noter
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

			Routing.RegisterRoute(nameof(NotesPage), typeof(NotesPage));
		}
    }
}
