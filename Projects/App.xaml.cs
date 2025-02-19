using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Data.Services;
using Business.Services;



namespace Presentation
{
    public partial class App : Application
    {
        public static IServiceProvider? ServiceProvider { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=MyDatabase;Integrated Security=True;Connect Timeout=30"));


            services.AddScoped<ICustomerContactsRepository, CustomerContactsRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IServicesRepository, ServicesRepository>();
            services.AddScoped<IStatusTypeRepository, StatusTypeRepository>();
            services.AddScoped<IUnitTypesRepository, UnitTypesRepository>();

            services.AddScoped<CustomerContactsService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ServicesService>();
            services.AddScoped<StatusTypesService>();
            services.AddScoped<UnitTypesService>();

            services.AddSingleton<MainWindow>();

            ServiceProvider = services.BuildServiceProvider();


            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}


