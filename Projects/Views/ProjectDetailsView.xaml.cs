using System.Collections.ObjectModel;
using System.Windows;
using Data.Entities;
using Data.Interfaces;


namespace Presentation
    //Mesta här är också AI-Genererat. Min vy som ska ansvara för att visa detaljerna i projekten jag skapar och togglar edit läge och sparar ner sakerna jag ändrar.
    //Man ska också kunna klicka avbryt för att ångra redigering och då ska den visa det som var innan.
{
    public partial class ProjectDetailsView : Window
    {
        private readonly IProjectRepository _projectRepository;
        private readonly bool _isNewProject;
        private bool _isEditing = false;

        public Project Project { get; set; }  
        public ObservableCollection<StatusType> StatusTypes { get; set; } = new ObservableCollection<StatusType>();

        private Project _originalProject;

        public ProjectDetailsView(Project project, IProjectRepository projectRepository, bool isNewProject)
        {
            InitializeComponent();
            _projectRepository = projectRepository;
            _isNewProject = isNewProject;
            Project = project;

            
            _originalProject = new Project
            {
                ProjectNumber = project.ProjectNumber,
                Name = project.Name,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                ProjectManager = project.ProjectManager != null ? new Employee { FirstName = project.ProjectManager.FirstName } : new Employee(),
                Customer = project.Customer != null ? new Customer { CustomerName = project.Customer.CustomerName } : new Customer(),
                Service = project.Service != null ? new Service { Name = project.Service.Name, Price = project.Service.Price } : new Service(),
                StatusType = project.StatusType
            };

            _ = LoadStatusTypesAsync();

          
            DataContext = this;
        }

        private async Task LoadStatusTypesAsync()
        {
            var statusList = await _projectRepository.GetAllStatusTypesAsync();
            StatusTypes.Clear();
            foreach (var status in statusList)
            {
                StatusTypes.Add(status);
            }

         
            DataContext = this;
        }

        private void ToggleEditMode(object sender, RoutedEventArgs e) //Mina saker är readonly tills att man klickar på redigera då ska det inte vara readonly längre och man ska kunna
            //redigera projektet som man vill. Men projektnummret går inte att ändra eftersom det är automatiskt genererat.
        {
            _isEditing = !_isEditing;

            NameTextBox.IsReadOnly = !_isEditing;
            StartDatePicker.IsEnabled = _isEditing;
            EndDatePicker.IsEnabled = _isEditing;
            ProjectManagerTextBox.IsReadOnly = !_isEditing;
            CustomerTextBox.IsReadOnly = !_isEditing;
            ServiceTextBox.IsReadOnly = !_isEditing;
            TotalPriceTextBox.IsReadOnly = !_isEditing;
            StatusComboBox.IsEnabled = _isEditing;

            EditButton.Visibility = _isEditing ? Visibility.Collapsed : Visibility.Visible;
            SaveButton.Visibility = _isEditing ? Visibility.Visible : Visibility.Collapsed;
        }

        private async void SaveProject(object sender, RoutedEventArgs e) 
            //Lägger till eller uppdaterar projekt i databasen. Om det är ett nytt projekt kallas AddAsync och är det ett existerande är det UpdateAsync.
        {
     
            if (Project.ProjectManager == null)
            {
                Project.ProjectManager = new Employee();
            }
            if (Project.Customer == null)
            {
                Project.Customer = new Customer();
            }
            if (Project.Service == null)
            {
                Project.Service = new Service();
            }

           
            Project.Name = NameTextBox.Text;
            Project.StartDate = StartDatePicker.SelectedDate ?? Project.StartDate;
            Project.EndDate = EndDatePicker.SelectedDate ?? Project.EndDate;

            Project.ProjectManager.FirstName = ProjectManagerTextBox.Text;
            Project.Customer.CustomerName = CustomerTextBox.Text;
            Project.Service.Name = ServiceTextBox.Text;
            Project.Service.Price = decimal.TryParse(TotalPriceTextBox.Text, out var price) ? price : 0;

            Project.StatusType = (StatusType)StatusComboBox.SelectedItem;

            
            if (_isNewProject)
            {
                await _projectRepository.AddAsync(Project);
            }
            else
            {
                await _projectRepository.UpdateAsync(Project);
            }

            this.DialogResult = true;
            this.Close();
        }



        private void CancelEdit(object sender, RoutedEventArgs e) //Ser till att när jag klickar avbryt återgår jag till hur projektet var innan.
        {
        
            Project.Name = _originalProject.Name;
            Project.StartDate = _originalProject.StartDate;
            Project.EndDate = _originalProject.EndDate;
            Project.ProjectManager = _originalProject.ProjectManager;
            Project.Customer = _originalProject.Customer;
            Project.Service = _originalProject.Service;
            Project.StatusType = _originalProject.StatusType;

            this.DialogResult = false;
            this.Close();
        }
    }
}







