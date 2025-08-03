using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using static InvTrackerApp.Helper;

//Placed in the inventory app namespace for organization
namespace InvTrackerApp
{
    public partial class PreMadeListWindow : Window
    {
        public ObservableCollection<PreMadeList> preMadeLists = new ObservableCollection<PreMadeList>();
        
        public PreMadeListWindow()
        {
            InitializeComponent();
            preMadeLists = LoadPreMadeLists("PreMadeLists.json");
            PreMadeLists.ItemsSource = preMadeLists;
            Debug.WriteLine("PreMadeListWindow Loaded");
            foreach (var list in preMadeLists)
            {
                Debug.WriteLine("list name is: " + list.Name);
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Window newWindow = new MainWindow();
            OpenAtSamePosition(this, newWindow);
        }

        private ObservableCollection<PreMadeList> LoadPreMadeLists(string filePath)
        {
            string json = File.ReadAllText(filePath);
            var lists = JsonSerializer.Deserialize<List<PreMadeList>>(json);
            return new ObservableCollection<PreMadeList>(lists ?? new List<PreMadeList>());
        }

        private void ListAddButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("List add button clicked");
        }
    }
}