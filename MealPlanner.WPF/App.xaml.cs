using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using MealPlanner.Domain.Models;
using MealPlanner.Domain.Services;
using MealPlanner.EntityFramework;
using MealPlanner.EntityFramework.Services;
using MealPlanner.WPF.ViewModels;
using MealPlanner.WPF.Views;

namespace MealPlanner.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (AppIsAlreadyRunning())
            {
                MessageBox.Show("Application is already running!");
                App.Current.Shutdown();
                Process.GetCurrentProcess().Kill();
                return;
            }

            IServiceProvider serviceProvider = CreateServiceProvider();

            Window window = serviceProvider.GetRequiredService<ShellView>();
            window.DataContext = serviceProvider.GetRequiredService<MainViewModel>();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<DietContextFactory>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IDietDataService, DietDataService>();
            services.AddScoped<MainViewModel>();
            services.AddScoped<ShellView>(s => new ShellView(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }

        private bool AppIsAlreadyRunning()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(currentProcess.ProcessName);

            foreach (Process process in processes)
            {
                if ((process.Id != currentProcess.Id) && (process.MainModule!.FileName == currentProcess.MainModule!.FileName))
                    return true;
            }

            return false;
        }
    }
}
