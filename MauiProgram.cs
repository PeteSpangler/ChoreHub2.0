﻿using Microsoft.Extensions.DependencyInjection;

namespace ChoreHub2._0;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("IBMPlexSans-Regular.ttf", "IBMPlexSansRegular");
				fonts.AddFont("IBMPlexSans-Semibold.ttf", "IBMPlexSansSemibold");
			});

		string dbPath = FileAccessHelper.GetLocalFilePath("user.db3");
		builder.Services.AddSingleton<UserRepository>(s => ActivatorUtilities.CreateInstance<UserRepository>(s, dbPath));

		return builder.Build();
	}
}
