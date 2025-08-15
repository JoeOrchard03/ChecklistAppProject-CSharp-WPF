using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using InvTrackerApp.Models;

//Placed in the inventory app namespace for organization
namespace InvTrackerApp
{
    //Define class, inherits from window to access the methods from that class
    public partial class MainWindow : Window
    {
        //Defines a list of items, ObservableCollection means that when you add or remove items the UI gets notified and updated
        private ObservableCollection<ChecklistItem> items = new ObservableCollection<ChecklistItem>();

        //This constructor runs when the window is created
        public MainWindow()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                MessageBox.Show(e.ExceptionObject.ToString(), "Unhandled Exception");
            };
            //InitializeComponent sets up the UI from the XAML file
            InitializeComponent();
            //Binds the ItemList listbox to the items list, now whenever items changes the listbox will update
            ItemList.ItemsSource = items;
        }

        //Runs when you click the Add button
        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            //input gets the input from the text box and .Trim removes the whitespace before the beginning and end of the string
            var input = ItemInput.Text.Trim();
            //Make sure input is not empty
            if (string.IsNullOrEmpty(input)) return;
            //Add item to the list, updating the listbox
            items.Add(new ChecklistItem { Text = input });
            //Clears the ItemInput ready for the next item
            ItemInput.Clear();
        }

        //Adds each item from a pre-made list
        public void AddItemFromPreMadeList(ChecklistItem itemToAdd)
        {
            Debug.WriteLine("Current count of items are: " + items.Count.ToString());
            //Adds item to the items collection
            items.Add(itemToAdd);
            Debug.WriteLine("Adding " + itemToAdd.Text);
            Debug.WriteLine("New count of items are: " + items.Count.ToString());
        }

        public void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Deleting item");
            //Gets the type of the button that was clicked
            Button clickedButton  = (Button)sender as Button;
            
            if (clickedButton?.Tag is ChecklistItem item)
            {
                //Remove item from collection, as it is an observable collection, will also remove it from listbox
                items.Remove(item);
            }
        }

        
        private void OpenPreMadeListWindow_Click(object sender, RoutedEventArgs e)
        {
            //Gives PreMadeList a refernce to the items in the main list
            PreMadeList.MainListItems = items;
            
            //Opens a new PreMadeListWindow
            PreMadeListWindow window = new PreMadeListWindow(this);
            //Helper function to open the new window in the same place as the currently opened window's position
            Helper.OpenAtSamePosition(this, window);
        }
        
        //Load items from json
        private ObservableCollection<ChecklistItem> LoadMyItems(string filePath)
        {
            string json = File.ReadAllText(filePath);
            var list = JsonSerializer.Deserialize<List<string>>(json);
            return new ObservableCollection<ChecklistItem>(list?.Select(x => new ChecklistItem { Text = x }) ?? new List<ChecklistItem>());
        }
    }
}