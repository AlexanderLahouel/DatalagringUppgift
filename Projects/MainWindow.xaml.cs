using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Data.Entities;
using Data.Interfaces;

namespace Presentation
{
    //Det mesta i den här koden är AI-Genererat, försökte själv men fick aldrig till det.
    public partial class MainWindow : Window
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IServicesRepository _servicesRepository;

        public ObservableCollection<Project> Projects { get; set; } = new ObservableCollection<Project>();

        public MainWindow(IProjectRepository projectRepository,
                          ICustomerRepository customerRepository,
                          IEmployeeRepository employeeRepository,
                          IServicesRepository servicesRepository)
        {
            InitializeComponent();
            _projectRepository = projectRepository;
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _servicesRepository = servicesRepository;
            _ = LoadProjectsAsync();
        }

        private async Task LoadProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            Projects.Clear();
            foreach (var project in projects)
            {
                Projects.Add(project);
            }
            DataContext = this;
        }

        private void SelectProject(object sender, SelectionChangedEventArgs e)
        {
         
            ProjectList.SelectedItem = null;
        }

        private void ViewProjectDetails(object sender, RoutedEventArgs e) //Öppnar min andra vy, "Viewprojectdetails" som ska visa egenskaperna i projektet utan att kunna redigera.
        {
            if ((sender as Button)?.Tag is Project selectedProject)
            {
                var detailsWindow = new ProjectDetailsView(selectedProject, _projectRepository, false);
                detailsWindow.ShowDialog();
            }
        }

        private void EditProject(object sender, RoutedEventArgs e) 
        {
            if ((sender as Button)?.Tag is Project selectedProject)
            {
                var detailsWindow = new ProjectDetailsView(selectedProject, _projectRepository, true);
                detailsWindow.ShowDialog();
                _ = LoadProjectsAsync();
            }
        }

        private async void AddProject(object sender, RoutedEventArgs e) 
            //AI-Genererat: Behövde hjälp med metod som skulle skapa standardvärden för customer, projectmanager, service eftersom min presentation kraschade för att jag manuellt inte
            //ville lägga till saker i min databas så min metod skapar dem och lägger till dem i databasen när jag skriver in ett nytt.
            //Defaultstatus är "Ej påbörjat" hittas det inte i databasen så visas felmeddelande.
            //Skapar nytt projekt och sparas ner och uppdaterar listan.
                                                                        
        {
            var newProjectNumber = await _projectRepository.GenerateProjectNumberAsync();

            var customer = await _customerRepository.AddOrGetCustomerAsync("Ej vald");
            var projectManager = await _employeeRepository.AddOrGetEmployeeAsync("Ej vald");

            var service = await _servicesRepository.AddOrGetServiceAsync("Ej vald", 2000, 1);

            var defaultStatus = (await _projectRepository.GetAllStatusTypesAsync())
                .FirstOrDefault(s => s.Status == "Ej påbörjat");

            if (defaultStatus == null)
            {
                MessageBox.Show("Kunde inte hitta standardstatusen i databasen.", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newProject = new Project
            {
                ProjectNumber = newProjectNumber,
                Name = "Nytt Projekt",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
                ProjectManagerId = projectManager.Id,
                CustomerId = customer.CustomerId,
                ServiceId = service.Id,
                StatusTypeId = defaultStatus.Id
            };

            var detailsWindow = new ProjectDetailsView(newProject, _projectRepository, true);
            detailsWindow.ShowDialog();

            await _projectRepository.AddAsync(newProject);
            await LoadProjectsAsync();
        }

        private async void DeleteProject(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is Project selectedProject)
            {
                var result = MessageBox.Show(
                    "Är du säker på att du vill ta bort projektet?",
                    "Bekräfta borttagning",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (result == MessageBoxResult.Yes)
                {
                    await _projectRepository.DeleteAsync(selectedProject.Id);
                    await LoadProjectsAsync();
                }
            }
        }
    }
}




