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
        //Collection of type PreMadeList
        public ObservableCollection<PreMadeList> preMadeLists = new ObservableCollection<PreMadeList>();
        //Reference to the MainWindow so user can go back to the original and not have to make a new one, losing data
        public MainWindow MainWindow { get; }

        public PreMadeListWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            //Gets the MainWindow from the constructors parameters (handed by the mainwindow function to open premadelistwindow)
            MainWindow = mainWindow;
            Debug.WriteLine("Loading from " + PreMadeListStorage.PreMadeListFilePath);
            //Clears and then loads items from the PreMadeList.json 
            preMadeLists.Clear();
            foreach (var item in PreMadeListStorage.Load())
            {
                preMadeLists.Add(item);
            }
            Debug.WriteLine("Loaded from " + PreMadeListStorage.PreMadeListFilePath);
            //Links the listbox to the collection of PreMadeList items
            PreMadeLists.ItemsSource = preMadeLists;
        }

        //Button to go back to the main window
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            OpenAtSamePosition(this, MainWindow);
        }

        //Deletes premade list from PreMadeLists.json, also removes from the preMadeLists collection
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            //Gets the button
            Button clickedButton  = (Button)sender as Button;
            
            //Makes sure the button is the correct type to avoid errors, assigns the list variable
            if (clickedButton?.Tag is PreMadeList list)
            {
                //Lets the user know what list they deleted and what it contained
                MessageBox.Show($"Deleting {list.Name} from pre-made lists, items contained: {string.Join(", ", list.Items)}");
                preMadeLists.Remove(list);
                PreMadeListStorage.Remove(list.Name);
            }
        }
        
        //Adds the pre-made list's items to the main list
        private void ListAddButton_Click(object sender, RoutedEventArgs e)
        {
            //Converts sender to the button variable type.
            Button clickedButton  = (Button)sender as Button;

            //Checks if the button's tag is a PreMadeList which it should be, assigns it to the list variable
            if (clickedButton?.Tag is PreMadeList list)
            {
                //Lets the user know what list they are adding and what items it contains
                MessageBox.Show($"Adding {list.Name}'s items: {string.Join(", ", list.Items.Select(i => i.Text))} to main list");
                //Checks for main window instance
                if (MainWindow != null)
                {
                    //Calls the AddItemFromPreMadeList function for each item in the premadelist that the user wants to add
                    foreach (ChecklistItem i in list.Items)
                    {
                        MainWindow.AddItemFromPreMadeList(i);
                    }
                }
                
                //Makes the item's checkbox match the appropriate value
                foreach (var l in preMadeLists)
                {
                    // Notify UI that CanAdd might have changed
                    var descriptor = DependencyPropertyDescriptor.FromProperty(
                        Button.IsEnabledProperty, typeof(Button));
                }
                
                //Refereshes the listbox
                PreMadeLists.Items.Refresh();
            }
        }

        //Opens the create custom list window, giving it a reference to this window so the user can come back to this instance of PreMadeListWindow
        private void OpenCreateCustomListWindow_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Create Custom List");
            CustomListWindow window = new CustomListWindow(this);
            OpenAtSamePosition(this, window);
        }
    }
}