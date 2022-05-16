using CarPool.App.Extensions;
using CarPool.App.Services;
using CarPool.App.Services.MessageDialog;
using CarPool.App.ViewModels;
using CarPool.App.Views;
using CarPool.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using CarPool.App.Factories;
using CarPool.App.Settings;
using CarPool.BL;
using CarPool.BL.Facades;
using CarPool.DAL.Factories;
using CarPool.DAL.UnitOfWork;
using Microsoft.Extensions.Options;


namespace CarPool.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("cs");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("cs");

            _host = Host.CreateDefaultBuilder()
                //.ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices((context, services) => { ConfigureServices(context.Configuration, services); })
                .Build();
        }

        private static void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
        {
            builder.AddJsonFile(@"appsettings.json", false, false);
        }

        private static void ConfigureServices(IConfiguration configuration,
            IServiceCollection services)
        {
            services.AddBLServices();

            services.Configure<DALSettings>(configuration.GetSection("CarPool:DAL"));

            services.AddSingleton<IDbContextFactory<CarRideDbContext>>(provider =>
            {
                //var dalSettings = provider.GetRequiredService<IOptions<DALSettings>>().Value;
                return new SqliteDbContextFactory(/*dalSettings.ConnectionString!, dalSettings.SkipMigrationAndSeedDemoData*/);
            });

            services.AddSingleton<MainWindow>();


            services.AddSingleton<IMessageDialogService, MessageDialogService>();
            services.AddSingleton<IMediator, Mediator>();

            services.AddSingleton<MainViewModel>();

            services.AddSingleton<CreateAccountViewModel>();
            services.AddSingleton<LoginViewModel>();

            services.AddSingleton<RidesViewModel>();

            services.AddSingleton<ManageAccountViewModel>();
            services.AddSingleton<EditCarViewModel>();

            services.AddSingleton<MyRidesViewModel>();
            services.AddSingleton<EditRideViewModel>();


            //services.AddSingleton<MainViewModel>();
            //services.AddSingleton<IIngredientListViewModel, IngredientListViewModel>();
            //services.AddFactory<IIngredientDetailViewModel, IngredientDetailViewModel>();
            //services.AddSingleton<IRecipeListViewModel, RecipeListViewModel>();
            //services.AddFactory<IRecipeDetailViewModel, RecipeDetailViewModel>();
            //services.AddFactory<IIngredientAmountDetailViewModel, IngredientAmountDetailViewModel>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var dbContextFactory = _host.Services.GetRequiredService<IDbContextFactory<CarRideDbContext>>();

            var dalSettings = _host.Services.GetRequiredService<IOptions<DALSettings>>().Value;

            await using (var dbx = await dbContextFactory.CreateDbContextAsync())
            {
                if (dalSettings.SkipMigrationAndSeedDemoData)
                {
                    await dbx.Database.EnsureDeletedAsync();
                    await dbx.Database.EnsureCreatedAsync();
                }
                else
                {
                    await dbx.Database.MigrateAsync();
                }
            }

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}
