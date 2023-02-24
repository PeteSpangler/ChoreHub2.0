namespace ChoreHub2._0.Views;

public partial class AllChoresPage : ContentPage
{
	public AllChoresPage()
	{
		InitializeComponent();
	}
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        choreCollection.SelectedItem = null;
    }
}