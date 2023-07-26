using Clinic.Models;
using Clinic.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Clinic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            Configuration(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void Configuration(ServiceCollection services)
        {
            services.AddSingleton(typeof(IClinicRepo), typeof(ClinicRepo));
            services.AddSingleton<MainWindow>();
            services.AddSingleton<AddPatient>();
            services.AddSingleton<Drags>();
            services.AddSingleton<Services>();
            services.AddSingleton<Money>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var window = serviceProvider.GetService<MainWindow>();
            window.Show();
        }
    }
}
