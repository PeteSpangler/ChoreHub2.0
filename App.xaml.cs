namespace ChoreHub2._0;

public partial class App : Application
{
	public static UserRepository UserRepo { get; private set; }
    public App(UserRepository repo)
	{
		InitializeComponent();

		MainPage = new AppShell();

		UserRepo = repo;
	}
}
