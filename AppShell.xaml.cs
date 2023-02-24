namespace ChoreHub2._0;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(Views.NotePage), typeof(Views.NotePage));
        Routing.RegisterRoute(nameof(Views.ChorePage), typeof(Views.ChorePage));
        Routing.RegisterRoute(nameof(Views.AllChoresPage), typeof(Views.AllChoresPage));
    }
}
