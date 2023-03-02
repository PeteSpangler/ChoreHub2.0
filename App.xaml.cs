namespace ChoreHub2._0;

public partial class App : Application
{
	public static NoteRepository NoteRepo { get; private set; }
    public App(NoteRepository repo)
	{
        InitializeComponent();

		MainPage = new AppShell();

		NoteRepo = repo;
	}
}
