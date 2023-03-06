using Microsoft.Extensions.DependencyInjection;
using ChoreHub2._0.Repositories;

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

		string dbPath = FileAccessHelper.GetLocalFilePath("notes.db3");
		builder.Services.AddSingleton<NoteRepository>(s => ActivatorUtilities.CreateInstance<NoteRepository>(s, dbPath));

		return builder.Build();
	}
}
