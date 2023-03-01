using System;
using System.Collections.Generic;
using ChoreHub2._0.Models;
using System.Threading;

namespace ChoreHub2._0.Views;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
	}

    public async void OnNewButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        await App.UserRepo.AddNewUser(newUser.Text);
        statusMessage.Text = App.UserRepo.StatusMessage;
    }

    public async void OnGetButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        List<User> user = await App.UserRepo.GetAllUsers();
        userList.ItemsSource = user;
    }

}