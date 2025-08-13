using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using InvTrackerApp.Models;
using static InvTrackerApp.Helper;

//Placed in the inventory app namespace for organization
namespace InvTrackerApp
{
    public partial class PreMadeListWindow : Window
    {
        public ObservableCollection<PreMadeList> preMadeLists = new ObservableCollection<PreMadeList>();
        private static bool firstLoad = true;
        public MainWindow MainWindow { get; }

        public PreMadeListWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;
            if (firstLoad)
            {
                File.WriteAllText(PreMadeListStorage.PreMadeListFilePath, string.Empty);
                firstLoad = false;  
            }
            Debug.WriteLine("Loading from " + PreMadeListStorage.PreMadeListFilePath);
            preMadeLists.Clear();
            foreach (var item in PreMadeListStorage.Load())
            {
                preMadeLists.Add(item);
            }
            Debug.WriteLine("Loaded from " + PreMadeListStorage.PreMadeListFilePath);
            PreMadeLists.ItemsSource = preMadeLists;
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            OpenAtSamePosition(this, MainWindow);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton  = (Button)sender as Button;
            
            if (clickedButton?.Tag is PreMadeList list)
            {
                MessageBox.Show($"Deleting {list.Name} from pre-made lists, items contained: {string.Join(", ", list.Items)}");
                preMadeLists.Remove(list);
                PreMadeListStorage.Remove(list.Name);
            }
        }
        
        private void ListAddButton_Click(object sender, RoutedEventArgs e)
        {
            //Converts sender to the button variable type.
            Button clickedButton  = (Button)sender as Button;

            //Checks if the button's tag is a PreMadeList which it should be, assigns it to the list variable
            if (clickedButton?.Tag is PreMadeList list)
            {
                MessageBox.Show($"Adding {list.Name}'s items: {string.Join(", ", list.Items.Select(i => i.Text))} to main list");
                if (MainWindow != null)
                {
                    foreach (ChecklistItem i in list.Items)
                    {
                        MainWindow.AddItemFromPreMadeList(i);
                    }
                }
                
                foreach (var l in preMadeLists)
                {
                    // Notify UI that CanAdd might have changed
                    var descriptor = DependencyPropertyDescriptor.FromProperty(
                        Button.IsEnabledProperty, typeof(Button));
                }
                
                PreMadeLists.Items.Refresh();
            }
        }

        private void OpenCreateCustomListWindow_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Create Custom List");
            CustomListWindow window = new CustomListWindow(this);
            OpenAtSamePosition(this, window);
        }
    }
}