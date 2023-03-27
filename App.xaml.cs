using System;
using System.IO;
using System.Reflection;
using System.Windows;
using BuildingMaterials.DbContext;
using BuildingMaterials.Services;
using BuildingMaterials.Stores;
using BuildingMaterials.ViewModels;
using BuildingMaterials.Views;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingMaterials
{

    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; set; }
        public IConfiguration Configuration { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            NavigationStore store = new NavigationStore();
            DialogStore dialogStore = new DialogStore();
            ConfigureServices(serviceCollection,ref store, ref dialogStore);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            ServiceProvider.GetRequiredService<MainWindow>().Show();

            store.Height = 450;
            store.Width = 800;
            store.CurrentViewModel = new LoginViewModel(store, ServiceProvider.GetRequiredService<SqlServerDbContext>(), dialogStore);
        }

        private void ConfigureServices(IServiceCollection services, ref  NavigationStore store, ref DialogStore dialogStore)
        {
            services.AddSingleton(new MainWindow()
            {
                DataContext = new MainViewModel(store)
            });
            services.AddSingleton(dialogStore);
            services.AddTransient<LoginView>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<SqlServerDbContext>();

        }
    }
}
